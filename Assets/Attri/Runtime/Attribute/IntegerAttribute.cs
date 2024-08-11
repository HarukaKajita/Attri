using System.Collections.Generic;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class IntegerAttribute : AttributeBase
    {
        public List<FrameData<int>> values = new();

        public IntegerAttribute() : base("IntegerAttribute", AttributeType.Integer, 1) {}
        public IntegerAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}
        public override int FrameCount()
        {
            return values.Count;
        }
        public override int AttributeCount(int frame)
        {
            return values[frame].data.Count;
        }
    }
}
