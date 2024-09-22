using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [CreateAssetMenu(fileName = nameof(Vector4AttributeAsset), menuName = "Attri/Attribute/Vector4", order = 0)]
    public class Vector4AttributeAsset : AttributeAsset
    {
        public Vector4Attribute _attribute = new ();

        private void Reset()
        {
            _attribute = new Vector4Attribute();
        }

        public override IAttribute GetAttribute()
        {
            return _attribute;
        }
    }
}
