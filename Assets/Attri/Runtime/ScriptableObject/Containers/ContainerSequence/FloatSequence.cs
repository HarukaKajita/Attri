using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(FloatSequence), menuName = "Attri/CSV/Sequence/Float", order = 4)]
	public class FloatSequence : Sequence<FloatContainer>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
	}
}