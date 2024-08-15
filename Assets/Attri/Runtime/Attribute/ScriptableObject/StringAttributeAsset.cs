using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(StringAttributeAsset), menuName = "Attri/Attribute/String", order = 0)]
    public class StringAttributeAsset : AttributeAsset
    {
        public StringAttribute attribute = new ();
        private void Reset()
        {
            attribute = new StringAttribute();
        }
        public override IAttribute GetAttribute()
        {
            return attribute;
        }
    }
}
