using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class StringAttribute : AttributeBase
    {
        public List<FrameData<string>> frames = new();
        public StringAttribute() : base("StringAttribute", AttributeType.String, 0) {}
        public StringAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}
        protected override List<FrameData<object>> GetFrameData()
        {
            return frames.ConvertAll(frame => new FrameData<object>(frame.data.Cast<object>().ToList()));
        }
    }
}
