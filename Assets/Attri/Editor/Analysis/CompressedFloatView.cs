using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Attri.Runtime;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

namespace Attri.Editor
{
    public class CompressedFloatView : AnalysisView
    {
        private const string VisualTreeAssetPath = "Assets/Attri/Editor/Analysis/CompressedFloatView.uxml";
        private readonly Toggle _compressToggle;
        private readonly SliderInt _precision;
        private readonly Toggle _compressPerContainerToggle;
        private readonly SliderInt _frameSlider;
        private readonly ObjectField _exportObjectField;
        private readonly Button _exportButton;
        private readonly MultiColumnListView _listView;
        FloatCompressor _compressor;
        
        protected override string VisualTreePath() => VisualTreeAssetPath;
        public CompressedFloatView(IDataProvider dataProvider) : base(dataProvider)
        {
            _listView = Self.Q<MultiColumnListView>("List");
            _listView.visible = false;
            _compressToggle = Self.Q<Toggle>("Compress");
            _precision = Self.Q<SliderInt>("Precision");
            _precision.value = 23;
            _precision.RegisterValueChangedCallback(OnPrecisionChanged);
            _compressToggle.RegisterValueChangedCallback(OnCompressToggleChanged);
            
            // PerContainer
            _compressPerContainerToggle = Self.Q<Toggle>("CompressPerContainer");
            _compressPerContainerToggle.RegisterValueChangedCallback(OnCompressToggleChanged);
            
            // FrameSlider
            _frameSlider = Self.Q<SliderInt>("FrameSlider");
            _frameSlider.highValue = dataProvider.AsFloat().Length - 1;
            _frameSlider.SetEnabled(false);
            _frameSlider.RegisterValueChangedCallback(OnFrameSliderChanged);
            
            // Export
            _exportObjectField = Self.Q<ObjectField>("ExportObject");
            _exportButton = Self.Q<Button>("ExportButton");
            _exportButton.clicked += OnExportButtonClicked;
        }

        private void OnExportButtonClicked()
        {
            DebugLog($"{GetType().Name}.OnExportButtonClicked");
            if (_dataProvider == null) return;
            if (_compressor == null) return;
            
            // 圧縮してデータを更新
            var compressPerFrame = _compressPerContainerToggle.value;
            var compressedData = _compressor.Compressed;
            var frameCount = compressedData.Length;
            
            // Sequenceの作成
            // CompressedContainerの作成
            // CompressionParamsObjectの作成
            // CompressedContainerまたはSequenceにCompressionParamsObjectを追加
            // CompressedContainerまたはSequenceに圧縮データを追加
            // 分岐 perFrame CreateOrUpdate
            var dstSequence = _exportObjectField.value as Sequence;
            var dstContainers = new CompressedContainer[frameCount];
            var dstCompressionParams = new List<CompressionParams>[frameCount];
            var originalSequenceAssetPath = AssetDatabase.GetAssetPath((ScriptableObject)_dataProvider);
            var needCreateAsset = dstSequence == null;
            if (needCreateAsset)
            {
                // dstSequenceの作成
                dstSequence = ScriptableObject.CreateInstance<Sequence>();
                var path = AssetDatabase.GetAssetPath((ScriptableObject)_dataProvider);
                path = path.Replace(".asset", "_Compressed.asset");
                AssetDatabase.CreateAsset(dstSequence, path);
                _exportObjectField.value = dstSequence;
            }
            // CompressedContainerの作成
            {
                var existingContainers = dstSequence.containers.Select(c=> c as CompressedContainer).Where(c=>c!= null).ToArray();
                Array.Copy(existingContainers, dstContainers, existingContainers.Length);
                var lackCount = frameCount - existingContainers.Length;
                // 足りない分を作成
                for (var i = 0; i < lackCount; i++)
                {
                    var newContainer = ScriptableObject.CreateInstance<CompressedContainer>();
                    AssetDatabase.CreateAsset(newContainer, containerPath);
                    dstContainers[existingContainers.Length + i] = newContainer;
                }
                // 多い分は削除
                var extraContainers = existingContainers.Skip(frameCount).ToArray();
                var extraContainerPaths = extraContainers.Select(AssetDatabase.GetAssetPath).ToList();
                extraContainerPaths.ForEach(p =>AssetDatabase.MoveAssetToTrash(p));
                // 名前の変更
                for (var i = 0; i < dstContainers.Length; i++)
                    dstContainers[i].name = $"{dstSequence.name}_Compressed_F{i}.asset";
                // Sequenceに追加
                dstSequence.containers = dstContainers.Cast<Container>().ToList();
            }
             // CompressionParamsObjectの作成
            if (compressPerFrame)
            {
                // [frame][component]
                var compressionParamsObjects = _compressor.MakeCompressionParamsObjectPerFrame();
                for (var frame = 0; frame < frameCount; frame++)
                {
                    var paramsObject = compressionParamsObjects[frame];
                    if (needCreateAsset)
                    {
                        var newContainer = ScriptableObject.CreateInstance<CompressedContainer>();
                        var path = AssetDatabase.GetAssetPath((ScriptableObject)_dataProvider);
                        path = path.Replace(".asset", $"_Compressed_F{frame}.asset");
                        AssetDatabase.CreateAsset(newContainer, path);
                        foreach (var param in paramsObject)
                            AssetDatabase.AddObjectToAsset(param, newContainer);
                    }
                    else
                    {
                        var container = ((Sequence)dstSequence).containers[frame] as CompressedContainer;
                        container.SetCompressionParams(paramsObject);
                    }
                }
                dstSequencePerFrame.Init(originalData[frame], compressedData[frame], compressionParamsObject);
                
            }
            else
            {
                // [component]
                var compressionParamsObject = _compressor.MakeCompressionParamsObject();
                dstSequence.Init(originalData, compressedData, compressionParamsObject);
            }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
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
        private void OnFrameSliderChanged(ChangeEvent<int> evt)
        {
            DebugLog($"{GetType().Name}.OnFrameSliderChanged");
            UpdateView();
        }
        public override void UpdateView()
        {
            DebugLog($"{GetType().Name}.UpdateListView()");
            _listView.Clear();
            var invalid = !_compressToggle.value || _dataProvider == null;
            if (invalid)
            {
                _listView.SetEnabled(false);
                _compressPerContainerToggle.SetEnabled(false);
                return;
            }
            _listView.SetEnabled(true);
            _compressPerContainerToggle.SetEnabled(true);
            
            // 圧縮前後の値を比較
            var originalData = _dataProvider.AsFloat();
            var precision = _precision.value;
            var frameId = _frameSlider.value;
            var perFrame = _compressPerContainerToggle.value;
            DebugLog($"Original[{frameId}]:{originalData[frameId].Length}x{originalData[frameId][0].Length} Precision:{precision}");
            _compressor = new FloatCompressor(originalData, precision);
            _compressor.Compress(perFrame);
            var compressed = _compressor.Compressed;
            DebugLog($"Compressed[{frameId}]:{compressed[frameId].Length}x{compressed[frameId][0].Length} Precision:{precision}");
            var comparer = new FloatComparer(_compressor.Original, compressed);
            // Viewの更新
            UpdateList(perFrame, frameId, comparer);
            DebugLog($"UpdateListView() End");
        }

        void UpdateList(bool perFrame, int frameId, FloatComparer comparer)
        {
            if (perFrame)
            {
                var targetItems = comparer.Diff[frameId].Transpose();
                _listView.itemsSource = targetItems;
                _listView.columns["name"].bindCell = (e, compoId) => MakeLabel((Label)e, $"[{compoId}]");
                _listView.columns["num"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(targetItems[compoId].Length));
                _listView.columns["min"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffMin[frameId][compoId]));
                _listView.columns["max"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffMax[frameId][compoId]));
                _listView.columns["std"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffStd[frameId][compoId]));
                _listView.columns["range"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffRange[frameId][compoId]));
                _listView.columns["center"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffMid[frameId][compoId]));
                _listView.columns["average"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffAve[frameId][compoId]));
                _listView.RefreshItems();
            }
            else
            {
                var diff = comparer.DiffAcrossAllFrame.Transpose();
                _listView.itemsSource = diff;
                _listView.columns["name"].bindCell = (e, compoId) => MakeLabel((Label)e, $"[{compoId}]");
                _listView.columns["num"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(diff[compoId].Length));
                _listView.columns["min"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffMinAcrossAllFrame[compoId]));
                _listView.columns["max"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffMaxAcrossAllFrame[compoId]));
                _listView.columns["std"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffStdAcrossAllFrame[compoId]));
                _listView.columns["range"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffRangeAcrossAllFrame[compoId]));
                _listView.columns["center"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffMidAcrossAllFrame[compoId]));
                _listView.columns["average"].bindCell = (e, compoId) => MakeLabel((Label)e, Str(comparer.DiffAveAcrossAllFrame[compoId]));
                _listView.RefreshItems();
            }
        }
        
        void MakeLabel(Label label, string text)
        {
            label.text = text;
            label.style.unityTextAlign = TextAnchor.MiddleRight;
        }
        private static string Str(float value) => value.ToString(CultureInfo.InvariantCulture);
    }
}
