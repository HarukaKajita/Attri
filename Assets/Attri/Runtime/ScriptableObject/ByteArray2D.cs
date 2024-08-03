using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(ByteArray2D), menuName = "Attri/ScriptableObject/ByteArray2D")]
    public class ByteArray2D : Array2DScriptableObject<byte> { }
}
