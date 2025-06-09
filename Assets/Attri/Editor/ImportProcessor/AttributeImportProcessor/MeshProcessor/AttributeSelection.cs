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
        public bool enabled = false;
        [SerializeField]
        internal VertexAttribute attribute;
        public VertexAttributeFormat format;
        [Range(1, 4)]
        public int dimension = 4;
        public virtual int[] GetDimensionArray()
        {
            return format.GetValidDimensionArray();
        }
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
            dimension = attribute.GetDefaultDimension();
        }

        public bool IsValid()
        {
            if(!enabled) return false;
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
