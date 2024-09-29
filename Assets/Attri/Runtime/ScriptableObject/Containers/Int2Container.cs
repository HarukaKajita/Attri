using Unity.Mathematics;
using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Int2Container), menuName = "Attri/ScriptableObject/Int2")]
	public class Int2Container : Container<int2> { }
}