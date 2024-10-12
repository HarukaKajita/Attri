using System;
using System.Linq;
using Unity.Mathematics;

namespace Attri.Runtime
{
	[Serializable]
	public struct IntBitData
	{
		public readonly int Value;
		public readonly bool MinusSigned;
		public readonly int UnsignedValue;
		public readonly int MaxBit;

		public IntBitData(int value)
		{
			Value = value;
			MinusSigned = value >> 31 != 0;
			// 符号なしの値
			UnsignedValue = value & 0x7fffffff;
			
			// 最大ビット数
			// 何桁必要か調べる
			var position = 0;
			while (value != 0)
			{
				value >>= 1;
				position++;
			}
			MaxBit = position - 1;
		}
	}

	// IntElementの中の成分を解析する
	[Serializable]
	public struct IntComponentAnalysisData
	{
		public readonly int[] values;
		public readonly int min;
		public readonly int max;
		public readonly int range;
		public readonly float sigma;
		
		public readonly IntBitData[] bitData;
		public readonly bool signed;
		public readonly int maxBit;
		public IntComponentAnalysisData(int[] values)
		{
			this.values = values;
			min = values.Min();
			max = values.Max();
			range = max - min;
			var average = values.Average();
			var variance = values.Sum(v => math.pow(v - average, 2));
			sigma = (float)math.sqrt(variance / values.Length);
			
			bitData = values.Select(v => new IntBitData(v)).ToArray();
			signed = bitData.Where(b=>b.MaxBit!=0).Select(b => b.MinusSigned).Distinct().Count() > 1;
			maxBit = bitData.Max(b => b.MaxBit);
		}
	}
	// IntElement列の解析
	[Serializable]
	public struct IntAnalysisData
	{
		public readonly int[][] components;
		public readonly IntComponentAnalysisData[] componentsAnalysisData;

		public IntAnalysisData(int[][] components)
		{
			this.components = components;
			var componentCount = components.Length;
			componentsAnalysisData = new IntComponentAnalysisData[componentCount];
			for (var i = 0; i < componentCount; i++)
				componentsAnalysisData[i] = new IntComponentAnalysisData(components[i]);
		}
	}
}