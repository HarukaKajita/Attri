using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using Newtonsoft.Json;
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
            _attributeAssets.Clear();
            var jsonText = File.ReadAllText(ctx.assetPath);
            // MessagePackSerializer.Deserialize<List<PackedAttribute>>( MessagePackSerializer.Serialize(jsonText));
            var packedAttributes = JsonSerializer.CreateDefault().Deserialize<List<PackedAttribute>>(new JsonTextReader(new StringReader(jsonText)));
            // _packedAttributes = JsonUtility.FromJson<List<PackedAttribute>>(jsonText);
            foreach (var packedAttribute in packedAttributes)
            {
                var asset = AttributeAssetBase.CreateInstance(packedAttribute);   
                // packedAttribute.attributeType
                // var asset = ScriptableObject.CreateInstance(assetType);
                // asset.SetFromPackedAttribute(packedAttribute);
                _attributeAssets.Add(asset);
                ctx.AddObjectToAsset($"{asset.name}_{GetHashCode()}", asset);
            }
                
            return _attributeAssets.Cast<Object>().ToArray();
        }
        
        
    }
}
