using System;
using System.Collections.Generic;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class FloatAttribute : AttributeBase
    {
        public List<FrameData<float>> values = new();

        public FloatAttribute() : base("FloatAttribute", AttributeType.Float, 1) {}
        public FloatAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}
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
