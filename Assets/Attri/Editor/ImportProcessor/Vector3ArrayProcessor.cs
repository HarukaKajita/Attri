using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;
using UnityEngine;
using yutokun;

namespace Attri.Editor
{
    public class Vector3ArrayProcessor : ImportProcessor
    {
        [SerializeField] bool skipFirstLine = true;
        private readonly List<Vector3Array> _scriptableObjects = new();
        public Vector3ArrayProcessor() { }
        public Vector3ArrayProcessor(string prefix) : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {
            var data = File.ReadLines(ctx.assetPath).ToList();
            // 1行目のヘッダーだけ読み飛ばす
            if (skipFirstLine) data.RemoveAt(0);
            // アセットの作成
            var vector3ArrayScriptableObject = ScriptableObject.CreateInstance<Vector3Array>();
            vector3ArrayScriptableObject.name = $"{assetPrefix}";
            vector3ArrayScriptableObject.values = ParseVector3(data);
            _scriptableObjects.Clear();
            _scriptableObjects.Add(vector3ArrayScriptableObject);
            // scriptableObjectsをsubAssetsに追加
            ctx.AddObjectToAsset($"{_scriptableObjects[0].name}_{GetHashCode()}", _scriptableObjects[0]);
            return _scriptableObjects.Cast<Object>().ToArray();
        }
        private List<Vector3> ParseVector3(List<string> csvLines)
        {
            // 行を無視して一列にしてから,で分離
            var csvText = string.Join(",", csvLines);
            var sheet = CSVParser.LoadFromString(csvText).First();
            // float列を3つ毎に分離
            return sheet
                .Select(float.Parse).Select((v, i) => new { v, i })
                .GroupBy(x => x.i / 3)// 3つ毎にグループ化
                .Select(g => new Vector3(g.ElementAt(0).v, g.ElementAt(1).v, g.ElementAt(2).v)).ToList();
        }
        
    }
}