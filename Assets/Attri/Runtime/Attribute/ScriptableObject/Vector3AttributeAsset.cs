using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Vector3AttributeAsset), menuName = "Attri/Attribute/Vector3", order = 0)]
    public class Vector3AttributeAsset : ScriptableObject
    {
        [SerializeField] private Vector3Attribute _attribute = new ();

        private void Reset()
        {
            _attribute = new Vector3Attribute();
        }
        [ContextMenu("SerializeTest")]
        private void SerializeTest()
        {
            _attribute.SerializeTest();
        }
        
        [ContextMenu("SerializeTest_Array")]
        private void SerializeTestArray()
        {
            var array = new Vector3Attribute[] {_attribute, _attribute};
            var bytes = AttributeSerializer.Serialize(array);
            var json = AttributeSerializer.ConvertToJson(bytes);
            Debug.Log(json);
        }
    }
}
