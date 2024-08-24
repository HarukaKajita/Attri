using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
    [Serializable]
    public class MeshProcessor :AttributeImportProcessor
    {
        [SerializeField, HideInInspector] List<Mesh> _meshes = new();
        [SerializeField]
        MeshDataSettings _meshDataSettings = new();

        public MeshProcessor() : this("Mesh") { }
        public MeshProcessor(string prefix = "Mesh") : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {
            Debug.Log($"{GetType().Name}.RunProcessor()");
            _meshes.Clear();
            
            return _meshes.Cast<Object>().ToArray();
        }
    }
}