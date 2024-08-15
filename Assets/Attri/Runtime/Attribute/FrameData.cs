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
        //indexerの実装
        // public T this[int index]
        // {
        //     get => data[index];
        //     set => data[index] = value;
        // }
        public int Count()
        {
            return data.Count;
        }

        public virtual T Max()
        {
            return data.Max(e => e);
        }
        public virtual T Min()
        {
            return data.Min(e => e);
        }
        // public virtual T Average()
        // {
        //     return data.Sum(e => e) / data.Count;
        // }
        // public virtual int[] Distribution()
        // {
        //     var result = new int[10];
        //     var max = data.Max(e => e);
        //     var min = data.Min(e => e);
        //     var interval = (max - min) / 10;
        //     foreach (var e in data)
        //     {
        //         var index = (int)((e - min) / interval);
        //         result[index]++;
        //     }
        //     return result;
        // }
    }
}
