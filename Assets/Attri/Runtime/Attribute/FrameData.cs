using System;

namespace Attri.Runtime
{
    public class FrameData<T>
    {
        public Value<T>[] elements;
        public FrameData() : this(Array.Empty<Value<T>>()) {}
        public FrameData(Value<T>[] elements)
        {
            this.elements = elements;
        }
        public int ElementCount()
        {
            // フレームに依って要素数が異なる場合を許容したいので、フレーム指定で要素数を取得する
            return elements.Length;
        }
        public int GetElementDimension()
        {
            if (elements.Length == 0) return 0;
            return elements[0].Length;
        }
    }
}
