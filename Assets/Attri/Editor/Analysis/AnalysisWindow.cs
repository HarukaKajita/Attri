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
		// Int
		[SerializeField] private VisualTreeAsset m_IntDataAnalysisVisualTreeAsset;
		[SerializeField] private VisualTreeAsset m_IntBitDataAnalysisVisualTreeAsset;
		
		private VisualElement windowElement;
		
		// Data (= Container,Sequence)
		[SerializeReference] private IDataProvider dataProvider = default;
		private ObjectField assetField;
		EventCallback<ChangeEvent<Object>> assignDataCallback;
		EventCallback<ChangeEvent<bool>> centeringToggleCallback;
		
		CompressedFloatView compressedFloatView;
		
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
			compressedFloatView = new CompressedFloatView(dataProvider);
			root.Add(compressedFloatView.VisualElement);
			Debug.Log("CreateGUI End");
		}

		private void UpdateListView(ChangeEvent<Object> changeEvent)
		{
			dataProvider = changeEvent.newValue as IDataProvider;
			compressedFloatView.Reset(dataProvider);
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
				var originalData = dataProvider.AsFloat();
				var analysis = new FloatAnalysisData(originalData);
				
				// AnalysisGroup
				var dataAnalysis = m_FloatDataAnalysisVisualTreeAsset.Instantiate();
				var dataListView = dataAnalysis.Q<MultiColumnListView>("List");
				var analysisGroup = rootVisualElement.Q<VisualElement>("DataAnalysis");
				analysisGroup.Clear();

				// ListViewにデータをセット
				SetupFloatDataListView(dataListView, analysis);
				analysisGroup.Add(dataAnalysis);
				dataListView.RefreshItems();
				
				compressedFloatView.UpdateView();
				
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
			listView.columns["std"].bindCell = (e, i) => MakeLabel((Label)e, items[i].signed ? "Signed" : "Unsigned");
			listView.columns["expRange"].bindCell = (e, i) => MakeLabel((Label)e, $"{items[i].exponentRange} ({items[i].minExponent}~{items[i].maxExponent})");
			listView.columns["exponentBitDepth"].bindCell = (e, i) => MakeLabel((Label)e, items[i].exponentBitDepth.ToString());
		}

		private static string ErrorStr(float value) => value.ToString("0.0000000");
		private static string Str(float value) => value.ToString(CultureInfo.InvariantCulture);
	}
    
}
