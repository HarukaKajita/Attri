using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Vector4IntAttributeAsset), menuName = "Attri/Attribute/Vector4Int", order = 0)]
    public class Vector4IntAttributeAsset : AttributeAsset
    {
        public Vector4IntAttribute attribute = new ();
        private void Reset()
        {
            attribute = new Vector4IntAttribute();
        }
        public override IAttribute GetAttribute()
        {
            return attribute;
        }
    }
}
