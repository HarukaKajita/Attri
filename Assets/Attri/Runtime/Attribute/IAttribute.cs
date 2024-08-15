using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [Union(0, typeof(IntegerAttribute))]
    [Union(1, typeof(FloatAttribute))]
    [Union(2, typeof(BoolAttribute))]
    [Union(3, typeof(StringAttribute))]
    [Union(4, typeof(Vector3Attribute))]
    [Union(5, typeof(Vector3IntAttribute))]
    [Union(6, typeof(Vector2Attribute))]
    [Union(7, typeof(Vector2IntAttribute))]
    
    [Union(8, typeof(AttributeBase<int>))]
    [Union(9, typeof(AttributeBase<float>))]
    [Union(10, typeof(AttributeBase<bool>))]
    [Union(11, typeof(AttributeBase<string>))]
    [Union(12, typeof(AttributeBase<Vector3>))]
    [Union(13, typeof(AttributeBase<Vector3Int>))]
    [Union(14, typeof(AttributeBase<Vector2>))]
    [Union(15, typeof(AttributeBase<Vector2Int>))]
    public interface IAttribute
    {
        public string Name();
        AttributeType GetAttributeType();
        ushort GetDimension();
        int FrameCount();
        Type GetDataType();
        List<object> GetObjectFrame(int index);
        List<List<object>> GetObjectFrames();
        void DrawAttributeDetailInspector();
    }
}
