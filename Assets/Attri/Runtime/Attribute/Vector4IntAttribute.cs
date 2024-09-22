using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class Vector4IntAttribute : AttributeBase<int[]>
    {
        public override AttributeType GetAttributeType() => AttributeType.Int;
        public override int GetDimension() => 4;
        public Vector4IntAttribute() : base(nameof(Vector4IntAttribute)) {}
        public Vector4IntAttribute(string name) : base(name) {}
        public override List<byte[]> GeByte(int frameIndex)
        {
            var frame = frames[frameIndex];
            var data = new List<byte[]>();
            foreach (var v in frame.data)
            {
                var array = new byte[GetByteSize()];
                var x = BitConverter.GetBytes(v[0]);
                var y = BitConverter.GetBytes(v[1]);
                var z = BitConverter.GetBytes(v[2]);
                var w = BitConverter.GetBytes(v[3]);
                array[0] = x[0];
                array[1] = x[1];
                array[2] = x[2];
                array[3] = x[3];
                array[4] = y[0];
                array[5] = y[1];
                array[6] = y[2];
                array[7] = y[3];
                array[8] = z[0];
                array[9] = z[1];
                array[10] = z[2];
                array[11] = z[3];
                array[12] = w[0];
                array[13] = w[1];
                array[14] = w[2];
                array[15] = w[3];
                data.Add(array);
            }
            return data;
        }

        public override int GetByteSize()
        {
            return sizeof(int) * GetDimension();
        }

        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<Vector4IntAttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }
        public override void DrawAttributeDetailInspector()
        {
        }
    }
}
