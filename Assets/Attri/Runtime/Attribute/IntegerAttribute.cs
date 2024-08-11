using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class IntegerAttribute : AttributeBase
    {
        public List<FrameData<int>> frames = new();

        public IntegerAttribute() : base(nameof(IntegerAttribute), AttributeType.Integer, 1) {}
        public IntegerAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}
        protected override List<FrameData<object>> GetFrameData()
        {
            return frames.ConvertAll(frame => new FrameData<object>(frame.data.Cast<object>().ToList()));
        }
    }
}
