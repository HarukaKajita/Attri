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

        public override List<FrameData<object>> GetTemporalFrameData()
        {
            return frames.ConvertAll(frame => new FrameData<object>(frame.data.Cast<object>().ToList()));
        }

        // public override Dictionary<string, object> GetValueDetailObject(int frameIndex)
        // {
        //     if (frameIndex < 0 || frameIndex >= frames.Count)
        //         return null;
        //     
        //     var frame = frames[frameIndex];
        //     var result = new Dictionary<string, object>
        //     {
        //         { "MaxX", frame.data.Max(a => a.x) },
        //         { "MinX", frame.data.Min(a => a.x) },
        //         { "MaxY", frame.data.Max(a => a.y) },
        //         { "MinY", frame.data.Min(a => a.y) },
        //         { "MaxZ", frame.data.Max(a => a.z) },
        //         { "MinZ", frame.data.Min(a => a.z) },
        //         { "MaxLength", frame.data.Max(a => a.magnitude) },
        //         { "MinLength", frame.data.Min(a => a.magnitude) }
        //     };
        //     return result;   
        // }
        //
        // public Dictionary<string, float>GetValueDetailRaw(int frameIndex)
        // {
        //     if (frameIndex < 0 || frameIndex >= frames.Count)
        //         return null;
        //     var frame = frames[frameIndex];
        //     var result = new Dictionary<string, float>();
        //     result.Add("MaxX", frame.data.Max(a => a.x));
        //     result.Add("MinX", frame.data.Min(a => a.x));
        //     result.Add("MaxY", frame.data.Max(a => a.y));
        //     result.Add("MinY", frame.data.Min(a => a.y));
        //     result.Add("MaxZ", frame.data.Max(a => a.z));
        //     result.Add("MinZ", frame.data.Min(a => a.z));
        //     result.Add("MaxLength", frame.data.Max(a => a.magnitude));
        //     result.Add("MinLength", frame.data.Min(a => a.magnitude));
        //     return result;
        // }
        // public override Dictionary<string, object> GetValueDetailObject()
        // {
        //     var values = frames.SelectMany(frame => frame.data).ToList();
        //     var result = new Dictionary<string, object>
        //     {
        //         { "MaxX", values.Max(a => a.x) },
        //         { "MinX", values.Min(a => a.x) },
        //         { "MaxY", values.Max(a => a.y) },
        //         { "MinY", values.Min(a => a.y) },
        //         { "MaxZ", values.Max(a => a.z) },
        //         { "MinZ", values.Min(a => a.z) },
        //         { "MaxLength", values.Max(a => a.magnitude) },
        //         { "MinLength", values.Min(a => a.magnitude) }
        //     };
        //     return result;
        // }
        // public Dictionary<string, float> GetValueDetailRaw()
        // {
        //     var values = frames.SelectMany(frame => frame.data).ToList();
        //     var result = new Dictionary<string, float>
        //     {
        //         { "MaxX", values.Max(a => a.x) },
        //         { "MinX", values.Min(a => a.x) },
        //         { "MaxY", values.Max(a => a.y) },
        //         { "MinY", values.Min(a => a.y) },
        //         { "MaxZ", values.Max(a => a.z) },
        //         { "MinZ", values.Min(a => a.z) },
        //         { "MaxLength", values.Max(a => a.magnitude) },
        //         { "MinLength", values.Min(a => a.magnitude) }
        //     };
        //     return result;
        // }
    }
}
