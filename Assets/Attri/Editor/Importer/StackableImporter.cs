using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
    public class StackableImporter<T> : ScriptedImporter where T : ImportProcessor
    {
        [SerializeReference]
        public List<T> processors = new();
        private List<Object> _subAssets = new();
        [SerializeReference, HideInInspector]
        internal List<IAttribute> attributes = new();

        protected void OnValidate()
        {
            Debug.Log($"{this.GetType().Name}.OnValidate()");
            Init(false);
        }
        private void Init(bool forceDeserialization = false)
        {
            Debug.Log($"{this.GetType().Name}.Init(forceDeserialization:{forceDeserialization})");
            if (forceDeserialization)
            {
                var jsonText = File.ReadAllText(assetPath);
                byte[] data = File.ReadAllBytes(assetPath);
                // Debug.Log(jsonText);
                var extension = Path.GetExtension(assetPath);
                if (extension is ".json" or ".attrijson") 
                    data = AttributeSerializer.ConvertFromJson(jsonText);
                attributes = AttributeSerializer.DeserializeAsArray(data).ToList();
            }
            
            // Debug.Log($"\t{this.GetType().Name}.Init() : attributes[{attributes.Count}]");
            // Debug.Log($"\t{this.GetType().Name}.Init() : processors[{processors.Count}]");
            foreach (var processor in processors)
            {
                if (processor == null) continue;
                processor.SetAssetPath(assetPath);
                processor.SetAttributes(attributes);
            }
        }
        
        public override void OnImportAsset(AssetImportContext ctx)
        {
            Debug.Log($"{this.GetType().Name}.OnImportAsset()");
            Init(true);
            // Mainアセットだけ設定
            MakeMainAsset(ctx);

            // 任意の前処理
            PreProcess(ctx);
            
            // 任意の処理を走らせる為のProcessorを実行
            _subAssets = RunProcessors(ctx).ToList();
            
            // 任意の後処理
            PostProcess(ctx);
            
        }
        
        protected virtual void MakeMainAsset(AssetImportContext ctx)
        {
            var assetName  = Path.GetFileNameWithoutExtension(ctx.assetPath);
            var subAssetContainer = ScriptableObject.CreateInstance<SubAssetContainer>();
            subAssetContainer.name = assetName;
            ctx.AddObjectToAsset(assetName, subAssetContainer);
            ctx.SetMainObject(subAssetContainer);
        }
        
        protected virtual Object[] RunProcessors(AssetImportContext ctx)
        {
            var subAssets = new List<Object>();
            foreach (var processor in processors)
                subAssets.AddRange(processor?.RunProcessor(ctx) ?? Array.Empty<Object>());
            return subAssets.ToArray();
        }
        
        protected virtual void PreProcess(AssetImportContext ctx)
        {
            // 任意の前処理
        }
        protected virtual void PostProcess(AssetImportContext ctx)
        {
            // 任意の後処理
            var mainAsset = ctx.mainObject as SubAssetContainer;
            if(mainAsset == null) return;
            foreach (var subAsset in _subAssets)
                mainAsset.subAssets.Add(subAsset);
            
        }
    }
}
