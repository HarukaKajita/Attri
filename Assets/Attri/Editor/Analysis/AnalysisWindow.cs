using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Attri.Runtime;
using Attri.Runtime.Extensions;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

namespace Attri.Editor
{
	public class AnalysisWindow : EditorWindow
	{
		[SerializeField]
		private VisualTreeAsset m_VisualTreeAsset = default;
		[SerializeField]
		Float3Sequence m_Float3Sequence = default;
		
		[MenuItem("Window/UI Toolkit/AnalysisWindow")]
		public static void ShowExample()
		{
			AnalysisWindow wnd = GetWindow<AnalysisWindow>();
			wnd.titleContent = new GUIContent("AnalysisWindow");
		}
		
		public void CreateGUI()
		{
			// Each editor window contains a root VisualElement object
			VisualElement root = rootVisualElement;

			// Instantiate UXML
			VisualElement analysisElement = m_VisualTreeAsset.Instantiate();
			root.Add(analysisElement);
			var sequenceField = rootVisualElement.Q<ObjectField>("Container");
			sequenceField.objectType = typeof(Float3Sequence);
			sequenceField.value = m_Float3Sequence;
			sequenceField.RegisterValueChangedCallback(value =>
			{
				m_Float3Sequence = value.newValue as Float3Sequence;
				DrawListView();
				var assetName = m_Float3Sequence == null ? "None" : m_Float3Sequence.name;
				Debug.Log($"Sequence Changed: {assetName}");
			});
			
			DrawListView();
		}
		
		void DrawListView()
		{
			// var listView = rootVisualElement.Query<MultiColumnListView>();
			var valueListView = rootVisualElement.Q<MultiColumnListView>("ValueDataListView");
			var bitListView = rootVisualElement.Q<MultiColumnListView>("BitDataListView");
			
			if (m_Float3Sequence == null) return;
			var analysisData = new AnalysisData(m_Float3Sequence);
			analysisData.SetupValueInfoListView(valueListView);
			analysisData.SetupBitInfoListView(bitListView);
			// 反映
			valueListView.RefreshItems();
			bitListView.RefreshItems();
		}

		private void OnInspectorUpdate()
		{
			Repaint();
		}

		private struct FloatData
        {
            public readonly bool Sign;
            public readonly int Exponent;
            public readonly uint Mantissa;
            public FloatData(uint bits)
            {
                Sign = bits >> 31 != 0;
                Exponent = (int)((bits >> 23) & 0xff);
                Mantissa = bits & 0x7fffff;
            }
        }
        private struct Float3Data
        {
            public readonly bool3 Signed;
            public readonly int3 Exponent;
            public readonly uint3 Mantissa;
            public Float3Data(uint3 bits)
            {
                Signed = new bool3(bits.x >> 31 != 0, bits.y >> 31 != 0, bits.z >> 31 != 0);
                Exponent = new int3((int)((bits.x >> 23) & 0xff), (int)((bits.y >> 23) & 0xff), (int)((bits.z >> 23) & 0xff));
                Mantissa = new uint3(bits.x & 0x7fffff, bits.y & 0x7fffff, bits.z & 0x7fffff);
            }
        }
        public class ValueDataItem
        {
	        public string name;
	        public float min;
	        public float max;
	        public float range;
	        public float sigma;
        }
        public class BitDataItem
        {
	        public string name;
	        public bool signed;
	        public int min;
	        public int max;
	        public uint range;
	        public uint depth;
        }
        private struct AnalysisData
        {
            public readonly float3[] values;
            public readonly float3 min;
            public readonly float3 max;
            public readonly float3 range;
            public readonly float3 sigma;
            public readonly float lengthMin;
            public readonly float lengthMax;
            public readonly float lengthRange;
            public readonly float lengthSigma;
            public readonly uint3 union;
            public readonly uint lengthUnion;
            
            public readonly bool3 signed;
            public readonly int3 exponentMin;
            public readonly int3 exponentMax;
            public readonly uint3 exponentRange;
            public readonly uint3 exponentBitDepth;
            public readonly bool lengthSigned;
            public readonly int lengthExponentMin;
            public readonly int lengthExponentMax;
            public readonly uint lengthExponentRange;
            public readonly uint lengthExponentBitDepth;
            public AnalysisData(Float3Sequence sequence)
            {
                values = sequence.GetValues();
                min = new float3(values.Min(v => v.x), values.Min(v => v.y), values.Min(v => v.z));
                max = new float3(values.Max(v => v.x), values.Max(v => v.y), values.Max(v => v.z));
                range = max - min;
                var average = new float3(values.Average(v => v.x), values.Average(v => v.y), values.Average(v => v.z));
                var variance = new float3(values.Sum(v => math.pow(v.x - average.x, 2)), values.Sum(v => math.pow(v.y - average.y, 2)), values.Sum(v => math.pow(v.z - average.z, 2)));
                sigma = new float3(math.sqrt(variance.x / values.Length), math.sqrt(variance.y / values.Length), math.sqrt(variance.z / values.Length));
                var length = values.Select(math.length).ToArray();
                lengthMin = length.Min();
                lengthMax = length.Max();
                lengthRange = lengthMax - lengthMin;
                var lengthAverage = length.Average();
                var lengthVariance = length.Sum(v => math.pow(v - lengthAverage, 2));
                lengthSigma = math.sqrt(lengthVariance / length.Length);
                
                var uints = values.AsUint().ToArray();
                var lengthUints = length.AsUint().ToArray();
                union = uints.Aggregate((a, b) => a | b);
                lengthUnion = lengthUints.Aggregate((a, b) => a | b);
                // var bitData = uints.Select(u => new Float3Data(u));
                // var lengthBitData = lengthUints.Select(u => new FloatData(u));
                
                var minBits = new Float3Data(min.AsUint());
                var maxBits = new Float3Data(max.AsUint());
                var lengthMinBits = new FloatData(lengthMin.AsUint());
                var lengthMaxBits = new FloatData(lengthMax.AsUint());
                signed = new bool3(minBits.Signed.x != maxBits.Signed.x, minBits.Signed.y != maxBits.Signed.y, minBits.Signed.z != maxBits.Signed.z);
                exponentMin = new int3(minBits.Exponent.x-127, minBits.Exponent.y-127, minBits.Exponent.z-127);
                exponentMax = new int3(maxBits.Exponent.x-127, maxBits.Exponent.y-127, maxBits.Exponent.z-127);
                exponentRange = (uint3)(exponentMax - exponentMin);
                exponentBitDepth = new uint3(exponentRange.x.BitDepth(), exponentRange.y.BitDepth(), exponentRange.z.BitDepth());
                lengthSigned = lengthMinBits.Sign != lengthMaxBits.Sign;
                lengthExponentMin = lengthMinBits.Exponent-127;
                lengthExponentMax = lengthMaxBits.Exponent-127;
                lengthExponentRange = (uint)(lengthExponentMax - lengthExponentMin);
                lengthExponentBitDepth = lengthExponentRange.BitDepth();
            }
            public List<ValueDataItem> GetValueDataItems()
			{
				var items = new List<ValueDataItem>
				{
					new ValueDataItem {name = "X", min = min.x, max = max.x, range = range.x, sigma = sigma.x},
					new ValueDataItem {name = "Y", min = min.y, max = max.y, range = range.y, sigma = sigma.y},
					new ValueDataItem {name = "Z", min = min.z, max = max.z, range = range.z, sigma = sigma.z},
					new ValueDataItem {name = "Length", min = lengthMin, max = lengthMax, range = lengthRange, sigma = lengthSigma},
				};
				return items;
			}

			public List<BitDataItem> GetBitDataItems()
			{
				var items = new List<BitDataItem>
				{
					new BitDataItem {name = "X", signed = signed.x, min = exponentMin.x, max = exponentMax.x, range = exponentRange.x, depth = exponentBitDepth.x},
					new BitDataItem {name = "Y", signed = signed.y, min = exponentMin.y, max = exponentMax.y, range = exponentRange.y, depth = exponentBitDepth.y},
					new BitDataItem {name = "Z", signed = signed.z, min = exponentMin.z, max = exponentMax.z, range = exponentRange.z, depth = exponentBitDepth.z},
					new BitDataItem {name = "Length", signed = lengthSigned, min = lengthExponentMin, max = lengthExponentMax, range = lengthExponentRange, depth = lengthExponentBitDepth},
				};
				return items;
			}
			
			

			public void SetupValueInfoListView(MultiColumnListView listView)
			{
				void SetRightAlignedLabel(Label label, string text)
				{
					label.text = text;
					label.style.unityTextAlign = TextAnchor.MiddleRight;
				}
				var items = GetValueDataItems();
				listView.Clear();
				listView.itemsSource = items;
				listView.columns["name"].makeCell = () => new Label();
				listView.columns["min"].makeCell = () => new Label();
				listView.columns["max"].makeCell = () => new Label();
				listView.columns["range"].makeCell = () => new Label();
				listView.columns["sigma"].makeCell = () => new Label();
				listView.columns["name"].bindCell = (e, i) => SetRightAlignedLabel((Label)e, items[i].name);
				listView.columns["min"].bindCell = (e, i) => SetRightAlignedLabel((Label)e, items[i].min.ToString("0000.0000000"));
				listView.columns["max"].bindCell = (e, i) => SetRightAlignedLabel((Label)e, items[i].max.ToString("0000.0000000"));
				listView.columns["range"].bindCell = (e, i) => SetRightAlignedLabel((Label)e, items[i].range.ToString("0000.0000000"));
				listView.columns["sigma"].bindCell = (e, i) => SetRightAlignedLabel((Label)e, items[i].sigma.ToString("000.0000"));
			}

			public void SetupBitInfoListView(MultiColumnListView listView)
			{
				void SetRightAlignedLabel(Label label, string text)
				{
					label.text = text;
					label.style.unityTextAlign = TextAnchor.MiddleRight;
				}
				var items = GetBitDataItems();
				listView.Clear();
				listView.itemsSource = items;
				listView.columns["name"].makeCell = () => new Label();
				listView.columns["signed"].makeCell = () => new Label();
				listView.columns["min"].makeCell = () => new Label();
				listView.columns["max"].makeCell = () => new Label();
				listView.columns["range"].makeCell = () => new Label();
				listView.columns["bitDepth"].makeCell = () => new Label();
				listView.columns["name"].bindCell = (e, i) => SetRightAlignedLabel((Label)e, items[i].name);
				listView.columns["signed"].bindCell = (e, i) => SetRightAlignedLabel((Label)e, items[i].signed.ToString(CultureInfo.InvariantCulture));
				listView.columns["min"].bindCell = (e, i) => SetRightAlignedLabel((Label)e, items[i].min.ToString(CultureInfo.InvariantCulture));
				listView.columns["max"].bindCell = (e, i) => SetRightAlignedLabel((Label)e, items[i].max.ToString(CultureInfo.InvariantCulture));
				listView.columns["range"].bindCell = (e, i) => SetRightAlignedLabel((Label)e, items[i].range.ToString(CultureInfo.InvariantCulture));
				listView.columns["bitDepth"].bindCell = (e, i) => SetRightAlignedLabel((Label)e, items[i].depth.ToString(CultureInfo.InvariantCulture));
			}
        }
	}
    
}
