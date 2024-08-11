using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{             
    [Union(1, typeof(IntegerAttributeAssetBase))]
    [Union(2, typeof(FloatAttributeAssetBase))]
    [Union(3, typeof(BoolAttributeAssetBase))]
    [Union(4, typeof(StringAttributeAssetBase))]
    [Union(5, typeof(Vector3AttributeAssetBase))]
    [Union(6, typeof(Vector3IntAttributeAssetBase))]
    [Union(7, typeof(Vector2AttributeAssetBase))]
    [Union(8, typeof(Vector2IntAttributeAssetBase))]
    [MessagePackObject(true)]
    public abstract class AttributeAssetBase : ScriptableObject
    {
        public AttributeType attributeType;
        public ushort dimension = 0;
        
        public AttributeAssetBase(AttributeType attributeType, ushort dimension)
        {
            this.attributeType = attributeType;
            this.dimension = dimension;
        }

        public override string ToString()
        {
            return $"{attributeType} {name}[{dimension}]";
        }
    }
}
