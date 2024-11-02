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
        private const string VisualTreeAssetPath = "Assets/Attri/Editor/Analysis/CompressedFloatView.uxml";
        private readonly Toggle _compressToggle;
        private readonly SliderInt _precision;
        private readonly MultiColumnListView _listView;
        
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
            var originalData = _dataProvider.AsFloat();
            var precision = _precision.value;
            DebugLog($"Original:{originalData.Length}x{originalData[0].Length}x{originalData[0][0].Length} Precision:{precision}");
            var compressor = new FloatCompressor(originalData, precision);
            var compressed = compressor.Compress();
            DebugLog($"Compressed:{compressed.Length}x{compressed[0].Length}x{compressed[0][0].Length} Precision:{precision}");
            var comparer = new FloatComparer(compressor.Original, compressed);
            // Viewの更新
            var items = comparer.diff;
            var frameId = 0;
            var targetItems = items[frameId].Transpose();
            _listView.visible = true;
            _listView.itemsSource = targetItems;
            _listView.columns["name"].bindCell = (e, compoId) => MakeLabel((Label)e, $"[{compoId}]");
            _listView.columns["num"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(targetItems[compoId].Length));
            _listView.columns["min"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.diffMin[frameId][compoId]));
            _listView.columns["max"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.diffMax[frameId][compoId]));
            _listView.columns["std"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.diffStd[frameId][compoId]));
            _listView.columns["range"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.diffRange[frameId][compoId]));
            _listView.columns["center"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.diffMid[frameId][compoId]));
            _listView.columns["average"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.diffAve[frameId][compoId]));
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
