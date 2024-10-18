using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Attri.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Attri.Editor
{
    public class CompressedFloatView : AnalysisView
    {
        private const string VisualTreeAssetPath = "Assets/Attri/Editor/Analysis/CompressedFloatData.uxml";
        private readonly SliderInt _precision;
        private readonly MultiColumnListView _listView;
        private readonly Toggle _compressToggle;
        
        protected override string VisualTreePath() => VisualTreeAssetPath;
        public CompressedFloatView(IDataProvider dataProvider) : base(dataProvider)
        {
            _listView = Self.Q<MultiColumnListView>("List");
            _compressToggle = Self.Q<Toggle>("Compress");
            _precision = Self.Q<SliderInt>("Precision");
            _precision.value = 23;
            _precision.RegisterValueChangedCallback(OnPrecisionChanged);
            _compressToggle.RegisterValueChangedCallback(OnCompressToggleChanged);
            _listView.visible = false;
        }

        private void OnCompressToggleChanged(ChangeEvent<bool> evt)
        {
            DebugLog($"{GetType().Name}.OnCompressToggleChanged");
            UpdateView();
        }

        private void OnPrecisionChanged(ChangeEvent<int> evt)
        {
            DebugLog($"{GetType().Name}.OnPrecisionChanged");
            UpdateView();
        }
        public override void UpdateView()
        {
            DebugLog($"{GetType().Name}.UpdateListView()");
            _listView.Clear();
            if (!_compressToggle.value || _dataProvider == null)
            {
                _listView.visible = false;
                return;
            }
            
            // 圧縮前後の値を比較
            var originalElements = _dataProvider.AsFloat();
            var precision = _precision.value;
            var compressor = new FloatCompressor(originalElements, precision);
            var comparer = new CompressedFloatComparer(compressor.OriginalComponents, compressor.Compress());
            DebugLog($"Original:{originalElements.Length}x{originalElements[0].Length} Precision:{precision}");
            // Viewの更新
            var items = comparer.diffComponents;
            _listView.visible = true;
            _listView.itemsSource = items;
            _listView.columns["name"].bindCell = (e, i) => MakeLabel((Label)e, $"[{i}]");
            _listView.columns["num"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].Length));
            _listView.columns["min"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.diffMin[i]));
            _listView.columns["max"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.diffMax[i]));
            _listView.columns["std"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.diffStd[i]));
            _listView.columns["range"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.diffRange[i]));
            _listView.columns["center"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.diffMid[i]));
            _listView.columns["average"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.diffAve[i]));
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
