using System.Collections.Generic;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
    public class AttributeAssetProcessor : AttributeImportProcessor
    {
        // [SerializeField] public bool CompressUnitVector = false;
        // [SerializeField] public bool CompressFloat = false;
        // [SerializeField] public ushort FloatPrecisionBit = 3;
        // [SerializeField] public bool CompressInteger = false;
        [SerializeField, HideInInspector] List<AttributeAsset> _attributeAssets = new();
        public AttributeAssetProcessor() : this("AttributeAsset") { }
        public AttributeAssetProcessor(string prefix = "AttributeAsset") : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {
            // Debug.Log($"{GetType().Name}.RunProcessor()");
            _attributeAssets.Clear();
            foreach (var a in attributes)
            {
                var asset = a.CreateAsset();
                asset.name = $"{assetPrefix}_{asset.name}";
                _attributeAssets.Add(asset);
                ctx.AddObjectToAsset($"{asset.name}_{asset.GetHashCode()}", asset);
            }
            return _attributeAssets.Cast<Object>().ToArray();
        }
    }
}
