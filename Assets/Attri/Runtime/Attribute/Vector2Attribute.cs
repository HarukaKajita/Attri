using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class Vector2Attribute : AttributeBase<Vector2>
    {
        public override AttributeType GetAttributeType() => AttributeType.Float;
        public override ushort GetDimension() => 2;
        public Vector2Attribute() : base(nameof(Vector2Attribute)) {}
        public Vector2Attribute(string name) : base(name) {}
        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<Vector2AttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }
        public override void DrawAttributeDetailInspector()
        {
        }
    }
}
