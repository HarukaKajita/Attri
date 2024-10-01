using Unity.Mathematics;
using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Int3Container), menuName = "Attri/CSV/Int3", order = 202)]
	public class Int3Container : Container<int3> { }
}