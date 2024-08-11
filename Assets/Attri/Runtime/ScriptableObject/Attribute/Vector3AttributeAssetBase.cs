using System.Collections.Generic;
using MessagePack;
using UnityEngine;

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [AttributeType(AttributeType.Float, 3)]
    [CreateAssetMenu(fileName = nameof(Vector3AttributeAssetBase), menuName = "Attri/ScriptableObject/Vector3Attribute")]
    public class Vector3AttributeAssetBase : AttributeAssetBase
    {
        public List<FrameData<Vector3>> values = new();

        public Vector3AttributeAssetBase() : base(AttributeType.Float, 3)
        {
        }
        public Vector3AttributeAssetBase(string name, AttributeType attributeType, ushort dimension) : base(attributeType, dimension)
        {
            this.name = name;
        }
        public override string ToString()
        {
            var str = base.ToString();
            foreach (var list in values)
            {
                str += $"Values: [{list.data.Count}]";
                foreach (var value in list.data)
                {
                    str += $"({value})";
                }
            }
            return str;
        }

        [ContextMenu("Serialize")]
        private void Serialize()
        {
            var bytes = AttributeSerializer.Serialize(this);
            var json = AttributeSerializer.ConvertToJson(bytes);
            Debug.Log(json);
        }
    }
}
