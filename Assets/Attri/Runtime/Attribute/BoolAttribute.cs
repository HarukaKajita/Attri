using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class BoolAttribute : AttributeBase
    {
        public List<FrameData<bool>> frames = new();

        public BoolAttribute() : base( "BoolAttribute" , AttributeType.Bool, 1) {}
        public BoolAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}

        protected override List<FrameData<object>> GetFrameData()
        {
            return frames.ConvertAll(frame => new FrameData<object>(frame.data.Cast<object>().ToList()));
        }
    }
}
