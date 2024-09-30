using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;
using UnityEngine;
using yutokun;

namespace Attri.Editor
{
    public class IntListProcessor : CsvImportProcessor
    {
        private readonly List<IntListContainer> _scriptableObjects = new();
        public IntListProcessor() : this("Int List") { }
        public IntListProcessor(string prefix) : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {   
            var data = File.ReadLines(ctx.assetPath).ToList();
            if (skipFirstLine) data.RemoveAt(0);
            // アセットの作成
            var container = ScriptableObject.CreateInstance<IntListContainer>();
            container.name = $"{assetPrefix}";
            container.elements = Parse(data);
            _scriptableObjects.Clear();
            _scriptableObjects.Add(container);
            // scriptableObjectsをsubAssetsに追加
            ctx.AddObjectToAsset($"{_scriptableObjects[0].name}_{GetHashCode()}", _scriptableObjects[0]);
            return _scriptableObjects.Cast<Object>().ToArray();
        }
        
        private List<ListWrapper<int>> Parse(List<string> csvLines)
        {
            var values = new List<ListWrapper<int>>();
            foreach (var lineStr in csvLines)
            {
                var line = CSVParser.LoadFromString(lineStr).First();
                values.Add(line.Select(int.Parse).ToList());
            }

            return values;
        }
    }
}
