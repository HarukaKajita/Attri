using System;
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
