using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class Vector2Attribute : AttributeBase
    {
        public List<FrameData<Vector2>> values = new();

        public Vector2Attribute() : base("Vector2AttributeBase" , AttributeType.Float, 2) {}
        public Vector2Attribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}
        public override int FrameCount()
        {
            return values.Count;
        }
        public override int AttributeCount(int frame)
        {
            return values[frame].data.Count;
        }
    }
}
