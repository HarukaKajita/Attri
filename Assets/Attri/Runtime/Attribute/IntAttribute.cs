using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class IntAttribute : AttributeBase<int>
    {
        public override AttributeType GetAttributeType() => AttributeType.Int;
        public override ushort GetDimension() => 1;
        public IntAttribute() : base(nameof(IntAttribute)) {}
        public IntAttribute(string name) : base(name) {}
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
