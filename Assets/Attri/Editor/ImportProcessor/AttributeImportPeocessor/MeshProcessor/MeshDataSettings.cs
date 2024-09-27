using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace Attri.Editor
{
    [Serializable]
    public class MeshDataSettings
    {
        [SerializeField, SerializeReference] public AttributeSelection positionSelection = new(VertexAttribute.Position, VertexAttributeFormat.Float16, "P");
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

        public VertexAttributeDescriptor[] MakeVertexBufferParams(out int totalByteSizePerVertex)
        {
            var vertexAttributeDescriptors = new List<VertexAttributeDescriptor>();
            if(positionSelection.IsValid()) vertexAttributeDescriptors.Add(positionSelection.MakeVertexAttributeDescriptor());
            if(normalSelection.IsValid())   vertexAttributeDescriptors.Add(normalSelection.MakeVertexAttributeDescriptor());
            if(colorSelection.IsValid())    vertexAttributeDescriptors.Add(colorSelection.MakeVertexAttributeDescriptor());
            if(tangentSelection.IsValid())  vertexAttributeDescriptors.Add(tangentSelection.MakeVertexAttributeDescriptor());
            if(uv0Selection.IsValid()) vertexAttributeDescriptors.Add(uv0Selection.MakeVertexAttributeDescriptor());
            if(uv1Selection.IsValid()) vertexAttributeDescriptors.Add(uv1Selection.MakeVertexAttributeDescriptor());
            if(uv2Selection.IsValid()) vertexAttributeDescriptors.Add(uv2Selection.MakeVertexAttributeDescriptor());
            if(uv3Selection.IsValid()) vertexAttributeDescriptors.Add(uv3Selection.MakeVertexAttributeDescriptor());
            if(uv4Selection.IsValid()) vertexAttributeDescriptors.Add(uv4Selection.MakeVertexAttributeDescriptor());
            if(uv5Selection.IsValid()) vertexAttributeDescriptors.Add(uv5Selection.MakeVertexAttributeDescriptor());
            if(uv6Selection.IsValid()) vertexAttributeDescriptors.Add(uv6Selection.MakeVertexAttributeDescriptor());
            if(uv7Selection.IsValid()) vertexAttributeDescriptors.Add(uv7Selection.MakeVertexAttributeDescriptor());
            
            totalByteSizePerVertex = 0;
            if(positionSelection.IsValid()) totalByteSizePerVertex += positionSelection.ByteSizePerVertex();
            if(normalSelection.IsValid())   totalByteSizePerVertex += normalSelection.ByteSizePerVertex();
            if(colorSelection.IsValid())    totalByteSizePerVertex += colorSelection.ByteSizePerVertex();
            if(tangentSelection.IsValid())  totalByteSizePerVertex += tangentSelection.ByteSizePerVertex();
            if(uv0Selection.IsValid()) totalByteSizePerVertex += uv0Selection.ByteSizePerVertex();
            if(uv1Selection.IsValid()) totalByteSizePerVertex += uv1Selection.ByteSizePerVertex();
            if(uv2Selection.IsValid()) totalByteSizePerVertex += uv2Selection.ByteSizePerVertex();
            if(uv3Selection.IsValid()) totalByteSizePerVertex += uv3Selection.ByteSizePerVertex();
            if(uv4Selection.IsValid()) totalByteSizePerVertex += uv4Selection.ByteSizePerVertex();
            if(uv5Selection.IsValid()) totalByteSizePerVertex += uv5Selection.ByteSizePerVertex();
            if(uv6Selection.IsValid()) totalByteSizePerVertex += uv6Selection.ByteSizePerVertex();
            if(uv7Selection.IsValid()) totalByteSizePerVertex += uv7Selection.ByteSizePerVertex();
            
            return vertexAttributeDescriptors.ToArray();            
        }
    }
}