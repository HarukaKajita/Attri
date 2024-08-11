using System;
using System.Collections.Generic;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [AttributeType(AttributeType.String, 0)]
    public class StringAttributeAssetBase : AttributeAssetBase
    {
        public List<FrameData<string>> values = new();
        public StringAttributeAssetBase() : base(AttributeType.String, 0)
        {
        }
        public StringAttributeAssetBase(string name, AttributeType attributeType, ushort dimension) : base(attributeType, dimension)
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
