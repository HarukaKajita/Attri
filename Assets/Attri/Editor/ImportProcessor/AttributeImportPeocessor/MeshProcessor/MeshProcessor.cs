using System;
using System.Collections.Generic;
using System.Linq;
using Attri.Runtime;
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
            var vertexAttributeBytes = _meshDataSettings.FetchVertexDataBytes(attributes);
            Debug.Log($"vertexAttributeBytes.Length: {vertexAttributeBytes.Length}");
            Debug.Log($"byteSizePerVertex: {byteSizePerVertex}");
            Debug.Log($"positionSelection byteSize: {_meshDataSettings.positionSelection.ByteSizePerVertex()}");
            Debug.Log($"positionSelection dimension: {_meshDataSettings.positionSelection.dimension}");
            Debug.Log($"normalSelection byteSize: {_meshDataSettings.normalSelection.ByteSizePerVertex()}");
            Debug.Log($"normalSelection dimension: {_meshDataSettings.normalSelection.dimension}");
            Debug.Log($"vertexAttributeBytes.Length / byteSizePerVertex: {vertexAttributeBytes.Length / byteSizePerVertex}");
            var vertexCount = vertexAttributeBytes.Length / byteSizePerVertex;
            mesh.SetVertexBufferParams(vertexCount, vertexAttributeDescriptors.ToArray());
            mesh.SetIndexBufferParams(vertexCount,  vertexCount < 65535 ? IndexFormat.UInt16 : IndexFormat.UInt32);
            VertexDataUtility.SetVertexData(mesh, vertexAttributeBytes, vertexCount);
            mesh.bounds = CalculateBounds();
            SetIndex(mesh);
            //TODO: IDを一意で不変にする
            ctx.AddObjectToAsset($"{mesh.name}_", mesh);
            return new Object[]{mesh};
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