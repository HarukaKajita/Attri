using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = "CompressionParams", menuName = "Attri/CompressionParams", order = 0)]
    public class CompressionParams : ScriptableObject
    {
        [Header("Compression Type")]
        [SerializeField,DisableOnInspector] internal CompressionType compressionType = CompressionType.UnCompressed;
        
        [Space(18)]
        [Header("Encode Params")]
        [SerializeField,DisableOnInspector] internal int precision = 23;
        [SerializeField,DisableOnInspector] internal SerializedDictionary<string, int> encodeIntParams = new();
        [SerializeField,DisableOnInspector] internal SerializedDictionary<string, float> encodeFloatParams = new();
        
        [Space(18)]
        [Header("Decode Params")]
        [SerializeField,DisableOnInspector] internal SerializedDictionary<string, int> decodeIntParams = new();
        [SerializeField,DisableOnInspector] internal SerializedDictionary<string, float> decodeFloatParams = new();
    }
}
