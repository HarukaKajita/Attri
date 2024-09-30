using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Float3Sequence), menuName = "Attri/ScriptableObject/Float3 Sequence")]
	public class Float3Sequence : Sequence<Float3Container>
	{
		[ContextMenu("Gather Container")]
		public void CallGatherContainer()
		{
			GatherContainer();
		}
	}
}