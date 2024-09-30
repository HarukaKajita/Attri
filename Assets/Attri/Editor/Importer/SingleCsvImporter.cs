using UnityEditor.AssetImporters;
using UnityEngine;

namespace Attri.Editor
{
    [ScriptedImporter(1,new[] {"singleassetcsv"}, new[] { "csv", "attricsv" })]
    public class SingleCsvImporter : ScriptedImporter
    {
        [SerializeReference] private CsvImportProcessor processor;
        public override void OnImportAsset(AssetImportContext ctx)
        {
            if (processor == null) return;
            // TODO:一行目に自動でProcessorの種類や設定を書くようにする
            // 
            // 設定を書いていない場合はnull
            processor.SetAssetPath(ctx.assetPath);
            processor.RunProcessor(ctx);
        }
    }
}
