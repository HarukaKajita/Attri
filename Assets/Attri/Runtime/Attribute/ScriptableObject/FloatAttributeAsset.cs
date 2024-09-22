using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(FloatAttributeAsset), menuName = "Attri/Attribute/Float", order = 0)]
    public class FloatAttributeAsset : AttributeAsset
    {
        public FloatAttribute attribute = new ();
        private void Reset()
        {
            attribute = new FloatAttribute();
        }
        public override IAttribute GetAttribute()
        {
            return attribute;
        }

        [ContextMenu("SerializeTest_Array")]
        private void SerializeTestArray()
        {
            var array = new FloatAttribute[] {attribute, attribute};
            var bytes = AttributeSerializer.Serialize(array);
            var json = AttributeSerializer.ConvertToJson(bytes);
            Debug.Log(json);
        }
    }
}
