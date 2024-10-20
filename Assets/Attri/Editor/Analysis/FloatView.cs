using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Attri.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Attri.Editor
{
    public class FloatView : AnalysisView
    {
        private const string VisualTreeAssetPath = "Assets/Attri/Editor/Analysis/FloatView.uxml";
        
        private readonly MultiColumnListView _listView;
        
        protected override string VisualTreePath() => VisualTreeAssetPath;
        public FloatView(IDataProvider dataProvider) : base(dataProvider)
        {
            _listView = Self.Q<MultiColumnListView>("List");
            _listView.visible = false;
        }
        public override void UpdateView()
        {
            DebugLog($"{GetType().Name}.UpdateListView()");
            _listView.Clear();
            if (_dataProvider == null)
            {
                _listView.visible = false;
                return;
            }
            
            // 値を分析
            var originalComponents = _dataProvider.AsFloat();
            var floatAnalysis = new FloatAnalysisData(originalComponents);
            // Viewの更新
            var items = floatAnalysis.componentsAnalysisData;
            _listView.visible = true;
            _listView.itemsSource = items;
            _listView.columns["name"].bindCell = (e, i) => MakeLabel((Label)e, $"[{i}]");
            _listView.columns["num"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].elementNum));
            _listView.columns["min"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].min));
            _listView.columns["max"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].max));
            _listView.columns["std"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].std));
            _listView.columns["range"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].range));
            _listView.columns["center"].bindCell = (e, i) => MakeLabel((Label)e, Str((items[i].min+items[i].max)/2f));
            _listView.columns["signed"].bindCell = (e, i) => MakeLabel((Label)e, items[i].signed ? "Signed" : "Unsigned");
            _listView.columns["expRange"].bindCell = (e, i) => MakeLabel((Label)e, $"{items[i].exponentRange} ({items[i].minExponent}~{items[i].maxExponent})");
            _listView.columns["exponentBitDepth"].bindCell = (e, i) => MakeLabel((Label)e, items[i].exponentBitDepth.ToString());
            _listView.RefreshItems();
            DebugLog($"UpdateListView() End");
        }
        void MakeLabel(Label label, string text)
        {
            label.text = text;
            label.style.unityTextAlign = TextAnchor.MiddleRight;
        }
        private static string Str(float value) => value.ToString(CultureInfo.InvariantCulture);
    }
}
