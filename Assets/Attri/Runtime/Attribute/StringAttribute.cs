using System;
using System.Collections.Generic;
using UnityEngine;

namespace Attri.Runtime
{
    [Serializable]
    public class StringAttribute : AttributeBase<string>
    {
        public override AttributeType GetAttributeType() => AttributeType.String;
        public StringAttribute() : base(nameof(StringAttribute)) {}
        public StringAttribute(string name) : base(name) {}
        public override List<byte[]> GeByte(int frameIndex)
        {
            var frame = frames[frameIndex];
            var data = new List<byte[]>();
            foreach (var element in frame.elements) //2次元配列のループ
                foreach (var component in element.components) //個々の値の成分のループ
                    data.Add(System.Text.Encoding.ASCII.GetBytes(component)); //byte変換
            return data;
        }

        public override int GetElementByteSize()
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
