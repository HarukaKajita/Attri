using UnityEngine;

namespace Attri.Runtime
{
	[CreateAssetMenu(fileName = nameof(FloatArrayContainer), menuName = "Attri/ScriptableObject/FloatArray")]
	public class FloatArrayContainer : ArrayContainer<float> { }
}