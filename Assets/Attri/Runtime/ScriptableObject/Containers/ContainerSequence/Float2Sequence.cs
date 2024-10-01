using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Float2Sequence), menuName = "Attri/CSV/Sequence/Float2", order = 5)]
	public class Float2Sequence : Sequence<Float2Container>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
	}
}