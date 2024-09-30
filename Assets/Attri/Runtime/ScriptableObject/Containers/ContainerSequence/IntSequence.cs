using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(IntSequence), menuName = "Attri/ScriptableObject/Int Sequence")]
	public class IntSequence : Sequence<IntContainer>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
	}
}