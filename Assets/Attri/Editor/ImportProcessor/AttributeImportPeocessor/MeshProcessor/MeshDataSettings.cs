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
        [SerializeField, SerializeReference] public AttributeSelection positionSelection = new (VertexAttribute.Position , VertexAttributeFormat.Float32, "P");
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
            
            foreach (var selection in validSelections)
                Debug.Log($"selection.attribute : {selection.attribute}");
            
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
            var bytes = new List<byte>();
            var positionBytes = FetchPositionBytes(out var positionCount, out var positionSize);
            var normalBytes   = FetchNormalBytes(out var normalCount, out var normalSize);
            Debug.Log($"positionBytes.Length: {positionBytes.Length}");
            Debug.Log($"normalBytes.Length: {normalBytes.Length}");
            var vertexCount = Mathf.Max(positionCount, normalCount);
            Debug.Log($"vertexCount: {vertexCount}");
            for (var vertexId = 0; vertexId < vertexCount; vertexId++)
            {
                for (var byteId = 0; byteId < positionSize; byteId++)
                    bytes.Add(positionBytes[vertexId * positionSize + byteId]);
                for (var byteId = 0; byteId < normalSize; byteId++)
                    bytes.Add(normalBytes[vertexId * normalSize + byteId]);
            }
            Debug.Log($"bytes.Count: {bytes.Count}");
            return bytes.ToArray();
        }
        public byte[] FetchPositionBytes(out int vertexCount, out int vertexSize)
        {
            // メッシュデータ設定が無効な場合は空のリストを返す
            var selection = positionSelection;
            vertexCount = 0;
            vertexSize = 0;
            if (!selection.IsValid()) return Array.Empty<byte>();
            
            // 指定した名前のアトリビュートが存在しない場合はエラー
            var attribute = selection.SelectedAttribute;
            if (attribute == null) throw new Exception($"Attribute not found: {selection.fetchAttributeName}");
            
            // アトリビュートのフォーマットがFloat16かFloat32でない場合はエラー
            var format = selection.format;
            if (format != VertexAttributeFormat.Float16 && format != VertexAttributeFormat.Float32)
                throw new Exception($"Unsupported format for {selection.attribute}: {format}");
            
            // アトリビュートの型がFloatでない場合はエラー
            if　(attribute.GetAttributeType() != AttributeType.Float)
                throw new Exception($"Attribute type must be Float: {attribute.GetAttributeType()}");
            
            var lackComponentCount = selection.dimension - attribute.GetDimension();
            // アトリビュートの値列をbyteに変換する。設定を次第でHalfに変換してからbyteに変換する。
            var cast2Half = format == VertexAttributeFormat.Float16;
            var floatAttribute = (FloatAttribute)attribute;
            var positions = floatAttribute.frames[0].elements;
            var values = positions.SelectMany(
                p =>
                {
                    if (lackComponentCount == 0) return p.components;
                    if (lackComponentCount < 0)  return p.components.Take(selection.dimension);
                    // 不足する成分は0で埋める
                    var paddingComponents = Enumerable.Repeat(0f, lackComponentCount);
                    return p.components.Take(selection.dimension).Concat(paddingComponents);
                }).ToArray();
            var bytes = new byte[values.Length * (cast2Half ? 2 : 4)];
            for (var i = 0; i < values.Length; i++)
            {
                if (cast2Half)
                {
                    var half = Mathf.FloatToHalf(values[i]);
                    var halfBytes = BitConverter.GetBytes(half);
                    Buffer.BlockCopy(halfBytes, 0, bytes, i*2, 2);
                }
                else
                {
                    var floatBytes = BitConverter.GetBytes(values[i]);
                    Buffer.BlockCopy(floatBytes, 0, bytes, i*4, 4);
                }
            }
            vertexCount = positions.Length;
            vertexSize = bytes.Length / vertexCount;
            return bytes;
        }
        
        public byte[] FetchNormalBytes(out int normalCount, out int normalSize)
        {
            // メッシュデータ設定が無効な場合は空のリストを返す
            var selection = normalSelection;
            normalCount = 0;
            normalSize = 0;
            if (!selection.IsValid()) return Array.Empty<byte>();
            
            // 指定した名前のアトリビュートが存在しない場合はエラー
            var attribute = selection.SelectedAttribute;
            if (attribute == null) throw new Exception($"Attribute not found: {selection.fetchAttributeName}");
            
            // アトリビュートのフォーマットがFloat16かFloat32でない場合はエラー
            var format = selection.format;
            if (format != VertexAttributeFormat.Float16 && format != VertexAttributeFormat.Float32)
                throw new Exception($"Unsupported format for {selection.attribute}: {format}");
            
            // アトリビュートの型がFloatでない場合はエラー
            if　(attribute.GetAttributeType() != AttributeType.Float)
                throw new Exception($"Attribute type must be Float: {attribute.GetAttributeType()}");
            
            var lackComponentCount = selection.dimension - attribute.GetDimension();
            // アトリビュートの値列をbyteに変換する。設定を次第でHalfに変換してからbyteに変換する。
            var cast2Half = format == VertexAttributeFormat.Float16;
            var floatAttribute = (FloatAttribute)attribute;
            var normals = floatAttribute.frames[0].elements;
            var values = normals.SelectMany(
                p =>
                {
                    if (lackComponentCount == 0) return p.components;
                    if (lackComponentCount < 0) return p.components.Take(selection.dimension);
                    // 不足する成分は0で埋める
                    var paddingComponents = Enumerable.Repeat(0f, lackComponentCount);
                    return p.components.Take(selection.dimension).Concat(paddingComponents);
                }).ToArray();
            var bytes = new byte[values.Length * (cast2Half ? 2 : 4)];
            for (var i = 0; i < values.Length; i++)
            {
                if (cast2Half)
                {
                    var half = Mathf.FloatToHalf(values[i]);
                    var halfBytes = BitConverter.GetBytes(half);
                    Buffer.BlockCopy(halfBytes, 0, bytes, i*2, 2);
                }
                else
                {
                    var floatBytes = BitConverter.GetBytes(values[i]);
                    Buffer.BlockCopy(floatBytes, 0, bytes, i*4, 4);
                }
            }
            normalCount = normals.Length;
            normalSize = bytes.Length / normalCount;
            return bytes;
        }
    }
}