using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Int2Sequence), menuName = "Attri/CSV/Sequence/Int2", order = 1)]
	public class Int2Sequence : Sequence<Int2Container>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
	}
}