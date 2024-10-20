using System;
using System.Linq;
using UnityEngine;

namespace Attri.Runtime
{
    [Serializable]
    public class FloatElement : IDataProvider
    {
        [SerializeField]
        public float[] value = Array.Empty<float>();
        
        public FloatElement(float[] value)
        {
            this.value = value;
        }
        // キャスト
        public static implicit operator float[](FloatElement element) => element.value;
        public static implicit operator FloatElement(float[] value) => new(value);
        public int Dimension() => value.Length;
        public AttributeDataType GetAttributeType() => AttributeDataType.Float;
        public float[][] AsFloat() => new []{value};
        public int[][] AsInt() => throw new NotImplementedException();
        public string[][] AsString() => throw new NotImplementedException();
        public ScriptableObject GetScriptableObject() => throw new NotImplementedException();
    }
}
