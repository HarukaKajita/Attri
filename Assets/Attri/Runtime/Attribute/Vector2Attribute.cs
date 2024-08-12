using System.Collections.Generic;
using System.Linq;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class Vector2Attribute : AttributeBase
    {
        public List<FrameData<Vector2>> frames = new();
        public Vector2Attribute() : base(nameof(Vector2Attribute) , AttributeType.Float, 2) {}
        public Vector2Attribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}

        public override List<FrameData<object>> GetTemporalFrameData()
        {
            return frames.ConvertAll(frame => new FrameData<object>(frame.data.Cast<object>().ToList()));
        }
    }
}
