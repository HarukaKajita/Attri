using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class FloatAttribute : AttributeBase<float>
    {
        public override AttributeType GetAttributeType() => AttributeType.Float;
        public override ushort GetDimension() => 1;
        public FloatAttribute() : base(nameof(FloatAttribute)) {}
        public FloatAttribute(string name) : base(name) {}
        public override List<byte[]> GeByte(int frameIndex)
        {
            var frame = frames[frameIndex];
            var data = new List<byte[]>();
            foreach (var v in frame.data)
                data.Add(BitConverter.GetBytes(v));
            return data;
        }

        public override int GetByteSize()
        {
            return sizeof(float);
        }

        public override void DrawAttributeDetailInspector()
        {
        }
        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<FloatAttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }
    }
}
