using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Int3Sequence), menuName = "Attri/ScriptableObject/Int3 Sequence")]
	public class Int3Sequence : Sequence<Int3Container>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
	}
}