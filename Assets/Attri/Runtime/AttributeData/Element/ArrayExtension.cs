using System.Collections.Generic;
using System.Linq;

namespace Attri.Runtime
{
	public static class ArrayExtension
	{
		public static float[][] ElementsToComponents(this float[][] elements, bool fillWithNan = false)
		{
			// [エレメント][成分]を[成分][エレメント]に変換
			var maxComponentLength = elements.Max(e => e.Length);
			var components = new List<float>[maxComponentLength];
			foreach (var element in elements)
			{
				if (fillWithNan)
				{
					for (var compoIndex = 0; compoIndex < maxComponentLength; compoIndex++)
					{
						components[compoIndex] ??= new List<float>();
						components[compoIndex].Add(compoIndex < element.Length ? element[compoIndex] : float.NaN);
					}
				}
				else
				{
					for (var compoIndex = 0; compoIndex < element.Length; compoIndex++)
					{
						components[compoIndex] ??= new List<float>();
						components[compoIndex].Add(element[compoIndex]);
					}
				}
			}
			return components.Select(c => c.ToArray()).ToArray();
		}
		
		public static float[][] ComponentsToElements(this float[][] components)
		{
			// [成分][エレメント]を[エレメント][成分]に変換
			var maxElementLength = components.Max(c => c.Length);
			var elements = new List<float>[maxElementLength];
			foreach (var component in components)
			{
				for (var elementIndex = 0; elementIndex < component.Length; elementIndex++)
				{
					elements[elementIndex] ??= new List<float>();
					elements[elementIndex].Add(component[elementIndex]);
				}
			}
			return elements.Select(e => e.ToArray()).ToArray();
		}
		
		public static int[][] ElementsToComponents(this int[][] elements)
		{
			// [エレメント][成分]を[成分][エレメント]に変換
			var maxComponentLength = elements.Max(e => e.Length);
			var components = new List<int>[maxComponentLength];
			foreach (var element in elements)
			{
				for (var compoIndex = 0; compoIndex < element.Length; compoIndex++)
				{
					components[compoIndex] ??= new List<int>();
					components[compoIndex].Add(element[compoIndex]);
				}
			}
			return components.Select(c => c.ToArray()).ToArray();
		}
		// いらないかも
		public static string[][] ElementsToComponents(this string[][] elements)
		{
			// [エレメント][成分]を[成分][エレメント]に変換
			var maxComponentLength = elements.Max(e => e.Length);
			var components = new List<string>[maxComponentLength];
			foreach (var element in elements)
			{
				for (var compoIndex = 0; compoIndex < element.Length; compoIndex++)
				{
					components[compoIndex] ??= new List<string>();
					components[compoIndex].Add(element[compoIndex]);
				}
			}
			return components.Select(c => c.ToArray()).ToArray();
		}
	}
}