using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(ByteArray), menuName = "Attri/ScriptableObject/ByteArray")]
    public class ByteArray : ArrayScriptableObject<byte> { }
}
