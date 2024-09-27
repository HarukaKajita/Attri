using System;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [Serializable]
    public abstract class AttributeBase<T> : IAttribute
    {
        public string name;
        public List<FrameData<T>> frames = new();
        public string Name()
        {
            return name;
        }
        public virtual AttributeType GetAttributeType()
        {
            return AttributeType.Unknown;
        }

        public int GetDimension()
        {
            if (frames == null) return 0;
            if (frames.Count == 0) return 0;
            return frames[0].GetElementDimension();
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
            for (var frameIndex = 0; frameIndex < frameCount; frameIndex++)
            {
                var frame = frames[frameIndex];
                var elementCount = frame.ElementCount().ToString();
                str += $", [{frameIndex}][{elementCount}]";
            }
                
            return str;
        }

        public int FrameCount()
        {
            return frames.Count;
        }
        
        public Type GetDataType()
        {
            return typeof(T);
        }

        public List<List<object>> GetObjectFrame(int frameIndex)
        {
            var elementCount = frames[frameIndex].ElementCount();
            var elementsList = new List<List<object>>(elementCount);
            var elements = frames[frameIndex].elements;
            // フレーム内の要素のループ
            for(var i = 0; i < elementCount; i++)
            {
                elementsList.Add(new List<object>(elements[i].Length));
                for (var j = 0; j < elements[i].Length; j++)
                {
                    // 要素の成分をobjectに変換してリスト化
                    elementsList[i].Add(elements[i][j]);
                }
            }
            return elementsList;
        }

        public abstract List<byte[]> GeByte(int frameIndex);
        public abstract int GetElementByteSize();

        public List<List<List<object>>> GetObjectFrames()
        {
            var frameList = new List<List<List<object>>>(FrameCount());
            // フレームループ
            for (var frameIndex = 0; frameIndex < FrameCount(); frameIndex++)
            {
                var frame = frames[frameIndex];
                var elementCount = frame.ElementCount();
                var elements = frame.elements;
                var elementsList = new List<List<object>>(elementCount);
                
                // フレーム内の要素のループ
                for(var i = 0; i < elementCount; i++)
                {
                    elementsList.Add(new List<object>(elements[i].Length));
                    for (var j = 0; j < elements[i].Length; j++)
                    {
                        // 要素の成分をobjectに変換してリスト化
                        elementsList[i].Add(elements[i][j]);
                    }
                }
                frameList.Add(elementsList);
            }
            return frameList;
        }
        public abstract AttributeAsset CreateAsset();
        internal void SerializeTest()
        {
            var bytes = AttributeSerializer.Serialize(this);
            var json = AttributeSerializer.ConvertToJson(bytes);
            Debug.Log(json);
            var attribute = MessagePackSerializer.Deserialize<IAttribute>(bytes, AttributeSerializer.options);
            Debug.Log(attribute.ToString());
        }
        public abstract void DrawAttributeDetailInspector();
    }
}
