using System;
using System.Linq;
using Attri.Runtime.Extensions;
using Unity.Mathematics;
using UnityEngine;

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
			
			public static string ExponentBitsName = nameof(ExponentBits);
			public static string LossBitCountName = nameof(LossBitCount);
			public static string OffsetName = nameof(Offset);
			
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
		public float[][][] Compress(bool perFrame = false)
		{
			var frameCount = Original.Length;
			EncodeParams = new EncodeParam[frameCount][];
			DecodeParams = new DecodeParam[frameCount][];
			Compressed = Original.EmptyClone();

			// 全フレームの成分を結合
			var allFrameComponents= Array.Empty<float[]>();
			if (!perFrame)
			{
				var allFrameElements = Original.SelectMany(f => f).ToArray();
				allFrameComponents = allFrameElements.Transpose();
			}
			
			// Component毎に圧縮パラメータを計算
			for(var frameId = 0; frameId < frameCount; frameId++)
			{
				var components = allFrameComponents;
				if (perFrame) components = Original[frameId].Transpose();
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

		public static float Decode(int mantissa, DecodeParam decodeParam)
		{
			return Decode(mantissa, decodeParam.ExponentBits, decodeParam.LossBitCount, decodeParam.Offset);
		}
		public static float Decode(int mantissa, int exponentBits, int lossBitCount, float offset)
		{
			mantissa <<= lossBitCount;
			var preFloat = BitConverter.ToSingle(BitConverter.GetBytes(exponentBits | mantissa), 0);
			return preFloat - offset;
		}
		
		// [frame][component] フレーム毎に異なる設定で圧縮した場合の設定
		public CompressionParams[][] MakeCompressionParamsObjectPerFrame()
		{
			var frameCount = Original.Length;
			var paramsArray = new CompressionParams[frameCount][];
			for (var frameId = 0; frameId < frameCount; frameId++)
			{
				var componentCount = EncodeParams[frameId].Length;
				paramsArray[frameId] = new CompressionParams[componentCount];
				for (var compoId = 0; compoId < componentCount; compoId++)
				{
					var encodeParam = EncodeParams[frameId][compoId];
					var decodeParam = DecodeParams[frameId][compoId];
					var param = ScriptableObject.CreateInstance<CompressionParams>();
					param.name = $"Frame{frameId}_Component{compoId}";
					// Encode
					param.SetEncodeIntParam(nameof(encodeParam.LossBitCount), encodeParam.LossBitCount);;
					param.SetEncodeFloatParam(nameof(encodeParam.Center), encodeParam.Center);
					param.SetEncodeFloatParam(nameof(encodeParam.ExpRangeCenterOffset), encodeParam.ExpRangeCenterOffset);
					// Decode
					param.SetDecodeIntParam(nameof(decodeParam.ExponentBits), decodeParam.ExponentBits);
					param.SetDecodeIntParam(nameof(decodeParam.LossBitCount), decodeParam.LossBitCount);
					param.SetDecodeFloatParam(nameof(decodeParam.Offset), decodeParam.Offset);
					paramsArray[frameId][compoId] = param;
				}
			}
			return paramsArray;
		}
		// [component] 全フレーム共通の設定で圧縮した場合の設定
		public CompressionParams[] MakeCompressionParamsObject()
		{
			var encodeParams = EncodeParams[0];
			var decodeParams = DecodeParams[0];
			var componentCount = encodeParams.Length;
			var paramsArray = new CompressionParams[componentCount];
			for (var compoId = 0; compoId < componentCount; compoId++)
			{
				var encodeParam = encodeParams[compoId];
				var decodeParam = decodeParams[compoId];
				var param = ScriptableObject.CreateInstance<CompressionParams>();
				param.name = $"FrameAll_Component{compoId}";
				// Compression Type
				param.compressionType = CompressionType.FixedPrecisionFloat;
				// Encode
				param.SetEncodeIntParam(nameof(encodeParam.LossBitCount), encodeParam.LossBitCount);;
				param.SetEncodeFloatParam(nameof(encodeParam.Center), encodeParam.Center);
				param.SetEncodeFloatParam(nameof(encodeParam.ExpRangeCenterOffset), encodeParam.ExpRangeCenterOffset);
				// Decode
				param.SetDecodeIntParam(nameof(decodeParam.ExponentBits), decodeParam.ExponentBits);
				param.SetDecodeIntParam(nameof(decodeParam.LossBitCount), decodeParam.LossBitCount);
				param.SetDecodeFloatParam(nameof(decodeParam.Offset), decodeParam.Offset);
				paramsArray[compoId] = param;
			}
			return paramsArray;
		}
	}
}