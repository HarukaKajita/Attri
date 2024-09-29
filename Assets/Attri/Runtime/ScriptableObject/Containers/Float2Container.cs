using Unity.Mathematics;
using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Float2Container), menuName = "Attri/ScriptableObject/Float2")]
	public class Float2Container : Container<float2> { }
}