using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Attri.Editor
{
    [Serializable]
    public class MeshDataSettings
    {
        [SerializeField, SerializeReference] public AttributeSelection positionSelection =  new(VertexAttribute.Position, VertexAttributeFormat.Float16, "P");
        [SerializeField, SerializeReference] public AttributeSelection normalSelection   = new(VertexAttribute.Normal, VertexAttributeFormat.Float16, "N");
        [SerializeField, SerializeReference] public AttributeSelection tangentSelection  = new(VertexAttribute.Tangent, VertexAttributeFormat.Float16, "tangent");
        [SerializeField, SerializeReference] public AttributeSelection colorSelection    = new(VertexAttribute.Color, VertexAttributeFormat.UNorm8 , "Cd");
        [SerializeField, SerializeReference] public AttributeSelection uv0Selection = new(VertexAttribute.TexCoord0, VertexAttributeFormat.UNorm8 , "uv");
        [SerializeField, SerializeReference] public AttributeSelection uv1Selection = new(VertexAttribute.TexCoord1, VertexAttributeFormat.UNorm8 , "uv1");
        [SerializeField, SerializeReference] public AttributeSelection uv2Selection = new(VertexAttribute.TexCoord2, VertexAttributeFormat.UNorm8 , "uv2");
        [SerializeField, SerializeReference] public AttributeSelection uv3Selection = new(VertexAttribute.TexCoord3, VertexAttributeFormat.UNorm8 , "uv3");
        [SerializeField, SerializeReference] public AttributeSelection uv4Selection = new(VertexAttribute.TexCoord4, VertexAttributeFormat.UNorm8 , "uv4");
        [SerializeField, SerializeReference] public AttributeSelection uv5Selection = new(VertexAttribute.TexCoord5, VertexAttributeFormat.UNorm8 , "uv5");
        [SerializeField, SerializeReference] public AttributeSelection uv6Selection = new(VertexAttribute.TexCoord6, VertexAttributeFormat.UNorm8 , "uv6");
        [SerializeField, SerializeReference] public AttributeSelection uv7Selection = new(VertexAttribute.TexCoord7, VertexAttributeFormat.UNorm8 , "uv7");
        public List<string> subMeshIndexList = new();
    }
}