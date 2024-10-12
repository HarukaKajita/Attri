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
		[SerializeField] private VisualTreeAsset m_FloatBitDataAnalysisVisualTreeAsset;
		// Int
		[SerializeField] private VisualTreeAsset m_IntDataAnalysisVisualTreeAsset;
		[SerializeField] private VisualTreeAsset m_IntBitDataAnalysisVisualTreeAsset;
		
		// Data (= Container,Sequence)
		[SerializeReference] private IDataProvider dataProvider = default;
		private ObjectField assetField;
		EventCallback<ChangeEvent<Object>> callback;
		
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
			VisualElement analysisElement = m_WindowVisualTreeAsset.Instantiate();
			assetField = analysisElement.Q<ObjectField>("DataProvider");
			assetField.objectType = typeof(IDataProvider);
			callback = UpdateListView;
			assetField.RegisterValueChangedCallback(callback);
			root.Add(analysisElement);
			Debug.Log("CreateGUI End");
		}

		private void UpdateListView(ChangeEvent<Object> changeEvent)
		{
			dataProvider = changeEvent.newValue as IDataProvider;
			DrawListView();
		}

		private void OnDestroy()
		{
			Debug.Log("OnDestroy");
			assetField.UnregisterCallback(callback);
		}

		public override IEnumerable<Type> GetExtraPaneTypes()
		{
			var extraPaneTypes = base.GetExtraPaneTypes().ToList();
			extraPaneTypes.Add(typeof(AnalysisWindow));
			return extraPaneTypes;
		}
		
		void DrawListView()
		{
			if (dataProvider == null)
			{
				var analysisElement = rootVisualElement.Q<VisualElement>("DataAnalysis");
				analysisElement.Clear();
				Debug.LogWarning("DataProvider is null");
				return;
			}
			var dataType = dataProvider.GetAttributeType();
			if(dataType == AttributeDataType.Float)
			{
				// AnalysisGroup
				var dataAnalysis = m_FloatDataAnalysisVisualTreeAsset.Instantiate();
				var dataListView = dataAnalysis.Q<MultiColumnListView>("List");
				var analysisGroup = rootVisualElement.Q<VisualElement>("DataAnalysis");
				analysisGroup.Clear();
				var analysis = new FloatAnalysisData(dataProvider.AsFloat());
				SetupFloatDataListView(dataListView, analysis);
				analysisGroup.Add(dataAnalysis);
				dataListView.RefreshItems();
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

		Label BindLabel(string bindingPath)
		{
			var l = new Label();
			l.bindingPath = bindingPath;
			return l;
		}
		void SetupFloatDataListView(MultiColumnListView listView, FloatAnalysisData analysisData)
		{
			var items = analysisData.componentsAnalysisData;
			listView.Clear();
			listView.itemsSource = items;
			// listView.columns["name"].makeCell = () => BindLabel("name");
			// listView.columns["min"].makeCell = () => BindLabel("min");
			// listView.columns["max"].makeCell = () => BindLabel("max");
			// listView.columns["range"].makeCell = () => BindLabel("range");
			// listView.columns["sigma"].makeCell = () => BindLabel("sigma");
			// listView.columns["signed"].makeCell = () => BindLabel("signed");
			// listView.columns["minExponent"].makeCell = () => BindLabel("minExponent");
			// listView.columns["maxExponent"].makeCell = () => BindLabel("maxExponent");
			// listView.columns["exponentRange"].makeCell = () => BindLabel("exponentRange");
			// listView.columns["exponentBitDepth"].makeCell = () => BindLabel("exponentBitDepth");
			
			listView.columns["name"].bindCell = (e, i) => MakeLabel((Label)e, $"[{i}]");
			listView.columns["min"].bindCell = (e, i) => MakeLabel((Label)e, items[i].min.ToString("0000.0000000"));
			listView.columns["max"].bindCell = (e, i) => MakeLabel((Label)e, items[i].max.ToString("0000.0000000"));
			listView.columns["range"].bindCell = (e, i) => MakeLabel((Label)e, items[i].range.ToString("0000.0000000"));
			listView.columns["sigma"].bindCell = (e, i) => MakeLabel((Label)e, items[i].sigma.ToString("000.0000"));
			listView.columns["signed"].bindCell = (e, i) => MakeLabel((Label)e, items[i].signed ? "Signed" : "Unsigned");
			listView.columns["minExponent"].bindCell = (e, i) => MakeLabel((Label)e, items[i].minExponent.ToString());
			listView.columns["maxExponent"].bindCell = (e, i) => MakeLabel((Label)e, items[i].maxExponent.ToString());
			listView.columns["exponentRange"].bindCell = (e, i) => MakeLabel((Label)e, items[i].exponentRange.ToString());
			listView.columns["exponentBitDepth"].bindCell = (e, i) => MakeLabel((Label)e, items[i].exponentBitDepth.ToString());
			
		}
	}
    
}
