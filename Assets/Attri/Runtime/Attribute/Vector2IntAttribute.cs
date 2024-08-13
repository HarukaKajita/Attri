using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class Vector2IntAttribute : AttributeBase<Vector2Int>
    {
        public override AttributeType GetAttributeType()
        {
            return AttributeType.Integer;
        }
        public override ushort GetDimension()
        {
            return 2;
        }
        public Vector2IntAttribute() : base(nameof(Vector2IntAttribute)) {}
        public Vector2IntAttribute(string name) : base(name) {}
    }
}
