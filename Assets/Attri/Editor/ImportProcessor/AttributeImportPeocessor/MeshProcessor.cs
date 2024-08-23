using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
    [Serializable]
    class AttributeSelection
    {
        public VertexAttribute? Attribute { get; }
        public VertexAttributeFormat format;
        public int dimension = 3;//[1,4]
        [HideInInspector]
        public int index = 0;
        public string fetchAttributeName;
        
        public string AttributeName()
        {
            return Attribute== null ? "Index" : Attribute.Value.ToString();
        }

        public AttributeSelection(VertexAttribute? attribute, VertexAttributeFormat format, string fetchAttributeName)
        {
            this.Attribute = attribute;
            this.format = format;
            this.fetchAttributeName = fetchAttributeName;
        }
    }
    [Serializable]
    public class MeshProcessor :AttributeImportProcessor
    {
        [SerializeField, HideInInspector] List<Mesh> _meshes = new();
        
        // TODO: Support SubMesh
        [SerializeField, HideInInspector]
        internal AttributeSelection[] attributeSelections =
        {
            new (VertexAttribute.Position , VertexAttributeFormat.Float16, "P"),
            new (VertexAttribute.Normal   , VertexAttributeFormat.Float16, "N"),
            new (VertexAttribute.Tangent  , VertexAttributeFormat.Float16, "tangent"),
            new (VertexAttribute.Color    , VertexAttributeFormat.UNorm8 , "Cd"),
            new (VertexAttribute.TexCoord0, VertexAttributeFormat.UNorm8 , "uv"),
            new (VertexAttribute.TexCoord1, VertexAttributeFormat.UNorm8 , "uv1"),
            new (VertexAttribute.TexCoord2, VertexAttributeFormat.UNorm8 , "uv2"),
            new (VertexAttribute.TexCoord3, VertexAttributeFormat.UNorm8 , "uv3"),
            new (VertexAttribute.TexCoord4, VertexAttributeFormat.UNorm8 , "uv4"),
            new (VertexAttribute.TexCoord5, VertexAttributeFormat.UNorm8 , "uv5"),
            new (VertexAttribute.TexCoord6, VertexAttributeFormat.UNorm8 , "uv6"),
            new (VertexAttribute.TexCoord7, VertexAttributeFormat.UNorm8 , "uv7"),
            new (null, VertexAttributeFormat.UInt32, "index")
        };
        
        public MeshProcessor():this("Mesh") {}
        public MeshProcessor(string prefix = "Mesh") : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {
            Debug.Log($"{GetType().Name}.RunProcessor()");
            _meshes.Clear();
            
            return _meshes.Cast<Object>().ToArray();
        }
    }
}