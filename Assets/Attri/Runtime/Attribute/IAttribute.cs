using System;
using System.Collections.Generic;
using MessagePack;

namespace Attri.Runtime
{
    // NOTE:ここのkeyの値がJSON形式で記述する際の型の識別子になる
    [Union(0, typeof(AttributeBase<int>))]
    [Union(1, typeof(AttributeBase<float>))]
    [Union(2, typeof(AttributeBase<string>))]
    [Union(3, typeof(IntAttribute))]
    [Union(4, typeof(FloatAttribute))]
    [Union(5, typeof(StringAttribute))]
    // [Union(6, typeof(FrameData<int>))]
    // [Union(7, typeof(FrameData<float>))]
    // [Union(8, typeof(FrameData<string>))]
    // [Union(9, typeof(Value<int>))]
    // [Union(10, typeof(Value<float>))]
    // [Union(11, typeof(Value<string>))]
    public interface IAttribute
    {
        public string Name();
        AttributeType GetAttributeType();
        int GetDimension();
        int FrameCount();
        Type GetDataType();
        List<List<object>> GetObjectFrame(int frameIndex);
        List<byte[]> GeByte(int frameIndex);
        int GetElementByteSize();
        List<List<List<object>>> GetObjectFrames();
        AttributeAsset CreateAsset();
        void DrawAttributeDetailInspector();
    }
}
