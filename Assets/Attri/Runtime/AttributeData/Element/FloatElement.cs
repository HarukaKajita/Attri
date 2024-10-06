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
        public float[] AsFloat() => value;
        public int[] AsInt() => throw new NotImplementedException();
        public string[] AsString() => throw new NotImplementedException();
        public object[] AsObject() => value.Cast<object>().ToArray();
        public ushort[] HalfValues() => value.Select(Mathf.FloatToHalf).ToArray();
        public byte[] AsByte() => value.SelectMany(BitConverter.GetBytes).ToArray();
        public uint[] AsUint() => value.Select(AsUint).ToArray();
        private static uint AsUint(float value) => BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
    }
}
