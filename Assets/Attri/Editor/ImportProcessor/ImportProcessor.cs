using System;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Attri.Runtime
{
    [Serializable]
    public abstract class ImportProcessor//: IImportProcessor
    {
        public bool enabled = true;
        internal string assetPath;
        [SerializeField, Delayed]
        internal string assetPrefix;
        internal List<IAttribute> attributes = new();
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
        public List<IAttribute> GetAttributes() => attributes;
        public void SetAttributes(List<IAttribute> attributes)
        {
            this.attributes = attributes;
        }

        internal virtual Object[] RunProcessor(AssetImportContext ctx)
        {
            return Array.Empty<Object>();
        }
    }
    // public interface IImportProcessor
    // {
    //     void SetAssetPrefix(string prefix);
    //     string GetAssetPrefix();
    //     void SetAssetPath(string path);
    //     string GetAssetPath();
    //     List<IAttribute> GetAttributes();
    //     void SetAttributes(List<IAttribute> attributes);
    //     Object[] RunProcessor(AssetImportContext ctx);
    // }
}