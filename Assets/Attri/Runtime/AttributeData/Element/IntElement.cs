using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Attri.Runtime
{
    [Serializable]
    public class IntElement : IElement
    {
        [SerializeField]
        public int[] value = Array.Empty<int>();

        public IntElement(int[] value)
        {
            this.value = value;
        }
        // int[]とFloatElementのキャスト
        public static implicit operator int[](IntElement element) => element.value;
        public static implicit operator IntElement(int[] value) => new(value);
        
        public int Size()
        {
            return value.Length;
        }
        public float[] AsFloat()
        {
            throw new NotImplementedException();
        }

        public int[] AsInt()
        {
            return value;
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
            throw new NotImplementedException();
        }

        public byte[] AsByte()
        {
            return value.SelectMany(BitConverter.GetBytes).ToArray();
        }

        public uint[] AsUint()
        {
            return value.Select(AsUint).ToArray();
        }

        private static uint AsUint(int value)
        {
            return BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
        }
    }
}
