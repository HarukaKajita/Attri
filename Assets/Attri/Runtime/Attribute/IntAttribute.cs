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
        public IntAttribute() : base(nameof(IntAttribute)) {}
        public IntAttribute(string name) : base(name) {}
        public override List<byte[]> GeByte(int frameIndex)
        {
            var frame = frames[frameIndex];
            var data = new List<byte[]>();
            foreach (var element in frame.elements) //要素のループ
                foreach (var component in element.components) //個々の値の成分のループ
                    data.Add(BitConverter.GetBytes(component));//byte変換
            return data;
        }

        public override int GetElementByteSize()
        {
            return sizeof(int) * GetDimension();
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
