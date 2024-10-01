using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(IntContainer), menuName = "Attri/CSV/Int", order = 200)]
	public class IntContainer : Container<int> { }
}