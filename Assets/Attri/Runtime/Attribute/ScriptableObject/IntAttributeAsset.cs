using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(IntAttributeAsset), menuName = "Attri/Attribute/Int", order = 0)]
    public class IntAttributeAsset : AttributeAsset
    {
        public IntAttribute attribute = new ();
        private void Reset()
        {
            attribute = new IntAttribute();
        }
        public override IAttribute GetAttribute()
        {
            return attribute;
        }
    }
}
