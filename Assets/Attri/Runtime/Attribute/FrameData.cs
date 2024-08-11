using System;
using System.Collections.Generic;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class FrameData<T>
    {
        public List<T> data = new();
    }
}
