using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class FrameData<T>
    {
        public List<T> data = new();
        public FrameData() {}
        public FrameData(List<T> data)
        {
            this.data = data;
        }
        
        // FrameData<T>が持つべき機能を追加する
        public void Add(T value)
        {
            data.Add(value);
        }
        public void Remove(T value)
        {
            data.Remove(value);
        }
        public void Clear()
        {
            data.Clear();
        }
        public void SetData(List<T> data)
        {
            this.data = data;
        }
        public List<T> GetData()
        {
            return data;
        }
        public int Count()
        {
            return data.Count;
        }
    }
}
