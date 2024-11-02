using System;
using UnityEngine;

namespace Attri.Runtime
{
    [Serializable]
    public class FloatElement : ElementBase
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
        public override int Dimension() => value.Length;
        public override AttributeDataType GetAttributeType() => AttributeDataType.Float;
        public override float[][][] AsFloat() => new[] { new []{value} };
    }
}
