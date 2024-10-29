using System;
using System.Linq;
using Attri.Runtime.Extensions;
using Unity.Mathematics;

namespace Attri.Runtime
{
	public class FloatCompressor
	{
		public int Precision;
		public float[][] OriginalElements;// IDataProviderでもいいかも
		public float[][] OriginalComponents;
		public float[][] Compressed;

		// Decode Params [ComponentNum]
		public int[] exponentBitParams;
		public int[] lossBitParams;
		public float[] offsetParams;
		// public encoded
		public FloatCompressor(float[][] originalElements, int precision)
		{
			Precision = precision;
			OriginalElements = originalElements;
			// [エレメント][成分]を[成分][エレメント]に変換
			OriginalComponents = OriginalElements.ElementsToComponents(true);
		}
		
		public float[][] Compress(bool outputIsElements = false)
		{
			// 成分ごとに圧縮
			Compressed = new float[OriginalComponents.Length][];
			exponentBitParams = new int[OriginalComponents.Length];
			lossBitParams = new int[OriginalComponents.Length];
			offsetParams = new float[OriginalComponents.Length];
			for(var i = 0; i < Compressed.Length; i++)
			{
				var (compressedValues, exponentBit, lossBit, offset) = CompressComponents(OriginalComponents[i]);
				Compressed[i] = compressedValues;
				exponentBitParams[i] = exponentBit;
				lossBitParams[i] = lossBit;
				offsetParams[i] = offset;
			}
			
			if (outputIsElements)
			{
				// [成分][エレメント]を[エレメント][成分]に変換
				return Compressed.ComponentsToElements();
			}
			
			return Compressed;
		}
		
		private (float[] compressedValues, int exponentBit, int lossBitParams, float offsetParms) 
			CompressComponents(float[] componentValues)
		{
			var min = componentValues.Min();
			var max = componentValues.Max();
			var range = max - min;
			var center = (max + min) / 2;
			var compressed = componentValues.Select(v => CompressValue(v, center, range, Precision)).ToArray();
			var exp = (int)Math.Ceiling(Math.Log(range,2));//+127してからbitにする
			var expRangeCenterOffset = math.exp2(exp) * 1.5f;
			// Decode Uniform Params
			var E = (exp+127) << 23;
			var lossBits = 23 - Precision;
			var offset = -center + expRangeCenterOffset;
			return (compressed, E, lossBits, offset);
		}
		
		static float CompressValue(float value, float center, float range, int precision)
		{
			if(value is float.NaN) return float.NaN;
			
			// Encode Preparation
			var exp = (int)Math.Ceiling(Math.Log(range,2));
			var expRangeCenterOffset = math.exp2(exp) * 1.5f;
			
			// Decode Params
			var E = (exp+127) << 23;
			var lossBitLength = 23 - precision;
			float offset = expRangeCenterOffset - center;
			// Encode ベイクする際は下位ビットを切り捨てる
			var M = (int)(value-center+expRangeCenterOffset).AsUint() >> lossBitLength;
			// Decode
			var decoded = Decode(E, M, lossBitLength, offset);
			return decoded;
		}
		public static float Decode(int exp, int mantissa, int lossBitLength, float offset)
		{
			mantissa <<= lossBitLength;
			var preFloat = BitConverter.ToSingle(BitConverter.GetBytes(exp | mantissa), 0);
			return preFloat - offset;
		}
		public static float Decode(int exp, byte mantissa, int lossBitLength, float offset)
		{
			mantissa <<= lossBitLength;
			var preFloat = BitConverter.ToSingle(BitConverter.GetBytes(exp | mantissa), 0);
			return preFloat - offset;
		}
	}
}