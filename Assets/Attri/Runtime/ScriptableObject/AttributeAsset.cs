using System;
using System.Collections.Generic;
using UnityEngine;

namespace Attri.Runtime
{
    public class AttributeAsset<T> : ScriptableObject
    {
        public AttributeType attributeType = AttributeType.String;
        public ushort dimension = 0;
        public List<T> values = new();
        
        public void SetFromPackedAttribute(PackedAttribute packedAttribute)
        {
            name = packedAttribute.name;
            attributeType = packedAttribute.attributeType;
            dimension = packedAttribute.dimension;
            values = new List<T>();
            foreach (var valueCsv in packedAttribute.valueCsvList)
            {
                values.Add((T)Convert.ChangeType(valueCsv, typeof(T)));
            }
        } 
    }
}
