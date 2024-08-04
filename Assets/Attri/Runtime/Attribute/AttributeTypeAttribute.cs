using System;

namespace Attri.Runtime
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AttributeTypeAttribute : Attribute
    {
        public AttributeTypeAttribute(AttributeType attributeType, ushort dimension)
        {
            this.attributeType = attributeType;
            this.dimension = dimension;
        }

        public AttributeType attributeType { get; }
        public ushort dimension { get; }
    }
}
