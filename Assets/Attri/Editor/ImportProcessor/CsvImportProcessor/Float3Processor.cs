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
    public class Float3Processor : CsvImportProcessor
    {
        [SerializeField] bool skipFirstLine = false;
        private readonly List<Float3Container> _scriptableObjects = new();
        public Float3Processor() : this("Float3") { }
        public Float3Processor(string prefix) : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {
            var data = File.ReadLines(ctx.assetPath).ToList();
            // 1行目のヘッダーだけ読み飛ばす
            if (skipFirstLine) data.RemoveAt(0);
            // アセットの作成
            var vector3ArrayScriptableObject = ScriptableObject.CreateInstance<Float3Container>();
            vector3ArrayScriptableObject.name = $"{assetPrefix}";
            vector3ArrayScriptableObject.values = Parse(data);
            _scriptableObjects.Clear();
            _scriptableObjects.Add(vector3ArrayScriptableObject);
            // scriptableObjectsをsubAssetsに追加
            ctx.AddObjectToAsset($"{_scriptableObjects[0].name}_{GetHashCode()}", _scriptableObjects[0]);
            return _scriptableObjects.Cast<Object>().ToArray();
        }
        private List<float3> Parse(List<string> csvLines)
        {
            // 行を無視して一列にしてから,で分離
            var csvText = string.Join(",", csvLines);
            var sheet = CSVParser.LoadFromString(csvText).First();
            // float列を3つ毎に分離
            return sheet
                .Select(float.Parse).Select((v, i) => new { v, i })
                .GroupBy(x => x.i / 3)// 3つ毎にグループ化
                .Select(g => new float3(g.ElementAt(0).v, g.ElementAt(1).v, g.ElementAt(2).v)).ToList();
        }
        
    }
}