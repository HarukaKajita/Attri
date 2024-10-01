using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(IntSequence), menuName = "Attri/CSV/Sequence/Int", order = 0)]
	public class IntSequence : Sequence<IntContainer>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
	}
}