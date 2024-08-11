using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class FloatAttribute : AttributeBase
    {
        public List<FrameData<float>> frames = new();

        public FloatAttribute() : base(nameof(FloatAttribute), AttributeType.Float, 1) {}
        public FloatAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}
        protected override List<FrameData<object>> GetFrameData()
        {
            return frames.ConvertAll(frame => new FrameData<object>(frame.data.Cast<object>().ToList()));
        }
    }
}
