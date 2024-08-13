using System;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class StringAttribute : AttributeBase<string>
    {
        public override AttributeType GetAttributeType()
        {
            return AttributeType.String;
        }
        public override ushort GetDimension()
        {
            return 1;
        }
        public StringAttribute() : base(nameof(StringAttribute)) {}
        public StringAttribute(string name) : base(name) {}
    }
}
