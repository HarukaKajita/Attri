using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine.Serialization;

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
		public float std;//標準偏差
		public float center;//中心
		
		public FloatBitData[] bitData;
		public bool signed;//符号
		public int maxExponent;//最大指数
		public int minExponent;//最小指数
		public int exponentRange;//指数の範囲
		public int exponentBitDepth;//指数のビット深度
		
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
			std = math.sqrt(variance / values.Length);
			
			bitData = values.Select(v => new FloatBitData(v)).ToArray();
			signed = bitData.Where(b=>b.Value!=0).Select(b => b.MinusSigned).Distinct().Count() > 1;
			maxExponent = bitData.Max(b => b.Exponent);
			minExponent = bitData.Min(b => b.Exponent);
			exponentRange = maxExponent - minExponent;
			exponentBitDepth = (int)math.ceil(math.log2(exponentRange + 1));
		}
	}
	
	[Serializable]
	public struct FloatFrameAnalysisData
	{
		public FloatComponentAnalysisData[] componentsAnalysisData;
		public FloatFrameAnalysisData(float[][] elements)
		{
			// [成分][エレメント]を[エレメント][成分]に変換
			var components = elements.Transpose();
			
			// 成分ごとに解析
			componentsAnalysisData = new FloatComponentAnalysisData[components.Length];
			for (var i = 0; i < components.Length; i++)
				componentsAnalysisData[i] = new FloatComponentAnalysisData(components[i].ToArray());
		}
	}
	
	// FloatElement列の解析
	[Serializable]
	public struct FloatAnalysisData
	{
		public FloatFrameAnalysisData[] frameAnalysisData;
		public FloatAnalysisData(float[][][] data)
		{
			var frameCount = data.Length;
			frameAnalysisData = new FloatFrameAnalysisData[frameCount];
			// フレームごとに解析
			for (var i = 0; i < frameCount; i++)
				frameAnalysisData[i] = new FloatFrameAnalysisData(data[i]);
		}
	}
}
