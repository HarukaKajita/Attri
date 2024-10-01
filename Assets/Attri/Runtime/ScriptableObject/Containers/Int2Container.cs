using Unity.Mathematics;
using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Int2Container), menuName = "Attri/CSV/Int2", order = 201)]
	public class Int2Container : Container<int2> { }
}