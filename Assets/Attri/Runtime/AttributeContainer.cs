using System;
using System.Collections.Generic;

namespace Attri.Runtime
{
    [Serializable]
    public enum AttributeType
    {
        Real,
        Integer,
        String,
    }
    [Serializable]
    public struct AttributeContainer
    {
        public string name;
        public AttributeType type;
        public ushort dimension;
        public ushort precision;//bit数?
        public bool signed;
        // 時系列データなどの場合にリストの要素数が増える
        public List<string> valueString;

        public AttributeContainer(string name, AttributeType attributeType, ushort dimension, ushort precision, bool signed, List<string> valueString)
        {
            this.name = name;
            this.type = attributeType;
            this.dimension = dimension;
            this.precision = precision;
            this.signed = signed;
            this.valueString = valueString;
        }
    }
}