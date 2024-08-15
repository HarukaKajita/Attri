using System;
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
