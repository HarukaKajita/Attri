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
            var frameCount = FrameCount();
            var str = $"{attributeType.ToString()} {name}[{frameCount}]";
            for (var i = 0; i < frameCount; i++)
                str += $", [{i++}][{ValueCount(i)}]";
            return str;
        }

        public int FrameCount()
        {
            return GetFrameData().Count;
        }

        public int ValueCount(int frame)
        {
            return GetFrameData()[frame].data.Count;
        }

        protected abstract List<FrameData<object>> GetFrameData();
    }
}
