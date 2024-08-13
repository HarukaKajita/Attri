using System;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class BoolAttribute : AttributeBase<bool>
    {
        public override AttributeType GetAttributeType()
        {
            return AttributeType.Bool;
        }
        public override ushort GetDimension()
        {
            return 1;
        }
        public BoolAttribute() : base( nameof(BoolAttribute)) {}
        public BoolAttribute(string name) : base(name) {}
    }
}
