using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace Attri.Runtime
{
    [Serializable]
    public class StringElement : IElement
    {
        [SerializeField]
        public string[] value = Array.Empty<string>();

        public StringElement(string[] value)
        {
            this.value = value;
        }
        // int[]とFloatElementのキャスト
        public static implicit operator string[](StringElement element) => element.value;
        public static implicit operator StringElement(string[] value) => new(value);
        
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
            throw new NotImplementedException();
        }

        public string[] AsString()
        {
            return value;
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
            return value.SelectMany(Encoding.UTF8.GetBytes).ToArray();
        }

        public uint[] AsUint()
        {
            throw new NotImplementedException();
        }
    }
}
