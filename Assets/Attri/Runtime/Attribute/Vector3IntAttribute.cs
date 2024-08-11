using System.Collections.Generic;
using System.Linq;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    public class Vector3IntAttribute : AttributeBase
    {
        public List<FrameData<Vector3Int>> frames = new();

        public Vector3IntAttribute() : base(nameof(Vector3IntAttribute), AttributeType.Integer, 3) {}
        public Vector3IntAttribute(string name, AttributeType attributeType, ushort dimension) : base(name, attributeType, dimension) {}

        protected override List<FrameData<object>> GetFrameData()
        {
            return frames.ConvertAll(frame => new FrameData<object>(frame.data.Cast<object>().ToList()));
        }
    }
}
