using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(IntArray2D), menuName = "Attri/ScriptableObject/IntArray2D")]
    public class IntArray2D : Array2DScriptableObject<int> { }
}
