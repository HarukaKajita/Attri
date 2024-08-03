using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;
using UnityEngine;
using yutokun;

namespace Attri.Editor
{
    public class FloatArrayProcessor : ImportProcessor
    {
        [SerializeField] bool skipFirstLine = true;
        private readonly List<FloatArray> _scriptableObjects = new();
        public FloatArrayProcessor() { }
        public FloatArrayProcessor(string prefix) : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {   
            var data = File.ReadLines(ctx.assetPath).ToList();
            // 1行目のヘッダーだけ読み飛ばす
            if (skipFirstLine) data.RemoveAt(0);
            // アセットの作成
            var floatArrayScriptableObject = ScriptableObject.CreateInstance<FloatArray>();
            floatArrayScriptableObject.name = $"{assetPrefix}";
            floatArrayScriptableObject.values = ParseFloats(data);
            _scriptableObjects.Clear();
            _scriptableObjects.Add(floatArrayScriptableObject);
            // scriptableObjectsをsubAssetsに追加
            ctx.AddObjectToAsset($"{_scriptableObjects[0].name}_{GetHashCode()}", _scriptableObjects[0]);
            return _scriptableObjects.Cast<Object>().ToArray();
        }
        
        private List<float> ParseFloats(List<string> csvLines)
        {
            // 行を無視して一列にしてから,で分離
            var csvText = string.Join(",", csvLines);
            var sheet = CSVParser.LoadFromString(csvText).First();
            return sheet.Select(float.Parse).ToList();
        }
    }
}
