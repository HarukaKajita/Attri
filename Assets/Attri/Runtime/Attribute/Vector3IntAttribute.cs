using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class Vector3IntAttribute : AttributeBase<Vector3Int>
    {
        public override AttributeType GetAttributeType() => AttributeType.Int;
        public override ushort GetDimension() => 3;
        public Vector3IntAttribute() : base(nameof(Vector3IntAttribute)) {}
        public Vector3IntAttribute(string name) : base(name) {}
        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<Vector3IntAttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }
        public override void DrawAttributeDetailInspector()
        {
        }
    }
}
