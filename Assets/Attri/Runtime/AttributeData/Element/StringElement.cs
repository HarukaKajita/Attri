using System;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Attri.Runtime
{
    [Serializable]
    public class StringElement : IDataProvider
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
        public int Dimension()=> value.Length;
        public AttributeDataType GetAttributeType() => AttributeDataType.String;
        public float[][] AsFloat() => throw new NotImplementedException();
        public int[][] AsInt() => throw new NotImplementedException();
        public string[][] AsString() => new[] { value };
        public ScriptableObject GetScriptableObject() => throw new NotImplementedException();
    }
}
