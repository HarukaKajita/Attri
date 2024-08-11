using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [AttributeType(AttributeType.Float, 2)]
    public class Vector2AttributeAssetBase : AttributeAssetBase
    {
        public List<FrameData<Vector2>> values = new();

        public Vector2AttributeAssetBase() : base(AttributeType.Float, 2)
        {
        }
        public Vector2AttributeAssetBase(string name, AttributeType attributeType, ushort dimension) : base(attributeType, dimension)
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
