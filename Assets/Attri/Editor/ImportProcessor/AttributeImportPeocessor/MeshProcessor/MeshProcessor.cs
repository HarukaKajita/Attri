using System;
using System.Collections.Generic;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
    [Serializable]
    public class MeshProcessor :AttributeImportProcessor
    {
        [SerializeField, HideInInspector] Mesh mesh;
        [SerializeField]
        MeshDataSettings _meshDataSettings = new();

        public MeshProcessor() : this("Mesh") { }
        public MeshProcessor(string prefix = "Mesh") : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {
            Debug.Log($"{GetType().Name}.RunProcessor()");
            var mesh = new Mesh();
            mesh.name = assetPrefix;
            SetPosition(mesh);
            SetIndex(mesh);
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mesh.RecalculateTangents();
            ctx.AddObjectToAsset($"{mesh.name}_{GetHashCode()}", mesh);
            return new Object[]{mesh};
        }

        private void SetPosition(Mesh mesh)
        {
            var selection = _meshDataSettings.positionSelection;
            if (!selection.IsValid()) return;
            
            var positionAttribute = attributes.FirstOrDefault(a => a.Name() == _meshDataSettings.positionSelection.fetchAttributeName);
            if (positionAttribute == null)
                throw new Exception($"Attribute not found: {_meshDataSettings.positionSelection.fetchAttributeName}");
                
            if (selection.dimension == 3)
            {
                var positions = (Vector3Attribute)positionAttribute;
                var data = positions.frames.First().data;
                mesh.SetVertices(data);
            }
        }
        private void SetIndex(Mesh mesh)
        {
            var subMeshIndexList = _meshDataSettings.subMeshIndexList;
            if (subMeshIndexList == null || subMeshIndexList.Count == 0) return;
            
            for (var i = 0; i < subMeshIndexList.Count; i++)
            {
                var attributeName = subMeshIndexList[i];
                var attribute = attributes.FirstOrDefault(a => a.Name() == attributeName);
                if (attribute == null)
                    throw new Exception($"Attribute not found: {attributeName}");
                var dimension = attribute.GetDimension();
                var topology = MeshTopology.Triangles;
                if (dimension == 1)
                    topology = MeshTopology.Points;
                else if (dimension == 2)
                    topology = MeshTopology.Lines;
                else if (dimension == 3)
                    topology = MeshTopology.Triangles;
                else if (dimension == 4)
                    topology = MeshTopology.Quads;
                int[] index = {};
                if (dimension == 1)
                    index = attribute.GetObjectFrame(0).Cast<int>().ToArray();
                else if (dimension == 2)
                    index = attribute.GetObjectFrame(0).Cast<Vector2Int>().SelectMany(v => new int[]{v.x, v.y}).ToArray();
                else if (dimension == 3)
                    index = attribute.GetObjectFrame(0).Cast<Vector3Int>().SelectMany(v => new int[]{v.z, v.y, v.x}).ToArray();
                else if (dimension == 4)
                    index = attribute.GetObjectFrame(0).Cast<int[]>().SelectMany(v=>v.Reverse()).ToArray();
                    
                mesh.SetIndices(index, topology, i);
            }
        }
    }
}