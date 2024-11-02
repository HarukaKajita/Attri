using System.Linq;
using Unity.Mathematics;

namespace Attri.Runtime
{
	public class DirectionComparer
	{
		// [frame][element]
		public float[][] DiffDegrees;
		public float[] DiffMax;
		public float[] DiffMin;
		public float[] DiffAve;
		public float[] DiffStd;
		public float[] DiffRange;
		public DirectionComparer(float3[][] originalVectors, float3[][] compressedVectors)
		{
			if (originalVectors == null)
				throw new System.ArgumentNullException(nameof(originalVectors));
			if (compressedVectors == null)
				throw new System.ArgumentNullException(nameof(compressedVectors));
			// 両方の配列の要素数が同じかどうか
			if (originalVectors.Length != compressedVectors.Length)
				throw new System.ArgumentException($"The number of elements in the original and compressed arrays are different. originalComponents:{originalVectors.Length} != compressedComponents:{compressedVectors.Length}");

			originalVectors = originalVectors.Select(vectors => vectors.Select(math.normalize).ToArray()).ToArray();
			compressedVectors = compressedVectors.Select(vectors => vectors.Select(math.normalize).ToArray()).ToArray();
			
			
			// 元の値に対して圧縮した値との差を計算する
			DiffDegrees = new float[originalVectors.Length][];
			for (var frame = 0; frame < originalVectors.Length; frame++)
			{
				DiffDegrees[frame] = new float[originalVectors[frame].Length];
				for (var element = 0; element < originalVectors[frame].Length; element++)
				{
					// 元の値に対して圧縮した値との差を計算する
					var v0 = originalVectors[frame][element];
					var v1 = compressedVectors[frame][element];
					var diffRadian = math.acos(math.clamp(math.dot(v0,v1), -1.0, 1.0));
					DiffDegrees[frame][element] = (float)math.degrees(diffRadian);
				}
			}
			
			// 最大値、最小値、値域の幅、標準偏差を計算する
			DiffMax = DiffDegrees.Select(e=>e.Max()).ToArray();
			DiffMin = DiffDegrees.Select(e=>e.Min()).ToArray();
			DiffRange = DiffMax.Zip(DiffMin, (max, min) => max - min).ToArray();
			DiffAve = DiffDegrees.Select(e=>e.Average()).ToArray();
			var frameCount = DiffDegrees.Length;
			DiffStd = new float[frameCount];
			for (var frame = 0; frame < frameCount; frame++)
			{
				double sum = 0;
				for (var element = 0; element < DiffDegrees[frame].Length; element++)
					sum += math.pow(DiffDegrees[frame][element] - DiffAve[frame], 2);
				DiffStd[frame] = (float)(sum / DiffDegrees[frame].Length);
			}
		}
	}
}
