using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;
using UnityEngine;
using yutokun;

namespace Attri.Editor
{
    public class FloatProcessor : CsvImportProcessor
    {
        private readonly List<Container> _scriptableObjects = new();
        public FloatProcessor() : this("Float") { }
        public FloatProcessor(string prefix) : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {   
            var data = File.ReadLines(ctx.assetPath).ToList();
            // 1行目のヘッダーだけ読み飛ばす
            if (skipFirstLine) data.RemoveAt(0);
            // アセットの作成
            var container = ScriptableObject.CreateInstance<Container>();
            container.SetValues(Parse(data));
            _scriptableObjects.Clear();
            _scriptableObjects.Add(container);
            // scriptableObjectsをsubAssetsに追加
            ctx.AddObjectToAsset($"{_scriptableObjects[0].name}_{GetHashCode()}", _scriptableObjects[0]);
            return _scriptableObjects.Cast<Object>().ToArray();
        }
        
        private float[][] Parse(List<string> csvLines)
        {
            var valueList = new List<float[]>();
            foreach (var lineStr in csvLines)
            {
                var line = CSVParser.LoadFromString(lineStr).First();
                var value = line.Select(float.Parse).ToArray();
                valueList.Add(value);
            }

            return valueList.ToArray();
        }
    }
}
