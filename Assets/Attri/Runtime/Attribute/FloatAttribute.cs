using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class FloatAttribute : AttributeBase<float>
    {
        public override AttributeType GetAttributeType()
        {
            return AttributeType.Float;
        }
        public override ushort GetDimension()
        {
            return 1;
        }
        public FloatAttribute() : base(nameof(FloatAttribute)) {}
        public FloatAttribute(string name) : base(name) {}

    }
}
