using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Attri.Runtime
{
	public class DirectionComparer
	{
		float3[] originalVectors;
		float3[] compressedVectors;
		public float[] DiffDegrees;
		public float DiffMax;
		public float DiffMin;
		public float DiffAve;
		public float DiffStd;
		public float DiffRange;
		public DirectionComparer(float3[] originalVectors, float3[] compressedVectors)
		{
			if (originalVectors == null)
				throw new System.ArgumentNullException(nameof(originalVectors));
			if (compressedVectors == null)
				throw new System.ArgumentNullException(nameof(compressedVectors));
			// 両方の配列の要素数が同じかどうか
			if (originalVectors.Length != compressedVectors.Length)
				throw new System.ArgumentException($"The number of elements in the original and compressed arrays are different. originalComponents:{originalVectors.Length} != compressedComponents:{compressedVectors.Length}");
			
			this.originalVectors = originalVectors.Select(math.normalize).ToArray();
			this.compressedVectors = compressedVectors.Select(math.normalize).ToArray();
			
			// 元の値に対して圧縮した値との差を計算する
			DiffDegrees = new float[this.originalVectors.Length];
			for (var vecId = 0; vecId < this.originalVectors.Length; vecId++)
			{
				// 元の値に対して圧縮した値との差を計算する
				var v0 = (double3)this.originalVectors[vecId];
				var v1 = (double3)this.compressedVectors[vecId];
				var diffRadian = math.acos(math.clamp(math.dot(v0,v1), -1.0, 1.0));
				DiffDegrees[vecId] = (float)math.degrees(diffRadian);
			}
			
			// 最大値、最小値、値域の幅、標準偏差を計算する
			DiffMax = DiffDegrees.Max();
			DiffMin = DiffDegrees.Min();
			DiffRange = DiffMax - DiffMin;
			DiffAve = DiffDegrees.Average();
			var diffVariances = DiffDegrees.Select(v => math.pow(v - DiffAve, 2)).ToArray();
			DiffStd = math.sqrt(diffVariances.Sum() / DiffDegrees.Length);
		}
	}
}
