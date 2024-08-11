using System;
using System.Collections.Generic;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [AttributeType(AttributeType.Float, 1)]
    public class FloatAttributeAssetBase : AttributeAssetBase
    {
        public List<FrameData<float>> values = new();

        public FloatAttributeAssetBase() : base(AttributeType.Float, 1)
        {
        }
        public FloatAttributeAssetBase(string name, AttributeType attributeType, ushort dimension) : base(attributeType, dimension)
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
