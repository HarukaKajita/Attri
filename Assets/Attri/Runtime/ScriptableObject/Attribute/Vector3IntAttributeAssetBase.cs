using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [AttributeType(AttributeType.Integer, 3)]
    public class Vector3IntAttributeAssetBase : AttributeAssetBase
    {
        public List<FrameData<Vector3Int>> values = new();

        public Vector3IntAttributeAssetBase() : base(AttributeType.Integer, 3)
        {
        }
        public Vector3IntAttributeAssetBase(string name, AttributeType attributeType, ushort dimension) : base(attributeType, dimension)
        {
            this.name = name;
        }
        public override string ToString()
        {
            var str = base.ToString();
            foreach (var list in values)
            {
                str += $"Values: [{list.data.Count}]";
                foreach (var value in list.data)
                {
                    str += $"({value})";
                }
            }
            return str;
        }
    }
}
