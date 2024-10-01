using Unity.Mathematics;
using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(Float3Container), menuName = "Attri/CSV/Float3", order = 206)]
	public class Float3Container : Container<float3> { }
}