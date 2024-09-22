using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [Union(0, typeof(IntAttribute))]
    [Union(1, typeof(FloatAttribute))]
    [Union(2, typeof(BoolAttribute))]
    [Union(3, typeof(StringAttribute))]
    [Union(4, typeof(Vector3Attribute))]
    [Union(5, typeof(Vector3IntAttribute))]
    [Union(6, typeof(Vector2Attribute))]
    [Union(7, typeof(Vector2IntAttribute))]
    [Union(8, typeof(Vector4IntAttribute))]
    [Union(9, typeof(Vector4Attribute))]
    
    [Union(10, typeof(AttributeBase<int>))]
    [Union(11, typeof(AttributeBase<float>))]
    [Union(12, typeof(AttributeBase<bool>))]
    [Union(13, typeof(AttributeBase<string>))]
    [Union(14, typeof(AttributeBase<Vector3>))]
    [Union(15, typeof(AttributeBase<Vector3Int>))]
    [Union(16, typeof(AttributeBase<Vector2>))]
    [Union(17, typeof(AttributeBase<Vector2Int>))]
    [Union(18, typeof(AttributeBase<Vector4>))]
    [Union(19, typeof(AttributeBase<int[]>))]
    public interface IAttribute
    {
        public string Name();
        AttributeType GetAttributeType();
        int GetDimension();
        int FrameCount();
        Type GetDataType();
        List<object> GetObjectFrame(int frameIndex);
        List<byte[]> GeByte(int frameIndex);
        int GetByteSize();
        List<List<object>> GetObjectFrames();
        AttributeAsset CreateAsset();
        void DrawAttributeDetailInspector();
    }
}
