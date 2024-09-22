using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class Vector2IntAttribute : AttributeBase<Vector2Int>
    {
        public override AttributeType GetAttributeType() => AttributeType.Int;
        public override ushort GetDimension() => 2;
        public Vector2IntAttribute() : base(nameof(Vector2IntAttribute)) {}
        public Vector2IntAttribute(string name) : base(name) {}
        public override List<byte[]> GeByte(int frameIndex)
        {
            var frame = frames[frameIndex];
            var data = new List<byte[]>();
            foreach (var v in frame.data)
            {
                var array = new byte[GetByteSize()];
                var x = BitConverter.GetBytes(v.x);
                var y = BitConverter.GetBytes(v.y);
                array[0] = x[0];
                array[1] = x[1];
                array[2] = x[2];
                array[3] = x[3];
                array[4] = y[0];
                array[5] = y[1];
                array[6] = y[2];
                array[7] = y[3];
                data.Add(array);
            }
            return data;
        }

        public override int GetByteSize()
        {
            return sizeof(int) * 2;
        }

        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<Vector2IntAttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }
        public override void DrawAttributeDetailInspector()
        {
        }
    }
}
