using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class StringAttribute : AttributeBase<string>
    {
        public override AttributeType GetAttributeType() => AttributeType.String;
        public override ushort GetDimension() => 1;
        public StringAttribute() : base(nameof(StringAttribute)) {}
        public StringAttribute(string name) : base(name) {}
        public override List<byte[]> GeByte(int frameIndex)
        {
            var frame = frames[frameIndex];
            var data = new List<byte[]>();
            foreach (var v in frame.data)
                data.Add(System.Text.Encoding.ASCII.GetBytes(v));
            return data;
        }

        public override int GetByteSize()
        {
            // stringの場合は文字数に依ってbyte数が可変なので無効
            return -1;
        }

        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<StringAttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }
        public override void DrawAttributeDetailInspector()
        {
        }
    }
}
