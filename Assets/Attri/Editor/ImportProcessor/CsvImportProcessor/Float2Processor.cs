using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using Unity.Mathematics;
using UnityEditor.AssetImporters;
using UnityEngine;
using yutokun;

namespace Attri.Editor
{
    public class Float2Processor : CsvImportProcessor
    {
        private readonly List<Float2Container> _scriptableObjects = new();
        public Float2Processor() : this("Float2") { }
        public Float2Processor(string prefix) : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {
            var data = File.ReadLines(ctx.assetPath).ToList();
            // 1行目のヘッダーだけ読み飛ばす
            if (skipFirstLine) data.RemoveAt(0);
            // アセットの作成
            var container = ScriptableObject.CreateInstance<Float2Container>();
            container.name = $"{assetPrefix}";
            container.values = Parse(data);
            _scriptableObjects.Clear();
            _scriptableObjects.Add(container);
            // scriptableObjectsをsubAssetsに追加
            ctx.AddObjectToAsset($"{_scriptableObjects[0].name}_{GetHashCode()}", _scriptableObjects[0]);
            return _scriptableObjects.Cast<Object>().ToArray();
        }
        private List<float2> Parse(List<string> csvLines)
        {
            // 行を無視して一列にしてから,で分離
            var csvText = string.Join(",", csvLines);
            var sheet = CSVParser.LoadFromString(csvText).First();
            // float列を3つ毎に分離
            return sheet
                .Select(float.Parse).Select((v, i) => new { v, i })
                .GroupBy(x => x.i / 2)// 2つ毎にグループ化
                .Select(g => new float2(g.ElementAt(0).v, g.ElementAt(1).v)).ToList();
        }
        
    }
}