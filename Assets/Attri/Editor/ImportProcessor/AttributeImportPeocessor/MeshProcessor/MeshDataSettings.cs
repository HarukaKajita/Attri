using System;
using System.Collections.Generic;
using System.Linq;
using Attri.Runtime;
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

        public IReadOnlyList<AttributeSelection> Selections()
        {
            return new[]
            {
                positionSelection, normalSelection, tangentSelection, colorSelection, uv0Selection, uv1Selection,
                uv2Selection, uv3Selection, uv4Selection, uv5Selection, uv6Selection, uv7Selection
            };
        }
        

        public VertexAttributeDescriptor[] MakeVertexBufferParams(out int totalByteSizePerVertex)
        {
            var selections = Selections();
            var validSelections = selections.Where(s => s.IsValid()).ToArray();
            var vertexAttributeDescriptors = validSelections.Select(s => s.MakeVertexAttributeDescriptor()).ToArray();
            
            totalByteSizePerVertex = 0;
            foreach (var selection in validSelections)
                totalByteSizePerVertex += selection.ByteSizePerVertex();
            
            return vertexAttributeDescriptors;
        }
        
        // 指定した名前を持つアトリビュートを検索してAttributeSelectionに設定する
        public void MapAttributeSelections(IReadOnlyList<IAttribute> attributes)
        {
            foreach (var selection in Selections())
            {
                if (!selection.IsValid()) continue;
                var attribute = attributes.FirstOrDefault(a => a.Name() == selection.fetchAttributeName);
                if (attribute == null) throw new Exception($"Attribute not found: {selection.fetchAttributeName}");
                selection.SelectedAttribute = attribute;
            }
        }

        public byte[] FetchVertexDataBytes(IReadOnlyList<IAttribute> attributes)
        {
            MapAttributeSelections(attributes.ToList());
            var positionBytes = FetchPositionBytes();
            return positionBytes;
        }
        public byte[] FetchPositionBytes()
        {
            // メッシュデータ設定が無効な場合は空のリストを返す
            var selection = positionSelection;
            if (!selection.IsValid()) return Array.Empty<byte>();
            
            // positionに指定したアトリビュートが存在しない場合はエラー
            // var positionAttribute = attributes.FirstOrDefault(a => a.Name() == selection.fetchAttributeName);
            var positionAttribute = positionSelection.SelectedAttribute;
            if (positionAttribute == null)
                throw new Exception($"Attribute not found: {selection.fetchAttributeName}");
            
            // アトリビュートのフォーマットがFloat16かFloat32でない場合はエラー
            var format = selection.format;
            if (format != VertexAttributeFormat.Float16 && format != VertexAttributeFormat.Float32)
                throw new Exception($"Unsupported format for position vertex attribute: {format}");
            
            // アトリビュートの型がFloatでない場合はエラー
            if　(positionAttribute.GetAttributeType() != AttributeType.Float)
                throw new Exception($"Attribute type must be Float: {positionAttribute.GetAttributeType()}");
            
            // アトリビュートの要素数が足りない場合はエラー
            if(positionAttribute.GetDimension() < selection.dimension)
                throw new Exception($"Attribute dimension must be greater-equal than selection dimension: {positionAttribute.GetDimension()} < {selection.dimension}");
            
            // アトリビュートの値列をbyteに変換する。設定を次第でHalfに変換してからbyteに変換する。
            var cast2Half = format == VertexAttributeFormat.Float16;
            var floatAttribute = (FloatAttribute)positionAttribute;
            var positions = floatAttribute.frames[0].elements;
            var positionBytes = new byte[positions.Length * selection.dimension * (cast2Half ? 2 : 4)];
            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                var copyOffset = i * selection.dimension * (cast2Half ? 2 : 4);
                for (var j = 0; j < selection.dimension; j++)
                {
                    var value = position.components[j];
                    if (cast2Half)
                    {
                        var half = Mathf.FloatToHalf(value);
                        var halfBytes = BitConverter.GetBytes(half);
                        Buffer.BlockCopy(halfBytes, 0, positionBytes, copyOffset+j*2, 2);
                    }
                    else
                    {
                        var floatBytes = BitConverter.GetBytes(value);
                        Buffer.BlockCopy(floatBytes, 0, positionBytes, copyOffset+j*4, 4);
                    }
                }
            }
            return positionBytes;
        }
    }
}