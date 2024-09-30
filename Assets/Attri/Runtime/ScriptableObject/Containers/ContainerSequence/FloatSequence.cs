using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(FloatSequence), menuName = "Attri/ScriptableObject/Float Sequence")]
	public class FloatSequence : Sequence<FloatContainer>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
	}
}