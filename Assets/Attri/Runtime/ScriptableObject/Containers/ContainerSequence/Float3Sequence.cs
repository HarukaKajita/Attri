using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Float3Sequence), menuName = "Attri/CSV/Sequence/Float3", order = 6)]
	public class Float3Sequence : Sequence<Float3Container>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
		
		public float3[] GetValues()
		{
			var valuesCount = containers.Select(container => container.values.Count).Sum();
			var values = new List<float3>(valuesCount);
			foreach (var container in containers)
			{
				if (container == null) continue;
				values.AddRange(container.values);
			}
			return values.ToArray();
		}
	}
}