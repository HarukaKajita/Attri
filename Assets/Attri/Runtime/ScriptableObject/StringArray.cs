using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(StringArray), menuName = "Attri/ScriptableObject/StringArray")]
    public class StringArray : ArrayScriptableObject<string> { }
}
