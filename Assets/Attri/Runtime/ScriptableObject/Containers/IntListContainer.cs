using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(IntListContainer), menuName = "Attri/ScriptableObject/Int List")]
	public class IntListContainer : ListContainer<int> { }
}