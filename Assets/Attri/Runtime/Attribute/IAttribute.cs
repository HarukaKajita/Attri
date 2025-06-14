using System;
using System.Collections.Generic;

namespace Attri.Runtime
{
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
