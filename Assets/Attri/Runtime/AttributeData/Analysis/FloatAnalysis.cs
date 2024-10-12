using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

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
		// // TODO: Floatに変換する
		// public float ToFloat()
		// {
		// 	var bits = (uint)(MinusSigned ? 1 << 31 : 0) | (uint)((Exponent + 127) << 23) | Mantissa;
		// 	return BitConverter.ToSingle(BitConverter.GetBytes(bits), 0);
		// }
	}
	
	// FloatElementの中の成分を解析する
	[Serializable]
	public struct FloatComponentAnalysisData
	{
		public readonly float[] values;
		public readonly float min;//最小値
		public readonly float max;//最大値
		public readonly float range;//範囲
		public readonly float sigma;//標準偏差
		
		public readonly FloatBitData[] bitData;
		public readonly bool signed;//符号
		public readonly int maxExponent;//最大指数
		public readonly int minExponent;//最小指数
		public readonly int exponentRange;//指数の範囲
		public readonly int exponentBitDepth;//指数のビット深度
		public FloatComponentAnalysisData(float[] values)
		{
			this.values = values;
			min = values.Min();
			max = values.Max();
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
		}
	}
	
	// FloatElement列の解析
	[Serializable]
	public struct FloatAnalysisData
	{
		public readonly float[][] Components;
		public FloatComponentAnalysisData[] componentsAnalysisData;

		public FloatAnalysisData(float[][] elements)
		{
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
			componentsAnalysisData = new FloatComponentAnalysisData[components.Length];
			for (var i = 0; i < components.Length; i++)
				componentsAnalysisData[i] = new FloatComponentAnalysisData(elements[i]);
			
			Components = components.Select(c => c.ToArray()).ToArray();
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
