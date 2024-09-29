using System;
using UnityEditor.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Attri.Runtime
{
    [Serializable]
    public abstract class ImportProcessor
    {
        public bool enabled = true;
        internal string assetPath;
        [SerializeField, Delayed]
        internal string assetPrefix;
        
        protected ImportProcessor(string prefix = "")
        {
            assetPrefix = string.IsNullOrEmpty(prefix) ? GetType().Name : prefix;
        }
        public void SetAssetPrefix(string prefix)
        {
            this.assetPrefix = prefix;
        }
        public string GetAssetPrefix() => assetPrefix;
        public void SetAssetPath(string path)
        {
            assetPath = path;
        }
        public string GetAssetPath() => assetPath;
        
        internal virtual Object[] RunProcessor(AssetImportContext ctx)
        {
            return Array.Empty<Object>();
        }
    }
}