using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(StringContainer), menuName = "Attri/ScriptableObject/String")]
	public class StringContainer : Container<string> { }
}