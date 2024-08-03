using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Vector3Array), menuName = "Attri/ScriptableObject/Vector3Array")]
    public class Vector3Array : ArrayScriptableObject<Vector3> { }
}
