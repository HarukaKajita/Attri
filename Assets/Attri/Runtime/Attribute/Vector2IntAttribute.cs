using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class Vector2IntAttribute : AttributeBase<Vector2Int>
    {
        public override AttributeType GetAttributeType() => AttributeType.Int;
        public override ushort GetDimension() => 2;
        public Vector2IntAttribute() : base(nameof(Vector2IntAttribute)) {}
        public Vector2IntAttribute(string name) : base(name) {}
        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<Vector2IntAttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }
        public override void DrawAttributeDetailInspector()
        {
        }
    }
}