using System;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class Vector3Attribute : AttributeBase<Vector3>
    {
        public override AttributeType GetAttributeType()
        {
            return AttributeType.Float;
        }
        public override ushort GetDimension()
        {
            return 3;
        }
        public Vector3Attribute() : base(nameof(Vector3Attribute)) {}
        public Vector3Attribute(string name) : base(name) {}
    }
}
