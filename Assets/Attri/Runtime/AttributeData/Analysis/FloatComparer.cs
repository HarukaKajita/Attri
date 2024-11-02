using System.Linq;
using Unity.Mathematics;

namespace Attri.Runtime
{
	public class FloatComparer
	{
		// [frame][element][component]
		public float[][][] diff;
		// [frame][component]
		public float[][] diffMax;
		public float[][] diffMin;
		public float[][] diffMid;
		public float[][] diffAve;
		public float[][] diffStd;
		public float[][] diffRange;
		public FloatComparer(float[][][] originalData, float[][][] compressedData)
		{
			if (originalData == null) throw new System.ArgumentNullException(nameof(originalData));
			if (compressedData == null) throw new System.ArgumentNullException(nameof(compressedData));
			// 両方の配列のフレーム数が同じかどうか
			if (originalData.Length != compressedData.Length)
				throw new System.ArgumentException($"The number of elements in the original and compressed arrays are different. Original:{originalData.Length} != Compressed:{compressedData.Length}");

			var frameCount = originalData.Length;
			diff = new float[frameCount][][];
			// フレームループ
			for (var frameId = 0; frameId < frameCount; frameId++)
			{
				// 両方の配列の要素数が同じかどうか
				if (originalData[frameId].Length != compressedData[frameId].Length)
					throw new System.ArgumentException("The ElementCount in the original and compressed arrays are different.");
				
				var elementCount = originalData[frameId].Length;
				diff[frameId] = new float[elementCount][];
				// 要素ループ
				for (var elementId = 0; elementId < elementCount; elementId++)
				{
					// 両方の配列の成分数が同じかどうか
					if (originalData[frameId][elementId].Length != compressedData[frameId][elementId].Length)
						throw new System.ArgumentException($"The ComponentCount in the original and compressed arrays are different. Original:{originalData[frameId][elementId].Length} != Compressed:{compressedData[frameId][elementId].Length}");
				
					var componentCount = originalData[frameId][elementId].Length;
					diff[frameId][elementId] = new float[componentCount];
					// 成分ループ
					for (var componentId = 0; componentId < componentCount; componentId++)
					{
						// 元の値に対して圧縮した値との差を計算する
						var originalValue = originalData[frameId][elementId][componentId];
						var compressedValue = compressedData[frameId][elementId][componentId];
						diff[frameId][elementId][componentId] = math.abs(originalValue - compressedValue);
					}
				}
			}
			
			
			// 各成分ごとに最大値、最小値、値域の中央、標準偏差を計算する
			diffMax = new float[frameCount][];
			diffMin = new float[frameCount][];
			diffMid = new float[frameCount][];
			diffAve = new float[frameCount][];
			diffStd = new float[frameCount][];
			diffRange = new float[frameCount][];
			var maxComponentCount = diff.GetMaxComponentCount();
			// フレームごとに計算
			for (var frameId = 0; frameId < frameCount; frameId++)
			{
				diffMax[frameId] = new float[maxComponentCount];
				diffMin[frameId] = new float[maxComponentCount];
				diffMid[frameId] = new float[maxComponentCount];
				diffAve[frameId] = new float[maxComponentCount];
				diffStd[frameId] = new float[maxComponentCount];
				diffRange[frameId] = new float[maxComponentCount];
				for (var componentId = 0; componentId < maxComponentCount; componentId++)
				{
					var d = diff.GetFrameComponent(frameId, componentId);
					diffMax[frameId][componentId] = d.Max();
					diffMin[frameId][componentId] = d.Min();
					diffMid[frameId][componentId] = (diffMax[frameId][componentId] + diffMin[frameId][componentId]) / 2;
					diffAve[frameId][componentId] = d.Average();
					var diffVariances = d.Select(v => math.pow(v - diffAve[frameId][componentId], 2)).ToArray();
					diffStd[frameId][componentId] = math.sqrt(diffVariances.Sum() / d.Length);
					diffRange[frameId][componentId] = diffMax[frameId][componentId] - diffMin[frameId][componentId];
				}
			}
		}
	}
}