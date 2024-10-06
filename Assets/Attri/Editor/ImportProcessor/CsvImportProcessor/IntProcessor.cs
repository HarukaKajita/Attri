using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;
using UnityEngine;
using yutokun;

namespace Attri.Editor
{
    public class IntProcessor : CsvImportProcessor
    {
        private readonly List<Container> _scriptableObjects = new();
        public IntProcessor() : this("Int") { }
        public IntProcessor(string prefix) : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {   
            var data = File.ReadLines(ctx.assetPath).ToList();
            if (skipFirstLine) data.RemoveAt(0);
            // アセットの作成
            var container = ScriptableObject.CreateInstance<Container>();
            container.name = $"{assetPrefix}";
            container.SetValues(Parse(data));
            _scriptableObjects.Clear();
            _scriptableObjects.Add(container);
            // scriptableObjectsをsubAssetsに追加
            ctx.AddObjectToAsset($"{_scriptableObjects[0].name}_{GetHashCode()}", _scriptableObjects[0]);
            return _scriptableObjects.Cast<Object>().ToArray();
        }
        
        private int[][] Parse(List<string> csvLines)
        {
            var valueList = new List<int[]>();
            foreach (var lineStr in csvLines)
            {
                var line = CSVParser.LoadFromString(lineStr).First();
                var value = line.Select(int.Parse).ToArray();
                valueList.Add(value);
            }

            return valueList.ToArray();
        }
    }
}
