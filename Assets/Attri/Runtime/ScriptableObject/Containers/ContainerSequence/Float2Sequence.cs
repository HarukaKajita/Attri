using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Float2Sequence), menuName = "Attri/ScriptableObject/Float2 Sequence")]
	public class Float2Sequence : Sequence<Float2Container>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
	}
}