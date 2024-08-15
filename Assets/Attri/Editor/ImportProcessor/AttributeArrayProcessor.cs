using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
    public class AttributeArrayProcessor : ImportProcessor
    {
        [SerializeField] public bool CompressUnitVector = false;
        [SerializeField] public bool CompressFloat = false;
        [SerializeField] public ushort FloatPrecisionBit = 3;
        [SerializeField] public bool CompressInteger = false;
        List<ScriptableObject> _attributeAssets = new();
        public AttributeArrayProcessor() { }
        public AttributeArrayProcessor(string prefix) : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {
            // _attributeAssets.Clear();
            // var jsonText = File.ReadAllText(ctx.assetPath);
            // byte[] data = File.ReadAllBytes(ctx.assetPath);
            // Debug.Log(jsonText);
            // if (Path.GetExtension(ctx.assetPath) == ".json") 
            //     data = AttributeSerializer.ConvertFromJson(jsonText);
            //
            // var attributes = AttributeSerializer.DeserializeAsArray(data);
            // var asset = ScriptableObject.CreateInstance<AttributeAsset>();
            // asset.attributes = attributes;
            // asset.name = assetPrefix;
            // foreach (var a in attributes)
            // {
            //     Debug.Log(a.ToString());
            // }
            // _attributeAssets.Add(asset);
            // ctx.AddObjectToAsset($"{asset.name}_{asset.GetHashCode()}", asset);
            // return _attributeAssets.Cast<Object>().ToArray();
            return new Object[] { };
        }
        
        
    }
}
