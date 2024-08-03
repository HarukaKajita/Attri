using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(StringArray2D), menuName = "Attri/ScriptableObject/StringArray2D")]
    public class StringArray2D : ArrayScriptableObject<string> { }
}
