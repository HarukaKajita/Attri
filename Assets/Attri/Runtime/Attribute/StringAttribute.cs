using System;
using System.Collections.Generic;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class StringAttribute : AttributeBase
    {
        public List<FrameData<string>> values = new();
        public StringAttribute() : base("StringAttribute", AttributeType.String, 0) {}
        public StringAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}
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
