using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = "CompressionParams", menuName = "Attri/CompressionParams", order = 0)]
    public class CompressionParams : ScriptableObject
    {
        // [Header("Compression Type")]
        // [SerializeField,DisableOnInspector] internal CompressionType compressionType = CompressionType.UnCompressed;
        
        [Space(18)]
        [Header("Encode Params")]
        [SerializeField,DisableOnInspector] internal SerializedDictionary<string, int> encodeIntParams = new();
        [SerializeField,DisableOnInspector] internal SerializedDictionary<string, float> encodeFloatParams = new();
        
        [Space(18)]
        [Header("Decode Params")]
        [SerializeField,DisableOnInspector] internal SerializedDictionary<string, int> decodeIntParams = new();
        [SerializeField,DisableOnInspector] internal SerializedDictionary<string, float> decodeFloatParams = new();
        
        internal void SetEncodeIntParam(string key, int value)
        {
            if(!encodeIntParams.TryAdd(key, value)) encodeIntParams[key] = value;
        }
        internal void SetEncodeFloatParam(string key, float value)
        {
            if(!encodeFloatParams.TryAdd(key, value)) encodeFloatParams[key] = value;
        }
        internal void SetDecodeIntParam(string key, int value)
        {
            if(!decodeIntParams.TryAdd(key, value)) decodeIntParams[key] = value;
        }
        internal void SetDecodeFloatParam(string key, float value)
        {
            if(!decodeFloatParams.TryAdd(key, value)) decodeFloatParams[key] = value;
        }
    }
}
