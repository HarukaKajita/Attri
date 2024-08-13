using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class Vector2Attribute : AttributeBase<Vector2>
    {
        public override AttributeType GetAttributeType()
        {
            return AttributeType.Float;
        }
        public override ushort GetDimension()
        {
            return 2;
        }
        public Vector2Attribute() : base(nameof(Vector2Attribute)) {}
        public Vector2Attribute(string name) : base(name) {}
    }
}
