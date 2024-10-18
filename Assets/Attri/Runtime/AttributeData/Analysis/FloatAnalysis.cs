using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Attri.Runtime.Extensions;
using UnityEngine;

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
		public float[] degreeErrors;
		public float maxDegreeError;
		public float minDegreeError;
		public float averageDegreeError;
		public float sigmaDegreeError;
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
			degreeErrors = Array.Empty<float>();
			maxDegreeError = 0;
			minDegreeError = 0;
			averageDegreeError = 0;
			sigmaDegreeError = 0;
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

	static class VectorCompressor
	{
		private const int Resolution24BIT = 2046;
		private const double HalfPI = math.PI_DBL / 2;
		public static uint EncodeUnitVectorTo24bit(float3 vec)
		{
			uint compressedValue = 0;
			const double delta_phi = HalfPI / Resolution24BIT;
			// 先頭3bitに符号を格納
			if (vec.x < 0)
			{
				compressedValue |= 1 << 23;
				vec.x *= -1;
			}
			if (vec.y < 0)
			{
				compressedValue |= 1 << 22;
				vec.y *= -1;
			}
			if (vec.z < 0)
			{
				compressedValue |= 1 << 21;
				vec.z *= -1;
			}
			// ベクトルの向きを立体角に変換
			double2 thetaPhi = VectorToSolidAngle(vec);
			// 立体角を解像度で量子化
			uint i = (uint)Math.Round(thetaPhi.y / delta_phi);
			uint j = (uint)Math.Round(thetaPhi.x * i * 2 / math.PI);

			uint n = (i + 1) * i / 2 + j;
			compressedValue |= n;
			return compressedValue;
		}
		static double2 VectorToSolidAngle(double3 cartesian)
		{
			//work for vector -z
			double theta = math.atan2(cartesian.z, cartesian.x); //-pi~pi 
			//float theta = (float)Math.Atan(cartesian.z /cartesian.x);
			
			double phi = math.acos(cartesian.y); //0~pi
			return new double2(theta, phi);
		}
		public static float3 DecodeUnitVectorFrom24bit(uint encode)
		{
			uint n = encode & 0x1FFFFF;
			uint i = (uint)(math.sqrt(1 + 8 * n) - 1) / 2;
			uint j = n - (i + 1) * i / 2;

			double delta_phi = HalfPI / Resolution24BIT;
			double phi = i * delta_phi;
			double theta = i > 0 ? j * HalfPI / i : 0;

			double sinePhi = math.sin(phi);
			double3 normal = new double3(math.cos(theta) * sinePhi, math.cos(phi), math.sin(theta) * sinePhi);

			if ((encode & 0x800000) != 0) normal.x *= -1;
			if ((encode & 0x400000) != 0) normal.y *= -1;
			if ((encode & 0x200000) != 0) normal.z *= -1;

			// if(DEBUG_CLUSTERING_NORMAL_COLOR_OUTPUT)
			// 	return normalize(float3(i%2==0, j%2==0, n%2==0));
			return (float3)math.normalize(normal);
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
