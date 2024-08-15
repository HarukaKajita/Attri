using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{             
    // [Union(1, typeof(AttributeBase<int>))]
    // [Union(2, typeof(AttributeBase<float>))]
    // [Union(3, typeof(AttributeBase<bool>))]
    // [Union(4, typeof(AttributeBase<string>))]
    // [Union(5, typeof(AttributeBase<Vector3>))]
    // [Union(6, typeof(AttributeBase<Vector3Int>))]
    // [Union(7, typeof(AttributeBase<Vector2>))]
    // [Union(8, typeof(AttributeBase<Vector2Int>))]
    // [Union(9, typeof(IntegerAttribute))]
    // [Union(10, typeof(FloatAttribute))]
    // [Union(11, typeof(BoolAttribute))]
    // [Union(12, typeof(StringAttribute))]
    // [Union(13, typeof(Vector3Attribute))]
    // [Union(14, typeof(Vector3IntAttribute))]
    // [Union(15, typeof(Vector2Attribute))]
    // [Union(16, typeof(Vector2IntAttribute))]
    // [MessagePackObject(true)]
    [Serializable]
    public class AttributeBase<T> : IAttribute
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
            var str = $"{GetAttributeType().ToString()}{GetDimension()}({typeof(T)}) {name}[{frameCount}]";
            for (var i = 0; i < frameCount; i++)
                str += $", [{i}][{frames[i].data.Count}]";
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

        public List<object> GetObjectFrame(int index)
        {
            return frames[index].data.Cast<object>().ToList();
        }

        public List<List<object>> GetObjectFrames()
        {
            return frames.Select(frame => frame.data.Cast<object>().ToList()).ToList();
        }

        public virtual void DrawAttributeDetailInspector()
        {
            // 継承先で実装
        }

        internal void SerializeTest()
        {
            var bytes = AttributeSerializer.Serialize(this);
            var json = AttributeSerializer.ConvertToJson(bytes);
            Debug.Log(json);
            var attribute = MessagePackSerializer.Deserialize<IAttribute>(bytes, AttributeSerializer.options);
            Debug.Log(attribute.ToString());
        }

        internal void PrintDetail(IAttribute attribute)
        {
            if(attribute == null)
                return;
            Debug.Log(attribute.ToString());
            //
            // switch (attribute.GetType())
            // {
            //     case Type t when t == typeof(IntegerAttribute):
            //         Debug.Log(((IntegerAttribute)attribute).ToString());
            //         break;
            //     case Type t when t == typeof(FloatAttribute):
            //         Debug.Log(((FloatAttribute)attribute).ToString());
            //         break;
            //     case Type t when t == typeof(BoolAttribute):
            //         Debug.Log(((BoolAttribute)attribute).ToString());
            //         break;
            //     case Type t when t == typeof(StringAttribute):
            //         Debug.Log(((StringAttribute)attribute).ToString());
            //         break;
            //     case Type t when t == typeof(Vector3Attribute):
            //         Debug.Log(((Vector3Attribute)attribute).ToString());
            //         break;
            //     case Type t when t == typeof(Vector3IntAttribute):
            //         Debug.Log(((Vector3IntAttribute)attribute).ToString());
            //         break;
            //     case Type t when t == typeof(Vector2Attribute):
            //         Debug.Log(((Vector2Attribute)attribute).ToString());
            //         break;
            //     case Type t when t == typeof(Vector2IntAttribute):
            //         Debug.Log(((Vector2IntAttribute)attribute).ToString());
            //         break;
            //     default:
            //         Debug.Log($"Unknown: {attribute.GetType().Name}, {attribute.Name()}, {attribute.GetAttributeType()}[{attribute.GetDimension()}]");
            //         break;
            // }
        }
    }
}
