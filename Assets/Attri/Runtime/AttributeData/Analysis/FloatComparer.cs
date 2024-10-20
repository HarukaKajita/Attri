using System.Linq;
using Unity.Mathematics;

namespace Attri.Runtime
{
	public class FloatComparer
	{
		float[][] originalComponents;
		float[][] compressedComponents;
		public float[][] diffComponents;
		public float[] diffMax;
		public float[] diffMin;
		public float[] diffMid;
		public float[] diffAve;
		public float[] diffStd;
		public float[] diffRange;
		public FloatComparer(float[][] originalComponents, float[][] compressedComponents)
		{
			this.originalComponents = originalComponents;
			this.compressedComponents = compressedComponents;
			
			if (originalComponents == null)
				throw new System.ArgumentNullException(nameof(originalComponents));
			if (compressedComponents == null)
				throw new System.ArgumentNullException(nameof(compressedComponents));
			// 両方の配列の要素数が同じかどうか
			if (originalComponents.Length != compressedComponents.Length)
				throw new System.ArgumentException($"The number of elements in the original and compressed arrays are different. originalComponents:{originalComponents.Length} != compressedComponents:{compressedComponents.Length}");
			
			diffComponents = new float[originalComponents.Length][];
			// 元の値に対して圧縮した値との差を計算する
			for (var componentId = 0; componentId < originalComponents.Length; componentId++)
			{
				// 両方の配列の要素数が同じかどうか
				if (originalComponents[componentId].Length != compressedComponents[componentId].Length)
					throw new System.ArgumentException("The number of components in the original and compressed arrays are different.");
				
				var elementLength = originalComponents[componentId].Length;
				diffComponents[componentId] = new float[elementLength];
				// 元の値に対して圧縮した値との差を計算する
				for (var elementId = 0; elementId < elementLength; elementId++)
				{
					var originalValue = originalComponents[componentId][elementId];
					var compressedValue = compressedComponents[componentId][elementId];
					var diff = originalValue - compressedValue;
					diffComponents[componentId][elementId] = math.abs(diff);
				}
			}
			
			// 各成分ごとに最大値、最小値、値域の中央、標準偏差を計算する
			diffMax = diffComponents.Select(x => x.Max()).ToArray();
			diffMin = diffComponents.Select(x => x.Min()).ToArray();
			diffRange = diffMax.Zip(diffMin, (max, min) => max - min).ToArray();
			diffMid = diffMax.Zip(diffMin, (max, min) => (max + min) / 2).ToArray();
			diffAve = diffComponents.Select(x => x.Average()).ToArray();
			var diffVariances = diffComponents.Zip(diffAve, (x, ave) => x.Sum(v => math.pow(v - ave, 2))).ToArray();
			diffStd = diffVariances.Select(v => math.sqrt(v / diffComponents[0].Length)).ToArray();
		}
	}
}