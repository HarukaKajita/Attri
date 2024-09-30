using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;
using UnityEngine;
using yutokun;

namespace Attri.Editor
{
    public class FloatListProcessor : CsvImportProcessor
    {
        private readonly List<FloatListContainer> _scriptableObjects = new();
        public FloatListProcessor() : this("Float List") { }
        public FloatListProcessor(string prefix) : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {   
            var data = File.ReadLines(ctx.assetPath).ToList();
            if (skipFirstLine) data.RemoveAt(0);
            // アセットの作成
            var container = ScriptableObject.CreateInstance<FloatListContainer>();
            container.name = $"{assetPrefix}";
            container.elements = Parse(data);
            _scriptableObjects.Clear();
            _scriptableObjects.Add(container);
            // scriptableObjectsをsubAssetsに追加
            ctx.AddObjectToAsset($"{_scriptableObjects[0].name}_{GetHashCode()}", _scriptableObjects[0]);
            return _scriptableObjects.Cast<Object>().ToArray();
        }
        
        private List<ListWrapper<float>> Parse(List<string> csvLines)
        {
            var values = new List<ListWrapper<float>>();
            foreach (var lineStr in csvLines)
            {
                var line = CSVParser.LoadFromString(lineStr).First();
                values.Add(line.Select(float.Parse).ToList());
            }

            return values;
        }
    }
}
