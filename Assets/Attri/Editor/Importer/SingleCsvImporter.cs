using System;
using System.IO;
using System.Linq;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Attri.Editor
{
    [ScriptedImporter(1,new[] {"singleassetcsv"}, new[] { "csv", "attricsv" })]
    public class SingleCsvImporter : ScriptedImporter
    {
        [SerializeReference] private CsvImportProcessor processor;

        private Description _description;
        
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var lines = File.ReadLines(ctx.assetPath).ToList();
            var descriptionLine = lines[0];
            if (processor != null)
            {
                processor.SetAssetPath(ctx.assetPath);
                processor.RunProcessor(ctx);
                return;
            }
            
            //初回インポート時のみ自動でDescriptionを取得してProcessorを設定する
            _description = new Description(descriptionLine);
            if (_description != null)
            {
                // Debug.Log($"Description: {descriptionLine}");
                // Debug.Log($"Description: {_description}");
                processor = _description.GetProcessor();
                processor.assetPrefix = Path.GetFileNameWithoutExtension(ctx.assetPath);
                processor.skipFirstLine = true;
            }
            else
            {
                // とりあえずインスペクタから確認だけでもできるようにStringProcessorをデフォルトにしておく
                processor = new StringProcessor();
                processor.assetPrefix = Path.GetFileNameWithoutExtension(ctx.assetPath);
                processor.skipFirstLine = false;
            }
            
            processor.SetAssetPath(ctx.assetPath);
            processor.RunProcessor(ctx);
        }   
    }
    class Description
    {
        private readonly Type _type;
        private readonly ushort _size;
        private readonly bool _isArray;
        public string Custom;//任意のメッセージを含められる。今のところ使ってない
        public override string ToString()
        {
            return $"Type:{_type},Size:{_size},IsArray:{_isArray},Custom:{Custom}";
        }
        //"Description:True","Type:Float","Size:3","IsArray:False","Custom:任意のメッセージ"  こんな感じで書かれた行を想定
        public Description(string descriptionLine)
        {
            if (string.IsNullOrEmpty(descriptionLine)) return;
            if (!descriptionLine.Contains("Description")) return;
            if (descriptionLine.Contains("Description:False")) return;
            
            var parts = descriptionLine.Split(',');
            foreach (var part in parts)
            {
                var keyValue = part.Split(':');
                if (keyValue.Length != 2) continue;
                
                var key = keyValue[0];
                var value = keyValue[1];
                
                switch (key)
                {
                    case "Type":
                    {
                        if (value.ToLower() == "int") _type = typeof(int);
                        if (value.ToLower() == "float") _type = typeof(float);
                        if (value.ToLower() == "string") _type = typeof(string);
                        break;
                    }
                    case "Size":
                        _size = ushort.Parse(value);
                        break;
                    case "IsArray":
                        _isArray = value == "True";
                        break;
                    case "Custom":
                        Custom = value;
                        break;
                }
            }
        }
        public CsvImportProcessor GetProcessor()
        {
            if (_type == typeof(int))
                return new IntProcessor();
            if (_type == typeof(float))
                return new FloatProcessor();
            if (_type == typeof(string))
                return new StringProcessor();
            throw new NotImplementedException("intとfloatとstring以外の配列は未実装です");
        }
    }
}
