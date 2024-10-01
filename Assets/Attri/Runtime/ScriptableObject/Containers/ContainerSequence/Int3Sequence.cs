using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Int3Sequence), menuName = "Attri/CSV/Sequence/Int3", order = 2)]
	public class Int3Sequence : Sequence<Int3Container>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
	}
}