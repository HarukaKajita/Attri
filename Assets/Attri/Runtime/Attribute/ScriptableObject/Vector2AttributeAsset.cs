using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Vector2AttributeAsset), menuName = "Attri/Attribute/Vector2", order = 0)]
    public class Vector2AttributeAsset : AttributeAsset
    {
        public Vector2Attribute attribute = new ();
        private void Reset()
        {
            attribute = new Vector2Attribute();
        }
        public override IAttribute GetAttribute()
        {
            return attribute;
        }
    }
}
