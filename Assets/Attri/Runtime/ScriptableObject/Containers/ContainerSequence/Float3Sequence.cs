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
	}
}