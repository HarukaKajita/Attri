using System.Collections.Generic;
using System.Linq;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class Vector3Attribute : AttributeBase
    {
        public List<FrameData<Vector3>> frames = new();

        public Vector3Attribute() : base(nameof(Vector3Attribute), AttributeType.Float, 3) {}
        public Vector3Attribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}

        public override List<FrameData<object>> GetTemporalFrameData()
        {
            return frames.ConvertAll(frame => new FrameData<object>(frame.data.Cast<object>().ToList()));
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
