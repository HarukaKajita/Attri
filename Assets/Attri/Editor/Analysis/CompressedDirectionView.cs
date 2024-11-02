using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Attri.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Attri.Editor
{
    public class CompressedDirectionView : AnalysisView
    {
        private const string VisualTreeAssetPath = "Assets/Attri/Editor/Analysis/CompressedDirectionView.uxml";
        private readonly Toggle _compressToggle;
        private readonly SliderInt _precision;
        private readonly MultiColumnListView _listView;
        
        protected override string VisualTreePath() => VisualTreeAssetPath;
        public CompressedDirectionView(IDataProvider dataProvider) : base(dataProvider)
        {
            _listView = Self.Q<MultiColumnListView>("List");
            _compressToggle = Self.Q<Toggle>("Compress");
            _precision = Self.Q<SliderInt>("Precision");
            _precision.value = 24;
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
            var compressor = new DirectionCompressor(originalElements, precision);
            var comparer = new DirectionComparer(compressor.OriginalVectors, compressor.Compress());
            DebugLog($"Original:{originalElements.Length}x{originalElements[0].Length} Precision:{precision}");
            // Viewの更新
            var franeId = 0;
            _listView.visible = true;
            _listView.itemsSource =  new List<DirectionComparer>{comparer};//dummy
            _listView.columns["num"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.DiffDegrees[franeId].Length));
            _listView.columns["min"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.DiffMin[franeId]));
            _listView.columns["max"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.DiffMax[franeId]));
            _listView.columns["std"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.DiffStd[franeId]));
            _listView.columns["range"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.DiffRange[franeId]));
            _listView.columns["average"].bindCell = (e, i) => MakeLabel((Label)e, Str(comparer.DiffAve[franeId]));
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
