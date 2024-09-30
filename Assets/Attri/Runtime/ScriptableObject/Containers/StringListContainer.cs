using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(StringListContainer), menuName = "Attri/ScriptableObject/String List")]
	public class StringListContainer : ListContainer<string> { }
}