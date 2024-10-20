using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Attri.Runtime.Extensions;
using UnityEngine;
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
		[FormerlySerializedAs("sigma")] public float std;//標準偏差
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
	
	// FloatElement列の解析
	[Serializable]
	public struct FloatAnalysisData
	{
		public float[][] Elements;
		public float[][] Components;
		public FloatComponentAnalysisData[] componentsAnalysisData;
		public float[] degreeErrors;
		public float maxDegreeError;
		public float minDegreeError;
		public float averageDegreeError;
		public float sigmaDegreeError;
		public FloatAnalysisData(float[][] elements)
		{
			Elements = elements;
			// [エレメント][成分]を[成分][エレメント]に変換
			var components = elements.ElementsToComponents();
			
			// 成分ごとに解析
			componentsAnalysisData = new FloatComponentAnalysisData[components.Length];
			for (var i = 0; i < components.Length; i++)
				componentsAnalysisData[i] = new FloatComponentAnalysisData(components[i].ToArray());
			
			Components = components.Select(c => c.ToArray()).ToArray();
			degreeErrors = Array.Empty<float>();
			maxDegreeError = 0;
			minDegreeError = 0;
			averageDegreeError = 0;
			sigmaDegreeError = 0;
		}
		
		public FloatAnalysisData CompressedAsUnitVector(int precision)
		{
			var elements = CloneElements();
			List<float> degreeDiffList = new List<float>(elements.Length);
			foreach (var vectorElement in elements)
			{
				var originalVector = new float3(vectorElement[0], vectorElement[1], vectorElement[2]);
				originalVector = math.normalize(originalVector);
				var encodedVector = VectorCompressor.EncodeUnitVectorTo24bit(originalVector);
				var decodedVector = VectorCompressor.DecodeUnitVectorFrom24bit(encodedVector);
				vectorElement[0] = decodedVector.x;
				vectorElement[1] = decodedVector.y;
				vectorElement[2] = decodedVector.z;
				var radiansDiff = math.abs(math.acos(math.dot(originalVector, decodedVector)));
				degreeDiffList.Add(math.degrees(radiansDiff));
			}
			var newAnalysisData = new FloatAnalysisData(elements);
			newAnalysisData.degreeErrors = degreeDiffList.ToArray();
			newAnalysisData.maxDegreeError = degreeErrors.Max();
			newAnalysisData.minDegreeError = degreeErrors.Min();
			var aveDegreeError = degreeErrors.Average();
			newAnalysisData.averageDegreeError = aveDegreeError; 
			newAnalysisData.sigmaDegreeError = math.sqrt(degreeErrors.Select(e => math.pow(e-aveDegreeError, 2)).Average());
			
			return newAnalysisData;
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
