using Unity.Mathematics;
using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Int3Container), menuName = "Attri/ScriptableObject/Int3")]
	public class Int3Container : Container<int3> { }
}