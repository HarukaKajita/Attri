using System;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class IntegerAttribute : AttributeBase<int>
    {
        public override AttributeType GetAttributeType()
        {
            return AttributeType.Integer;
        }
        public override ushort GetDimension()
        {
            return 1;
        }
        public IntegerAttribute() : base(nameof(IntegerAttribute)) {}
        public IntegerAttribute(string name) : base(name) {}
    }
}
