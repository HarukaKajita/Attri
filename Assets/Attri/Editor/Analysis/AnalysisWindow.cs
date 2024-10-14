using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Attri.Runtime;
using UnityEditor;
using UnityEditor.Search;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
	public class AnalysisWindow : EditorWindow
	{
		// Window
		[SerializeField] private VisualTreeAsset m_WindowVisualTreeAsset;
		// Float
		[SerializeField] private VisualTreeAsset m_FloatDataAnalysisVisualTreeAsset;
		[SerializeField] private VisualTreeAsset m_CompressedFloatDataAnalysisVisualTreeAsset;
		// Int
		[SerializeField] private VisualTreeAsset m_IntDataAnalysisVisualTreeAsset;
		[SerializeField] private VisualTreeAsset m_IntBitDataAnalysisVisualTreeAsset;
		
		private VisualElement windowElement;
		private Toggle centeringToggleElement;
		private SliderInt precisionSliderElement;
		
		// Data (= Container,Sequence)
		[SerializeReference] private IDataProvider dataProvider = default;
		private ObjectField assetField;
		EventCallback<ChangeEvent<Object>> assignDataCallback;
		EventCallback<ChangeEvent<bool>> centeringToggleCallback;
		
		[MenuItem("Window/UI Toolkit/AnalysisWindow")]
		[Shortcut("Attribute Analysis Window", KeyCode.W, ShortcutModifiers.Shift | ShortcutModifiers.Control)]
		public static void ShowExample()
		{
			AnalysisWindow wnd = CreateInstance<AnalysisWindow>();
			wnd.titleContent = new GUIContent("AnalysisWindow");
			wnd.ShowUtility();
		}
		
		public void CreateGUI()
		{
			Debug.Log("CreateGUI");
			// Each editor window contains a root VisualElement object
			VisualElement root = rootVisualElement;
			root.Clear();
			
			// Instantiate UXML
			// 分析ListView
			windowElement = m_WindowVisualTreeAsset.Instantiate();
			root.Add(windowElement);
			assetField = windowElement.Q<ObjectField>("DataProvider");
			assetField.objectType = typeof(IDataProvider);
			assignDataCallback = UpdateListView;
			assetField.RegisterValueChangedCallback(assignDataCallback);
			
			// オプション
			precisionSliderElement = windowElement.Q<SliderInt>("Precision");
			precisionSliderElement.value = 23;
			precisionSliderElement.RegisterCallback<ChangeEvent<int>>(_ => DrawListView());
			centeringToggleElement = windowElement.Q<Toggle>("CompressAsFloat");
			centeringToggleCallback = UpdateListViewOnToggle;
			centeringToggleElement.RegisterValueChangedCallback(centeringToggleCallback);
			Debug.Log("CreateGUI End");
		}

		private void UpdateListView(ChangeEvent<Object> changeEvent)
		{
			dataProvider = changeEvent.newValue as IDataProvider;
			DrawListView();
		}
		private void UpdateListViewOnToggle(ChangeEvent<bool> changeEvent)
		{
			DrawListView();
		}

		public override IEnumerable<Type> GetExtraPaneTypes()
		{
			var extraPaneTypes = base.GetExtraPaneTypes().ToList();
			extraPaneTypes.Add(typeof(AnalysisWindow));
			return extraPaneTypes;
		}
		
		void DrawListView()
		{
			Debug.Log("DrawListView");
			if (dataProvider == null)
			{
				var analysisElement = windowElement.Q<VisualElement>("DataAnalysis");
				analysisElement.Clear();
				Debug.LogWarning("DataProvider is null");
				return;
			}
			var dataType = dataProvider.GetAttributeType();
			if(dataType == AttributeDataType.Float)
			{
				// データの分析
				var data = dataProvider.AsFloat();
				var analysis = new FloatAnalysisData(data);
				FloatAnalysisData comparisonAnalysisData = default;
				var centering = centeringToggleElement.value;
				var compare = centering;
				if (centering)
				{
					comparisonAnalysisData = analysis.Compressed(precisionSliderElement.value);
				}
				
				// AnalysisGroup
				var dataAnalysis = m_FloatDataAnalysisVisualTreeAsset.Instantiate();
				var dataListView = dataAnalysis.Q<MultiColumnListView>("List");
				var analysisGroup = rootVisualElement.Q<VisualElement>("DataAnalysis");
				analysisGroup.Clear();

				// ListViewにデータをセット
				SetupFloatDataListView(dataListView, analysis);
				analysisGroup.Add(dataAnalysis);
				dataListView.RefreshItems();
				
				if(compare)
				{
					rootVisualElement.Q<VisualElement>("ComparisonAnalysis").Clear();
					var comparisonDataAnalysis = m_CompressedFloatDataAnalysisVisualTreeAsset.Instantiate();
					var comparisonDataListView = comparisonDataAnalysis.Q<MultiColumnListView>("List");
					var comparisonAnalysisGroup = rootVisualElement.Q<VisualElement>("ComparisonAnalysis");
					comparisonAnalysisGroup.Add(comparisonDataAnalysis);
					SetupCompressedFloatDataListView(comparisonDataListView, comparisonAnalysisData);
					comparisonDataListView.RefreshItems();
				}
			}
			else if(dataType == AttributeDataType.Int)
			{
				var dataAnalysis = m_IntDataAnalysisVisualTreeAsset.Instantiate();
				rootVisualElement.Add(dataAnalysis);
				var dataListView = dataAnalysis.Q<MultiColumnListView>("List");
				var analysis = new IntAnalysisData(dataProvider.AsInt());
				dataListView.RefreshItems();
			}
		}

		void MakeLabel(Label label, string text)
		{
			label.text = text;
			label.style.unityTextAlign = TextAnchor.MiddleRight;
		}

		void SetupFloatDataListView(MultiColumnListView listView, FloatAnalysisData analysisData)
		{
			var items = analysisData.componentsAnalysisData;
			listView.Clear();
			listView.itemsSource = items;
			
			listView.columns["name"].bindCell = (e, i) => MakeLabel((Label)e, $"[{i}]");
			listView.columns["num"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].elementNum));
			listView.columns["min"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].min));
			listView.columns["max"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].max));
			listView.columns["sigma"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].sigma));
			listView.columns["range"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].range));
			listView.columns["center"].bindCell = (e, i) => MakeLabel((Label)e, Str((items[i].min+items[i].max)/2f));
			listView.columns["signed"].bindCell = (e, i) => MakeLabel((Label)e, items[i].signed ? "Signed" : "Unsigned");
			listView.columns["expRange"].bindCell = (e, i) => MakeLabel((Label)e, $"{items[i].exponentRange} ({items[i].minExponent}~{items[i].maxExponent})");
			listView.columns["exponentBitDepth"].bindCell = (e, i) => MakeLabel((Label)e, items[i].exponentBitDepth.ToString());
		}
		
		void SetupCompressedFloatDataListView(MultiColumnListView listView, FloatAnalysisData analysisData)
		{
			var items = analysisData.componentsAnalysisData;
			listView.Clear();
			listView.itemsSource = items;
			
			listView.columns["name"].bindCell = (e, i) => MakeLabel((Label)e, $"[{i}]");
			listView.columns["num"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].elementNum));
			listView.columns["min"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].min));
			listView.columns["max"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].max));
			listView.columns["sigma"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].sigma));
			listView.columns["range"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].range));
			listView.columns["center"].bindCell = (e, i) => MakeLabel((Label)e, Str((items[i].min+items[i].max)/2f));
			
			listView.columns["errorMin"].bindCell = (e, i) => MakeLabel((Label)e, ErrorStr(items[i].errorMin));
			listView.columns["errorMax"].bindCell = (e, i) => MakeLabel((Label)e, ErrorStr(items[i].errorMax));
			listView.columns["errorAverage"].bindCell = (e, i) => MakeLabel((Label)e, ErrorStr(items[i].errorAverage));
			listView.columns["errorSigma"].bindCell = (e, i) => MakeLabel((Label)e, ErrorStr(items[i].errorSigma));
			
			listView.columns["E"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].E));
			listView.columns["offset"].bindCell = (e, i) => MakeLabel((Label)e, Str(items[i].offset));
		}

		private static string ErrorStr(float value) => value.ToString("0.0000000");
		private static string Str(float value) => value.ToString(CultureInfo.InvariantCulture);
	}
    
}
