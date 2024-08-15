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
    }
}
