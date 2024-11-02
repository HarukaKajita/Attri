using System;
using System.Linq;
using Attri.Runtime.Extensions;
using Unity.Mathematics;

namespace Attri.Runtime
{
	public class FloatCompressor
	{
		// Encode Params
		public class EncodeParam
		{
			public float Center;
			public float ExpRangeCenterOffset;
			public int LossBitCount;

			public EncodeParam(float center, float expRangeCenterOffset, int lossBitCount)
			{
				Center = center;
				ExpRangeCenterOffset = expRangeCenterOffset;
				LossBitCount = lossBitCount;
			}
		}
		// Decode Params
		public class DecodeParam
		{
			public int ExponentBits;
			public int LossBitCount;
			public float Offset;
			
			public DecodeParam(int exponentBits, int lossBitCount, float offset)
			{
				ExponentBits = exponentBits;
				LossBitCount = lossBitCount;
				Offset = offset;
			}
		}
		
		public int Precision;
		// [frame][element][component]
		public float[][][] Original;// IDataProviderでもいいかも
		public float[][][] Compressed;
		
		// Decode Params [frame][ComponentNum]
		public EncodeParam[][] EncodeParams;
		public DecodeParam[][] DecodeParams;
		public FloatCompressor(float[][][] original, int precision)
		{
			Precision = precision;
			Original = original;
			EncodeParams = Array.Empty<EncodeParam[]>();
			DecodeParams = Array.Empty<DecodeParam[]>();
		}
		
		// 成分ごとに圧縮設定を持つ。要素毎ではない。
		public float[][][] Compress()
		{
			var frameCount = Original.Length;
			EncodeParams = new EncodeParam[frameCount][];
			DecodeParams = new DecodeParam[frameCount][];
			Compressed = Original.EmptyClone();
			
			// Component毎に圧縮パラメータを計算
			for(var frameId = 0; frameId < frameCount; frameId++)
			{
				var components = Original[frameId].Transpose();
				var componentCount = components.Length;
				EncodeParams[frameId] = new EncodeParam[componentCount];
				DecodeParams[frameId] = new DecodeParam[componentCount];
				// 成分ごとに圧縮
				for (var componentId = 0; componentId < componentCount; componentId++)
				{
					EncodeParams[frameId][componentId] = CalEncodeParam(components[componentId]);
					DecodeParams[frameId][componentId] = CalDecodeParam(components[componentId]);
				}
			}
			// 圧縮
			for (var frameId = 0; frameId < frameCount; frameId++)
			{
				var elementCount = Original[frameId].Length;
				for (int elementId = 0; elementId < elementCount; elementId++)
				{
					var componentCount = Original[frameId][elementId].Length;
					for (int componentId = 0; componentId < componentCount; componentId++)
					{
						var originalValue = Original[frameId][elementId][componentId];
						var encodeParam = EncodeParams[frameId][componentId];
						var decodeParam = DecodeParams[frameId][componentId];
						Compressed[frameId][elementId][componentId] = CompressValue(originalValue, encodeParam, decodeParam);
					}
				}
			}
			return Compressed;
		}
		
		private EncodeParam CalEncodeParam(float[] componentValues)
		{
			var min = componentValues.Min();
			var max = componentValues.Max();
			var range = max - min;
			var center = (max + min) / 2;
			var exp = (int)Math.Ceiling(Math.Log(range+float.Epsilon,2));//+127してからbitにする
			var expRangeCenterOffset = math.exp2(exp) * 1.5f;// 1.5fは中心をずらすためのオフセット
			var lossBitCount = 23 - Precision;
			return new EncodeParam(center, expRangeCenterOffset, lossBitCount);
		}

		private DecodeParam CalDecodeParam(float[] componentValues)
		{
			// Decode Uniform Params
			var min = componentValues.Min();
			var max = componentValues.Max();
			var range = max - min;
			var center = (max + min) / 2;
			var exp = (int)Math.Ceiling(Math.Log(range+float.Epsilon,2));//+127してからbitにする
			var expRangeCenterOffset = math.exp2(exp) * 1.5f;// 1.5fは中心をずらすためのオフセット
			// Decode Uniform Params
			var E = (exp+127) << 23;
			var lossBitCount = 23 - Precision;
			var offset = -center + expRangeCenterOffset;
			return new DecodeParam(E, lossBitCount, offset);
		}
		
		static float CompressValue(float value, EncodeParam encodeParam, DecodeParam decodeParam)
		{
			if(value is float.NaN) return float.NaN;
			// Encode
			var M = Encode(value, encodeParam);
			// Decode
			var decoded = Decode(M, decodeParam);
			return decoded;
		}

		private static int Encode(float value, EncodeParam encodeParam)
		{
			// Encode Preparation
			var center = encodeParam.Center;
			var expRangeCenterOffset = encodeParam.ExpRangeCenterOffset;
			var lossBitCount = encodeParam.LossBitCount;
			
			// Encode ベイクする際は下位ビットを切り捨てる
			var M = (value-center+expRangeCenterOffset).AsInt() >> lossBitCount;
			return M;
		}

		private static float Decode(int mantissa, DecodeParam decodeParam)
		{
			mantissa <<= decodeParam.LossBitCount;
			var preFloat = BitConverter.ToSingle(BitConverter.GetBytes(decodeParam.ExponentBits | mantissa), 0);
			return preFloat - decodeParam.Offset;
		}
	}
}