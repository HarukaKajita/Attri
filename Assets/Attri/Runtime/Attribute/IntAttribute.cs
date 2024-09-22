using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class IntAttribute : AttributeBase<int>
    {
        public override AttributeType GetAttributeType() => AttributeType.Int;
        public override int GetDimension() => 1;
        public IntAttribute() : base(nameof(IntAttribute)) {}
        public IntAttribute(string name) : base(name) {}
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
            return sizeof(int);
        }

        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<IntAttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }
        public override void DrawAttributeDetailInspector()
        {
        }
    }
}
