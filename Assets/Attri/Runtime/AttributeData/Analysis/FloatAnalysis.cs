using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Attri.Runtime.Extensions;
	
namespace Attri.Runtime
{
	[Serializable]
	public struct FloatBitData
	{
		public readonly float Value;
		public readonly bool MinusSigned;
		public readonly int Exponent;
		public readonly uint Mantissa;

		public FloatBitData(float value)
		{
			Value = value;
			var bits = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
			MinusSigned = bits >> 31 != 0;
			Exponent = (int)((bits >> 23) & 0xff)-127;
			Mantissa = bits & 0x7fffff;
		}
	}
	
	// FloatElementの中の成分を解析する
	[Serializable]
	public struct FloatComponentAnalysisData
	{
		public float[] values;
		public int elementNum;
		public float min;//最小値
		public float max;//最大値
		public float range;//範囲
		public float sigma;//標準偏差
		public float center;//中心
		
		public FloatBitData[] bitData;
		public bool signed;//符号
		public int maxExponent;//最大指数
		public int minExponent;//最小指数
		public int exponentRange;//指数の範囲
		public int exponentBitDepth;//指数のビット深度
		
		//圧縮後の値
		public float[] error;//誤差
		public float errorMin;
		public float errorMax;
		public float errorAverage;
		public float errorSigma;
		public int E;
		public float offset;
		public FloatComponentAnalysisData(float[] values)
		{
			this.values = values;
			elementNum = values.Length;
			min = values.Min();
			max = values.Max();
			center = (min + max) / 2f;
			range = max - min;
			var average = values.Average();
			var variance = values.Sum(v => math.pow(v - average, 2));
			sigma = math.sqrt(variance / values.Length);
			
			bitData = values.Select(v => new FloatBitData(v)).ToArray();
			signed = bitData.Where(b=>b.Value!=0).Select(b => b.MinusSigned).Distinct().Count() > 1;
			maxExponent = bitData.Max(b => b.Exponent);
			minExponent = bitData.Min(b => b.Exponent);
			exponentRange = maxExponent - minExponent;
			exponentBitDepth = (int)math.ceil(math.log2(exponentRange + 1));
			
			error = Array.Empty<float>();
			errorMin = 0;
			errorMax = 0;
			errorAverage = 0;
			errorSigma = 0;
			E = 0;
			offset = 0;
		}

		public FloatComponentAnalysisData Compress(int precision)
		{
			float c = center, r = range;
			var compressed = values.Select(v => CompressValue(v, c, r, precision)).ToArray();
			var errorArray = values.Zip(compressed, (v, e) =>  math.abs(e-v)).ToArray();
			var errorAve = errorArray.Average();
			var errorSig = math.sqrt(errorArray.Select(e => math.pow(e-errorAve, 2)).Average());
			var exp = (int)Math.Ceiling(Math.Log(range,2));
			var expRangeCenterOffset = math.exp2(exp) * 1.5f;
			var newComponentAnalysisData = new FloatComponentAnalysisData(compressed)
			{
				error = errorArray,
				errorMin = errorArray.Min(),
				errorMax = errorArray.Max(),	
				errorAverage = errorAve,
				errorSigma = errorSig,
				E = exp,//(exp+127) << 23,
				offset = -center + expRangeCenterOffset
			};
			
			return newComponentAnalysisData;
		}
		
		static float CompressValue(float value, float center, float range, int precision)
		{
			// Encode
			var exp = (int)Math.Ceiling(Math.Log(range,2));
			var expRangeCenterOffset = math.exp2(exp) * 1.5f;
			var E = (exp+127) << 23;
			var ignoreBit = 23 - precision;
			var mantissaAsInt = (int)(value-center+expRangeCenterOffset).AsUint();
			// ベイクする際は下位ビットを切り捨てた状態でベイク
			mantissaAsInt = (mantissaAsInt >> ignoreBit) << ignoreBit;
			// Decode
			return ComposeFloat(E, mantissaAsInt)-expRangeCenterOffset+center;
		}
		static float ComposeFloat(int exp, int mantissa)
		{
			return BitConverter.ToSingle(BitConverter.GetBytes(exp | mantissa), 0);
		}
	}
	
	// FloatElement列の解析
	[Serializable]
	public struct FloatAnalysisData
	{
		public float[][] Elements;
		public float[][] Components;
		public FloatComponentAnalysisData[] componentsAnalysisData;

		public FloatAnalysisData(float[][] elements)
		{
			Elements = elements;
			// [エレメント][成分]を[成分][エレメント]に変換
			var maxComponentLength = elements.Max(e => e.Length);
			var components = new List<float>[maxComponentLength];
			foreach (var element in elements)
			{
				for (var compoIndex = 0; compoIndex < element.Length; compoIndex++)
				{
					components[compoIndex] ??= new List<float>();
					components[compoIndex].Add(element[compoIndex]);
				}
			}
			
			// 成分ごとに解析
			componentsAnalysisData = new FloatComponentAnalysisData[components.Length];
			for (var i = 0; i < components.Length; i++)
				componentsAnalysisData[i] = new FloatComponentAnalysisData(components[i].ToArray());
			
			Components = components.Select(c => c.ToArray()).ToArray();
		}

		public FloatAnalysisData Compressed(int precision)
		{
			var copy = new FloatAnalysisData
			{
				Components = CloneComponents(),
				Elements = CloneElements(),
				componentsAnalysisData = new FloatComponentAnalysisData[componentsAnalysisData.Length]
			};
			for (var compId = 0; compId < copy.Components.Length; compId++)
			{
				var componentAnalysisData = componentsAnalysisData[compId];
				var compressedComponentAnalysisData = componentAnalysisData.Compress(precision);
				copy.componentsAnalysisData[compId] = compressedComponentAnalysisData;
			}
			return copy;
		}


		float[][] CloneElements()
		{
			var copy = Elements.Select(e =>
			{
				var c = new float[e.Length];
				e.CopyTo(c,0);
				return c;
			}).ToArray();
			return copy;
		}
		
		float[][] CloneComponents()
		{
			var copy = Components.Select(comp =>
			{
				var c = new float[comp.Length];
				comp.CopyTo(c,0);
				return c;
			}).ToArray();
			return copy;
		}
	}
	
	// 成分が方向ベクトルである場合の解析データ
	public struct DirectionAnalysisData
	{
		public readonly float[][] values;
		public readonly float min;
		public readonly float max;
		public readonly float range;
		public readonly float sigma;
	}
	
}
