using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using Csv;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Serialization;

namespace Attri.Editor
{
    [ScriptedImporter(1, ".attricsv")]
    public class CsvImporter : ScriptedImporter
    {
        //TODO これだとインスペクタに表示されない
        [SerializeField] private readonly List<AttributeImportProcessor> _processors = new();
        [SerializeField] private bool skipFirstLine = true;
        [SerializeField] private AttributeContainer attributeContainer;
        
        
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var data = File.ReadLines(ctx.assetPath).ToList();
            // 1行目のヘッダーだけ読み飛ばす
            if (skipFirstLine) data.RemoveAt(0);
            
            // アトリビュートの初期値推測
            // 仮でアトリビュート名=ファイル名
            if(string.IsNullOrEmpty(attributeContainer.name)) attributeContainer.name = Path.GetFileNameWithoutExtension(ctx.assetPath);
            // 要素数は","の数+1次元
            if(attributeContainer.dimension == 0) attributeContainer.dimension = (ushort)(data[0].Split(',').Length+1);
            // CSVで記述された状態のアトリビュート列
            attributeContainer.valueString = new List<string>();
            
            var isFloat = attributeContainer is { type: AttributeType.Real, signed: true };
            var isInt = attributeContainer is { type: AttributeType.Integer, signed: true };
            var isString = attributeContainer is { type: AttributeType.String };

            // アトリビュートの値を読んで隙に処理するプロセッサーにデータを渡す
            foreach (var parser in _processors)
            {
                parser.Parse(ctx, data);
            }
            // if (isString)
            // {
            //     //csvの中身を読み込む
            //     foreach (var line in data)
            //     {
            //         var values = CsvSerializer.Deserialize<string>(line).ToArray();
            //     }    
            // }
            //
            //
            //
            // //csvの中身を読み込む
            // foreach (var line in data)
            // {
            //     // var values = CsvSerializer.Deserialize<Vector3>(line);
            //     attributeContainer.valueString.Add(line);
            // }
            
        }
    }
}
