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
            var frameAnalysisData = floatAnalysis.frameAnalysisData;
            var franeId = 0;
            var targetItems = frameAnalysisData[franeId].componentsAnalysisData;
            _listView.visible = true;
            _listView.itemsSource = targetItems;
            _listView.columns["name"].bindCell = (e, compoId) => MakeLabel((Label)e, $"[{compoId}]");
            _listView.columns["num"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(targetItems[compoId].elementNum));
            _listView.columns["min"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(targetItems[compoId].min));
            _listView.columns["max"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(targetItems[compoId].max));
            _listView.columns["std"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(targetItems[compoId].std));
            _listView.columns["range"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(targetItems[compoId].range));
            _listView.columns["center"].bindCell = (e, compoId) => MakeLabel((Label)e, Str((targetItems[compoId].min+targetItems[compoId].max)/2f));
            _listView.columns["signed"].bindCell = (e, compoId) => MakeLabel((Label)e, targetItems[compoId].signed ? "Signed" : "Unsigned");
            _listView.columns["expRange"].bindCell = (e, compoId) => MakeLabel((Label)e, $"{targetItems[compoId].exponentRange} ({targetItems[compoId].minExponent}~{targetItems[compoId].maxExponent})");
            _listView.columns["exponentBitDepth"].bindCell = (e, compoId) => MakeLabel((Label)e, targetItems[compoId].exponentBitDepth.ToString());
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
