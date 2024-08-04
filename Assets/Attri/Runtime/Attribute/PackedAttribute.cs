using System.Collections.Generic;

namespace Attri.Runtime
{
    public class PackedAttribute
    {
        public string name = "";
        public AttributeType attributeType = AttributeType.String;
        public ushort dimension = 0;
        public List<string> valueCsvList = new();
    }

    public enum AttributeType
    {
        Float,
        Integer,
        Bool,
        String,
    }
}
