using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using Attri.Runtime.Resolvers;
using MessagePack;
using MessagePack.Resolvers;
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
            byte[] data = File.ReadAllBytes(ctx.assetPath);
            Debug.Log(jsonText);
            if (Path.GetExtension(ctx.assetPath) == ".json") 
                data = AttributeSerializer.ConvertFromJson(jsonText);
            
            // var allBytes = File.ReadAllBytes(ctx.assetPath);
            // var attributes = MessagePackSerializer.Deserialize<List<AttributeAssetBase>>(allBytes);
            
            // var attributes = MessagePackSerializer.Deserialize<dynamic>(allBytes, options);
            // var attributes = MessagePackSerializer.Deserialize<AttributeAssetBase>(allBytes, options);
            var attributes = AttributeSerializer.DeserializeAsArray(data);
            // var packedAttributes = JsonSerializer.CreateDefault().Deserialize<List<PackedAttribute>>(new JsonTextReader(new StringReader(jsonText)));
            // _packedAttributes = JsonUtility.FromJson<List<PackedAttribute>>(jsonText);
            
            // foreach (var packedAttribute in packedAttributes)
            // {
            //     Debug.Log(packedAttribute.ToString());
            //     var asset = AttributeAssetBase.CreateInstance(packedAttribute);   
            //     // packedAttribute.attributeType
            //     // var asset = ScriptableObject.CreateInstance(assetType);
            //     // asset.SetFromPackedAttribute(packedAttribute);
            //     _attributeAssets.Add(asset);
            //     ctx.AddObjectToAsset($"{asset.name}_{GetHashCode()}", asset);
            // }
            foreach (var a in attributes)
            {
                Debug.Log(a.ToString());
                _attributeAssets.Add(a);
                ctx.AddObjectToAsset($"{a.name}_{a.GetHashCode()}", a);
            }
            
            return _attributeAssets.Cast<Object>().ToArray();
        }
        
        
    }
}
