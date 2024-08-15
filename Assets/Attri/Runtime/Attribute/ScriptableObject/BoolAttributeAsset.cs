using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(BoolAttributeAsset), menuName = "Attri/Attribute/Bool", order = 0)]
    public class BoolAttributeAsset : AttributeAsset
    {
        public BoolAttribute attribute = new ();
        private void Reset()
        {
            attribute = new BoolAttribute();
        }
        public override IAttribute GetAttribute()
        {
            return attribute;
        }
    }
}
