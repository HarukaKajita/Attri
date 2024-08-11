using System.Collections.Generic;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class BoolAttribute : AttributeBase
    {
        public List<FrameData<bool>> values = new ();

        public BoolAttribute() : base( "BoolAttribute" , AttributeType.Bool, 1) {}
        public BoolAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}
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
