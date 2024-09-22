using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class BoolAttribute : AttributeBase<bool>
    {
        public override AttributeType GetAttributeType() => AttributeType.Bool;
        public override int GetDimension() => 1;
        public BoolAttribute() : base( nameof(BoolAttribute)) {}
        public BoolAttribute(string name) : base(name) {}
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
            return sizeof(bool);
        }

        public override void DrawAttributeDetailInspector()
        {
        }
        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<BoolAttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }   
    }
}
