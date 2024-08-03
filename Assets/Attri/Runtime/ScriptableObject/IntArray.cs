using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(IntArray), menuName = "Attri/ScriptableObject/IntArray")]
    public class IntArray : ArrayScriptableObject<int> { }
}
