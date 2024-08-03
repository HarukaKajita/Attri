using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Vector3Array2D), menuName = "Attri/ScriptableObject/Vector3Array2D")]
    public class Vector3Array2D : Array2DScriptableObject<Vector3> { }
}
