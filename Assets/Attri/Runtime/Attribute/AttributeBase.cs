using System.Collections.Generic;
using MessagePack;

namespace Attri.Runtime
{             
    [Union(1, typeof(IntegerAttribute))]
    [Union(2, typeof(FloatAttribute))]
    [Union(3, typeof(BoolAttribute))]
    [Union(4, typeof(StringAttribute))]
    [Union(5, typeof(Vector3Attribute))]
    [Union(6, typeof(Vector3IntAttribute))]
    [Union(7, typeof(Vector2Attribute))]
    [Union(8, typeof(Vector2IntAttribute))]
    [MessagePackObject(true)]
    public abstract class AttributeBase
    {
        public string name;
        public AttributeType attributeType;
        public ushort dimension = 0;
        public AttributeBase(string name, AttributeType attributeType, ushort dimension)
        {
            this.name = name;
            this.attributeType = attributeType;
            this.dimension = dimension;
        }

        public override string ToString()
        {
            var data = GetTemporalFrameData();
            var frameCount = data.Count;
            var str = $"{attributeType.ToString()}{dimension} {name}[{frameCount}]";
            for (var i = 0; i < frameCount; i++)
                str += $", [{i}][{data[i].data.Count}]";
            return str;
        }

        public int FrameCount()
        {
            return GetTemporalFrameData().Count;
        }

        public abstract List<FrameData<object>> GetTemporalFrameData();
        // public abstract Dictionary<string, object> GetValueDetailObject(int frameIndex);
        // public abstract Dictionary<string, object> GetValueDetailObject();
    }
}
