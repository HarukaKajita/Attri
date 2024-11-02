using System.Linq;

namespace Attri.Runtime
{
	public static class ArrayExtension
	{
		public static int[][] Transpose(this int[][] data)
		{
			var maxComponentCount = data.Max(e => e.Length);
			var transposed = new int[maxComponentCount][];
			for (var componentId = 0; componentId < maxComponentCount; componentId++)
				transposed[componentId] = data.Where(e => e.Length > componentId).Select(e => e[componentId]).ToArray();
			return transposed;
		}
		
		public static float[][] Transpose(this float[][] data)
		{
			var maxComponentCount = data.Max(e => e.Length);
			var transposed = new float[maxComponentCount][];
			for (var componentId = 0; componentId < maxComponentCount; componentId++)
				transposed[componentId] = data.Where(e => e.Length > componentId).Select(e => e[componentId]).ToArray();
			return transposed;
		}

		public static float[][][] EmptyClone(this float[][][] data)
		{
			var clone = new float[data.Length][][];
			for (var frame = 0; frame < data.Length; frame++)
			{
				clone[frame] = new float[data[frame].Length][];
				for (var element = 0; element < data[frame].Length; element++)
				{
					clone[frame][element] = new float[data[frame][element].Length];
				}
			}
			return clone;
		}		
		
		public static int GetMaxComponentCount(this int[][][] data)
		{
			return data.Max(e=>e.Max(c=>c.Length));
		}
		public static int GetMaxComponentCount(this float[][][] data)
		{
			return data.Max(e=>e.Max(c=>c.Length));
		}
		public static int GetMaxComponentCount(this string[][][] data)
		{
			return data.Max(e=>e.Max(c=>c.Length));
		}
		// [frame][element][component] -> [frame][component][]
		public static int[] GetFrameComponent(this int[][][] data, int frame, int component)
		{
			var elements = data[frame];
			return elements.Where(e=>e.Length > component).Select(e=>e[component]).ToArray();
		}
        
		public static float[] GetFrameComponent(this float[][][] data, int frame, int component)
		{
			var elements = data[frame];
			return elements.Where(e=>e.Length > component).Select(e=>e[component]).ToArray();
		}
        
		public static string[] GetFrameComponent(this string[][][] data, int frame, int component)
		{
			var elements = data[frame];
			return elements.Where(e=>e.Length > component).Select(e=>e[component]).ToArray();
		}
	}
}