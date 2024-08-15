using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Vector2IntAttributeAsset), menuName = "Attri/Attribute/Vector2Int", order = 0)]
    public class Vector2IntAttributeAsset : AttributeAsset
    {
        public Vector2IntAttribute attribute = new ();
        private void Reset()
        {
            attribute = new Vector2IntAttribute();
        }
        public override IAttribute GetAttribute()
        {
            return attribute;
        }
    }
}
