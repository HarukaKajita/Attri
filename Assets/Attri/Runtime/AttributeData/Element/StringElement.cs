using System;
using UnityEngine;

namespace Attri.Runtime
{
    [Serializable]
    public class StringElement : ElementBase
    {
        [SerializeField]
        public string[] value = Array.Empty<string>();

        public StringElement(string[] value)
        {
            this.value = value;
        }
        // キャスト
        public static implicit operator string[](StringElement element) => element.value;
        public static implicit operator StringElement(string[] value) => new(value);
        public override AttributeDataType GetAttributeType() => AttributeDataType.String;
        public override string[][][] AsString() => new[] { new[] { value } };
    }
}
