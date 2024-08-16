using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
    public class AttributeAssetProcessor : AttributeImportProcessor
    {
        [SerializeField] public bool CompressUnitVector = false;
        [SerializeField] public bool CompressFloat = false;
        [SerializeField] public ushort FloatPrecisionBit = 3;
        [SerializeField] public bool CompressInteger = false;
        List<AttributeAsset> _attributeAssets = new();
        public AttributeAssetProcessor() : this("AttributeAsset") { }
        public AttributeAssetProcessor(string prefix) : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {
            _attributeAssets.Clear();
            var jsonText = File.ReadAllText(ctx.assetPath);
            byte[] data = File.ReadAllBytes(ctx.assetPath);
            Debug.Log(jsonText);
            var extension = Path.GetExtension(ctx.assetPath);
            if (extension is ".json" or ".attrijson") 
                data = AttributeSerializer.ConvertFromJson(jsonText);
            
            var attributes = AttributeSerializer.DeserializeAsArray(data);
            foreach (var a in attributes)
            {
                var asset = a.CreateAsset();
                asset.name = $"{assetPrefix}_{asset.name}";
                Debug.Log(a.ToString());
                _attributeAssets.Add(asset);
                ctx.AddObjectToAsset($"{asset.name}_{asset.GetHashCode()}", asset);
            }
            return _attributeAssets.Cast<Object>().ToArray();
        }
        
        
    }
}
