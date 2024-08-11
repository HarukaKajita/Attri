using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class Vector2IntAttribute : AttributeBase
    {
        public List<FrameData<Vector2Int>> values = new();
        
        public Vector2IntAttribute() : base("Vector2IntAttribute", AttributeType.Integer, 2) {}
        public Vector2IntAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}
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
