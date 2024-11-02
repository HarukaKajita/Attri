using System;
using UnityEngine;

namespace Attri.Runtime
{
    [Serializable]
    public class IntElement : ElementBase
    {
        [SerializeField]
        public int[] value = Array.Empty<int>();

        public IntElement(int[] value)
        {
            this.value = value;
        }
        // キャスト
        public static implicit operator int[](IntElement dataProvider) => dataProvider.value;
        public static implicit operator IntElement(int[] value) => new(value);
        public override int Dimension() => value.Length;
        public override AttributeDataType GetAttributeType() => AttributeDataType.Int;
        public override int[][][] AsInt() => new[] { new []{value} };
    }
}
