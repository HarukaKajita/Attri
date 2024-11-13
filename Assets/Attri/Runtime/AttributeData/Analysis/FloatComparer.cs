using System.Linq;
using Unity.Mathematics;

namespace Attri.Runtime
{
	public class FloatComparer
	{
		// [frame][element][component]
		public readonly float[][][] Diff;
		// [frame][component]
		public readonly float[][] DiffMax;
		public readonly float[][] DiffMin;
		public readonly float[][] DiffMid;
		public readonly float[][] DiffAve;
		public readonly float[][] DiffStd;
		public readonly float[][] DiffRange;
		
		// [element][component] 全フレーム間での値
		public readonly float[][] DiffAcrossAllFrame;
		// [component] 全フレーム間での値
		public float[] DiffMaxAcrossAllFrame;
		public float[] DiffMinAcrossAllFrame;
		public float[] DiffMidAcrossAllFrame;
		public float[] DiffAveAcrossAllFrame;
		public float[] DiffStdAcrossAllFrame;
		public float[] DiffRangeAcrossAllFrame;
		
		public FloatComparer(float[][][] originalData, float[][][] compressedData)
		{
			if (originalData == null) throw new System.ArgumentNullException(nameof(originalData));
			if (compressedData == null) throw new System.ArgumentNullException(nameof(compressedData));
			// 両方の配列のフレーム数が同じかどうか
			if (originalData.Length != compressedData.Length)
				throw new System.ArgumentException($"The number of elements in the original and compressed arrays are different. Original:{originalData.Length} != Compressed:{compressedData.Length}");

			var frameCount = originalData.Length;
			Diff = new float[frameCount][][];
			// フレームループ
			for (var frameId = 0; frameId < frameCount; frameId++)
			{
				// 両方の配列の要素数が同じかどうか
				if (originalData[frameId].Length != compressedData[frameId].Length)
					throw new System.ArgumentException("The ElementCount in the original and compressed arrays are different.");
				
				var elementCount = originalData[frameId].Length;
				Diff[frameId] = new float[elementCount][];
				// 要素ループ
				for (var elementId = 0; elementId < elementCount; elementId++)
				{
					// 両方の配列の成分数が同じかどうか
					if (originalData[frameId][elementId].Length != compressedData[frameId][elementId].Length)
						throw new System.ArgumentException($"The ComponentCount in the original and compressed arrays are different. Original:{originalData[frameId][elementId].Length} != Compressed:{compressedData[frameId][elementId].Length}");
				
					var componentCount = originalData[frameId][elementId].Length;
					Diff[frameId][elementId] = new float[componentCount];
					// 成分ループ
					for (var componentId = 0; componentId < componentCount; componentId++)
					{
						// 元の値に対して圧縮した値との差を計算する
						var originalValue = originalData[frameId][elementId][componentId];
						var compressedValue = compressedData[frameId][elementId][componentId];
						Diff[frameId][elementId][componentId] = math.abs(originalValue - compressedValue);
					}
				}
			}
			
			
			// 各成分ごとに最大値、最小値、値域の中央、標準偏差を計算する
			DiffMax = new float[frameCount][];
			DiffMin = new float[frameCount][];
			DiffMid = new float[frameCount][];
			DiffAve = new float[frameCount][];
			DiffStd = new float[frameCount][];
			DiffRange = new float[frameCount][];
			var maxComponentCount = Diff.GetMaxComponentCount();
			// フレームごとに計算
			float[] d;
			for (var frameId = 0; frameId < frameCount; frameId++)
			{
				DiffMax[frameId] = new float[maxComponentCount];
				DiffMin[frameId] = new float[maxComponentCount];
				DiffMid[frameId] = new float[maxComponentCount];
				DiffAve[frameId] = new float[maxComponentCount];
				DiffStd[frameId] = new float[maxComponentCount];
				DiffRange[frameId] = new float[maxComponentCount];
				for (var componentId = 0; componentId < maxComponentCount; componentId++)
				{
					d = Diff.GetFrameComponent(frameId, componentId);
					DiffMax[frameId][componentId] = d.Max();
					DiffMin[frameId][componentId] = d.Min();
					DiffMid[frameId][componentId] = (DiffMax[frameId][componentId] + DiffMin[frameId][componentId]) / 2;
					DiffAve[frameId][componentId] = d.Average();
					var diffVariances = d.Select(v => math.pow(v - DiffAve[frameId][componentId], 2)).ToArray();
					DiffStd[frameId][componentId] = math.sqrt(diffVariances.Sum() / d.Length);
					DiffRange[frameId][componentId] = DiffMax[frameId][componentId] - DiffMin[frameId][componentId];
				}
			}
			
			// 全フレーム間での値を計算する
			DiffAcrossAllFrame = Diff.SelectMany(f => f).ToArray();
			DiffMaxAcrossAllFrame = new float[maxComponentCount];
			DiffMinAcrossAllFrame = new float[maxComponentCount];
			DiffMidAcrossAllFrame = new float[maxComponentCount];
			DiffAveAcrossAllFrame = new float[maxComponentCount];
			DiffStdAcrossAllFrame = new float[maxComponentCount];
			DiffRangeAcrossAllFrame = new float[maxComponentCount];
			// [component] 全フレーム間での値
			d = DiffAcrossAllFrame.Transpose().SelectMany(e => e).ToArray();
			for (var componentId = 0; componentId < maxComponentCount; componentId++)
			{
				DiffMaxAcrossAllFrame[componentId] = d.Max();
				DiffMinAcrossAllFrame[componentId] = d.Min();
				DiffMidAcrossAllFrame[componentId] = (DiffMaxAcrossAllFrame[componentId] + DiffMinAcrossAllFrame[componentId]) / 2;
				DiffAveAcrossAllFrame[componentId] = d.Average();
				var diffVariances = d.Select(v => math.pow(v - DiffAveAcrossAllFrame[componentId], 2)).ToArray();
				DiffStdAcrossAllFrame[componentId] = math.sqrt(diffVariances.Sum() / d.Length);
				DiffRangeAcrossAllFrame[componentId] = DiffMaxAcrossAllFrame[componentId] - DiffMinAcrossAllFrame[componentId];
			}
		}
	}
}