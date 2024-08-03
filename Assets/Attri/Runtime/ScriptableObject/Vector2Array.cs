using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Vector2Array), menuName = "Attri/ScriptableObject/Vector2Array")]
    public class Vector2Array : ArrayScriptableObject<Vector2> { }
}
