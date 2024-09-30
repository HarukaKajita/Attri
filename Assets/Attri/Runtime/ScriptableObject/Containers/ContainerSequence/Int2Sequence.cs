using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Int2Sequence), menuName = "Attri/ScriptableObject/Int2 Sequence")]
	public class Int2Sequence : Sequence<Int2Container>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
	}
}