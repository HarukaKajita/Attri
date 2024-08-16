using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class Vector4IntAttribute : AttributeBase<int[]>
    {
        public override AttributeType GetAttributeType() => AttributeType.Int;
        public override ushort GetDimension() => 4;
        public Vector4IntAttribute() : base(nameof(Vector4IntAttribute)) {}
        public Vector4IntAttribute(string name) : base(name) {}
        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<Vector4IntAttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }
        public override void DrawAttributeDetailInspector()
        {
        }
    }
}
