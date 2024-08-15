using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class BoolAttribute : AttributeBase<bool>
    {
        public override AttributeType GetAttributeType() => AttributeType.Bool;
        public override ushort GetDimension() => 1;
        public BoolAttribute() : base( nameof(BoolAttribute)) {}
        public BoolAttribute(string name) : base(name) {}
        public override void DrawAttributeDetailInspector()
        {
        }
        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<BoolAttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }   
    }
}
