using System;
using System.Linq;
using UnityEngine;

namespace Attri.Runtime
{
    [Serializable]
    public class IntElement : IDataProvider
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
        public int Dimension() => value.Length;
        public AttributeDataType GetAttributeType() => AttributeDataType.Int;
        public float[][] AsFloat() => throw new NotImplementedException();
        public int[][] AsInt() => new []{value};
        public string[][] AsString() => throw new NotImplementedException();
        public ScriptableObject GetScriptableObject() => throw new NotImplementedException();
        // public object[] AsObject() => value.Cast<object>().ToArray();
        // public ushort[] HalfValues() => throw new NotImplementedException();
        // public byte[] AsByte() => value.SelectMany(BitConverter.GetBytes).ToArray();
        // public uint[] AsUint() => value.Select(AsUint).ToArray();
        // private static uint AsUint(int value) => BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
    }
}
