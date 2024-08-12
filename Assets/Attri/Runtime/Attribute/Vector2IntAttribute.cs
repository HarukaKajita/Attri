using System.Collections.Generic;
using System.Linq;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class Vector2IntAttribute : AttributeBase
    {
        public List<FrameData<Vector2Int>> frames = new();
        
        public Vector2IntAttribute() : base(nameof(Vector2IntAttribute), AttributeType.Integer, 2) {}
        public Vector2IntAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}

        public override List<FrameData<object>> GetTemporalFrameData()
        {
            return frames.ConvertAll(frame => new FrameData<object>(frame.data.Cast<object>().ToList()));
        }
    }
}
