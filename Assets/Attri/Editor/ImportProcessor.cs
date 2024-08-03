using System;
using UnityEditor.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Attri.Runtime
{
    [Serializable]
    public abstract class ImportProcessor
    {
        [SerializeField]
        protected string assetPrefix;
        protected ImportProcessor(string prefix = "")
        {
            assetPrefix = string.IsNullOrEmpty(prefix) ? GetType().Name : prefix;
        }
        internal virtual Object[] RunProcessor(AssetImportContext ctx)
        {
            return Array.Empty<Object>();
        }
    }
}