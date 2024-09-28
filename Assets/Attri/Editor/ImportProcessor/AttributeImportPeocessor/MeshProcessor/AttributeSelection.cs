using System;
using Attri.Runtime;
using UnityEngine;
using UnityEngine.Rendering;

namespace Attri.Editor
{
    [Serializable]
    public class AttributeSelection
    {
        [SerializeField]
        internal VertexAttribute attribute;
        public VertexAttributeFormat format;
        [Range(1, 4)]
        public int dimension = 3;//[1,4]
        // [HideInInspector]
        // public int index = 0;
        [Delayed]
        public string fetchAttributeName;
        
        [HideInInspector]
        public IAttribute SelectedAttribute;
        
        public string AttributeName()
        {
            return attribute.ToString();
        }

        public AttributeSelection(VertexAttribute attribute, VertexAttributeFormat format, string fetchAttributeName)
        {
            this.attribute = attribute;
            this.format = format;
            this.fetchAttributeName = fetchAttributeName;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(fetchAttributeName);
        }
        public VertexAttributeDescriptor MakeVertexAttributeDescriptor()
        {
            return new VertexAttributeDescriptor(attribute, format, dimension);
        }
        public int ByteSizePerVertex()
        {
            return format.AttributeFormatByteSize() * dimension;
        }
    }
}
