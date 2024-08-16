using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
    public class StackableImporter<T> : ScriptedImporter where T : ImportProcessor
    {
        [SerializeReference, SubclassSelector]
        public List<T> processors = new();
        private List<Object> _subAssets = new();
        
        public override void OnImportAsset(AssetImportContext ctx)
        {
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
