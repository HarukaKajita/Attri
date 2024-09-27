using System;
using System.Collections.Generic;
using System.Linq;
using Attri.Runtime;
using Unity.Collections;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
    [Serializable]
    public class MeshProcessor :AttributeImportProcessor
    {
        [SerializeField, HideInInspector] Mesh mesh;
        [SerializeField]
        MeshDataSettings _meshDataSettings = new();
        [SerializeField]
        private Vector3 boundsExtents = Vector3.zero;
        
        public MeshProcessor() : this("Mesh") { }
        public MeshProcessor(string prefix = "Mesh") : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {
            Debug.Log($"{GetType().Name}.RunProcessor()");
            var mesh = new Mesh();
            mesh.name = assetPrefix;
            var vertexAttributeDescriptors = _meshDataSettings.MakeVertexBufferParams(out var byteSizePerVertex);
            var vertexAttributeBytes = new List<byte>(byteSizePerVertex);
            foreach (var attributeDescriptor in vertexAttributeDescriptors)
            {
                var format = attributeDescriptor.attribute; 
                if(format == VertexAttribute.Position)       vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.Normal)    vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.Tangent)   vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.Color)     vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.TexCoord0) vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.TexCoord1) vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.TexCoord2) vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.TexCoord3) vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.TexCoord4) vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.TexCoord5) vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.TexCoord6) vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.TexCoord7) vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.BlendWeight)  vertexAttributeBytes.AddRange(FetchPositionBytes());
                else if(format == VertexAttribute.BlendIndices) vertexAttributeBytes.AddRange(FetchPositionBytes());
                else throw new ArgumentOutOfRangeException();
            }
            var vertexCount = vertexAttributeBytes.Count / byteSizePerVertex;
            mesh.SetVertexBufferParams(vertexCount, vertexAttributeDescriptors.ToArray());
            mesh.SetIndexBufferParams(vertexCount,  vertexCount < 65535 ? IndexFormat.UInt16 : IndexFormat.UInt32);
            VertexDataUtility.SetVertexData(mesh, vertexAttributeBytes.ToArray(), vertexCount);
            SetIndex(mesh);
            mesh.bounds = CalculateBounds();
            mesh.RecalculateNormals();
            // mesh.RecalculateTangents();
            //TODO: IDを一意で不変にする
            ctx.AddObjectToAsset($"{mesh.name}_", mesh);
            return new Object[]{mesh};
        }
        
        private byte[] FetchPositionBytes()
        {
            // メッシュデータ設定が無効な場合は空のリストを返す
            var selection = _meshDataSettings.positionSelection;
            if (!selection.IsValid()) return Array.Empty<byte>();
            
            // positionに指定したアトリビュートが存在しない場合はエラー
            var positionAttribute = attributes.FirstOrDefault(a => a.Name() == _meshDataSettings.positionSelection.fetchAttributeName);
            if (positionAttribute == null)
                throw new Exception($"Attribute not found: {_meshDataSettings.positionSelection.fetchAttributeName}");
            
            // アトリビュートのフォーマットがFloat16かFloat32でない場合はエラー
            var format = _meshDataSettings.positionSelection.format;
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
        private Bounds CalculateBounds()
        {
            var bounds = new Bounds();
            var positionAttribute = attributes.FirstOrDefault(a => a.Name() == _meshDataSettings.positionSelection.fetchAttributeName);
            if (positionAttribute == null)
                throw new Exception($"Attribute not found: {_meshDataSettings.positionSelection.fetchAttributeName}");
            if (positionAttribute.GetAttributeType() != AttributeType.Float)
                throw new Exception($"Attribute type must be Float: {positionAttribute.GetAttributeType()}");
            
            // Boundsの中心とサイズを計算する
            var floatAttribute = (FloatAttribute)positionAttribute;
            var values = floatAttribute.frames[0];
            var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            foreach (var value in values.elements)
            {
                for (var i = 0; i < value.components.Length; i++)
                {
                    min[i] = Mathf.Min(min[i], value.components[i]);
                    max[i] = Mathf.Max(max[i], value.components[i]);
                }
            }
            bounds.center = (min + max) / 2;
            var size = max - min + boundsExtents;
            // boundsのサイズが0以下の場合は最低限のサイズを設定する
            for (var axis = 0; axis < 3; axis++)
                if (size[axis] <= 0) size[axis] = 0.001f;
            bounds.size = size;
            
            return bounds;
        }
        private void SetIndex(Mesh targetMesh)
        {
            var subMeshIndexList = _meshDataSettings.subMeshIndexList;
            if (subMeshIndexList == null || subMeshIndexList.Count == 0) return;
            
            for (var i = 0; i < subMeshIndexList.Count; i++)
            {
                var attributeName = subMeshIndexList[i];
                var attribute = attributes.FirstOrDefault(a => a.Name() == attributeName);
                if (attribute == null)
                    throw new Exception($"Attribute not found: {attributeName}");
                var intAttribute = (IntAttribute)attribute;
                
                var dimension = intAttribute.GetDimension();
                var topology = dimension switch
                {
                    1 => MeshTopology.Points,
                    2 => MeshTopology.Lines,
                    3 => MeshTopology.Triangles,
                    4 => MeshTopology.Quads,
                    _ => throw new Exception($"Unsupported dimension: {dimension}")
                };
                var isTriangleOrQuad = topology is MeshTopology.Triangles or MeshTopology.Quads;
                var index = intAttribute.frames[0].elements.SelectMany(v => isTriangleOrQuad ? v.components.Reverse() : v.components).ToArray();
                targetMesh.SetIndices(index, topology, i);
            }
        }
    }
}