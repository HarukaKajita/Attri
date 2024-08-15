using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Vector3IntAttributeAsset), menuName = "Attri/Attribute/Vector3Int", order = 0)]
    public class Vector3IntAttributeAsset : AttributeAsset
    {
        public Vector3IntAttribute attribute = new ();
        private void Reset()
        {
            attribute = new Vector3IntAttribute();
        }
        public override IAttribute GetAttribute()
        {
            return attribute;
        }
    }
}
