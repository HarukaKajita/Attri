using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class Vector3Attribute : AttributeBase
    {
        public List<FrameData<Vector3>> values = new();

        public Vector3Attribute() : base("Vector3Attribute", AttributeType.Float, 3) {}
        public Vector3Attribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}
        public override int FrameCount()
        {
            return values.Count;
        }
        public override int AttributeCount(int frame)
        {
            return values[frame].data.Count;
        }
        [ContextMenu("Serialize")]
        private void Serialize()
        {
            var bytes = AttributeSerializer.Serialize(this);
            var json = AttributeSerializer.ConvertToJson(bytes);
            Debug.Log(json);
        }
    }
}
