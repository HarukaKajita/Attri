using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class Vector3IntAttribute : AttributeBase
    {
        public List<FrameData<Vector3Int>> values = new();

        public Vector3IntAttribute() : base("Vector3IntAttribute", AttributeType.Integer, 3) {}
        public Vector3IntAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}

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
