using System.Collections.Generic;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [AttributeType(AttributeType.Integer, 1)]
    public class IntegerAttributeAssetBase : AttributeAssetBase
    {
        public List<FrameData<int>> values = new();

        public IntegerAttributeAssetBase() : base(AttributeType.Integer, 1)
        {
        }
        public IntegerAttributeAssetBase(string name, AttributeType attributeType, ushort dimension) : base(attributeType, dimension)
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
