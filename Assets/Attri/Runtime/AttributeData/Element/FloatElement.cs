using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Attri.Runtime
{
    [Serializable]
    public class FloatElement : IElement
    {
        [SerializeField]
        public float[] value = Array.Empty<float>();
        
        public FloatElement(float[] value)
        {
            this.value = value;
        }
        // float[]とFloatElementのキャスト
        public static implicit operator float[](FloatElement element) => element.value;
        public static implicit operator FloatElement(float[] value) => new(value);
        
        public int Size()
        {
            return value.Length;
        }
        public float[] AsFloat()
        {
            return value;
        }

        public int[] AsInt()
        {
            throw new NotImplementedException();
        }

        public string[] AsString()
        {
            throw new NotImplementedException();
        }

        public object[] AsObject()
        {
            return value.Cast<object>().ToArray();
        }

        public ushort[] HalfValues()
        {
            return value.Select(Mathf.FloatToHalf).ToArray();
        }

        public byte[] AsByte()
        {
            return value.SelectMany(BitConverter.GetBytes).ToArray();
        }

        public uint[] AsUint()
        {
            return value.Select(AsUint).ToArray();
        }

        private static uint AsUint(float value)
        {
            return BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
        }
    }
}
