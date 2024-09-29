using Unity.Mathematics;
using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Float3Container), menuName = "Attri/ScriptableObject/Float3")]
	public class Float3Container : Container<float3> { }
}