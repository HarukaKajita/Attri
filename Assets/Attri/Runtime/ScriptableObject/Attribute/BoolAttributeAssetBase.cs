using System.Collections.Generic;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [AttributeType(AttributeType.Bool, 1)]
    public class BoolAttributeAssetBase : AttributeAssetBase
    {
        public List<FrameData<bool>> values = new ();

        public BoolAttributeAssetBase() : base(AttributeType.Bool, 1)
        {
        }
        public BoolAttributeAssetBase(string name, AttributeType attributeType, ushort dimension) : base(attributeType, dimension)
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
