using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{             
    // [Union(1, typeof(IntegerAttribute))]
    // [Union(2, typeof(FloatAttribute))]
    // [Union(3, typeof(BoolAttribute))]
    // [Union(4, typeof(StringAttribute))]
    // [Union(5, typeof(Vector3Attribute))]
    // [Union(6, typeof(Vector3IntAttribute))]
    // [Union(7, typeof(Vector2Attribute))]
    // [Union(8, typeof(Vector2IntAttribute))]
    [Union(1, typeof(AttributeBase<int>))]
    [Union(2, typeof(AttributeBase<float>))]
    [Union(3, typeof(AttributeBase<bool>))]
    [Union(4, typeof(AttributeBase<string>))]
    [Union(5, typeof(AttributeBase<Vector3>))]
    [Union(6, typeof(AttributeBase<Vector3Int>))]
    [Union(7, typeof(AttributeBase<Vector2>))]
    [Union(8, typeof(AttributeBase<Vector2Int>))]
    [MessagePackObject(true)]
    [Serializable]
    public class AttributeBase<T>
    {
        public string name;
        public List<FrameData<T>> frames = new();

        public virtual AttributeType GetAttributeType()
        {
            return AttributeType.Unknown;
        }

        public virtual ushort GetDimension()
        {
            return 0;
        }
        public AttributeBase(string name)
        {
            this.name = name;
            frames = new List<FrameData<T>>();
        }

        public override string ToString()
        {
            var frameCount = frames.Count;
            var str = $"{GetAttributeType().ToString()}{GetDimension()} {name}[{frameCount}]";
            for (var i = 0; i < frameCount; i++)
                str += $", [{i}][{frames[i].data.Count}]";
            return str;
        }

        public int FrameCount()
        {
            return frames.Count;
        }
    }
}
