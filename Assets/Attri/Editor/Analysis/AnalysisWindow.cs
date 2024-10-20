using System;
using System.Collections.Generic;
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
		EventCallback<ChangeEvent<Object>> assignDataCallback;

		private FloatView _floatView;
		private CompressedFloatView _compressedFloatView;

		[MenuItem("Window/UI Toolkit/AnalysisWindow")]
		[Shortcut("Attribute Analysis Window", KeyCode.W, ShortcutModifiers.Shift | ShortcutModifiers.Control)]
		public static void ShowWindow()
		{
			AnalysisWindow wnd = CreateWindow<AnalysisWindow>("AnalysisWindow", typeof(AnalysisWindow));
			wnd.ShowUtility();
		}

		private void OnEnable()
		{
			titleContent = new GUIContent();
		}

		public void CreateGUI()
		{
			Debug.Log("CreateGUI");
			
			// Windowの初期化
			windowElement = m_WindowVisualTreeAsset.Instantiate();
			var dataProviderField = windowElement.Q<ObjectField>("DataProvider");
			dataProviderField.objectType = typeof(IDataProvider);
			assignDataCallback = OnDataProviderChanged;
			dataProviderField.RegisterValueChangedCallback(assignDataCallback);
			rootVisualElement.Add(windowElement);
			
			Debug.Log("CreateGUI End");
		}

		private void OnDataProviderChanged(ChangeEvent<Object> changeEvent)
		{
			dataProvider = changeEvent.newValue as IDataProvider;
			_compressedFloatView?.Reset(dataProvider);
			_floatView?.Reset(dataProvider);
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
				// Float
				if(_floatView == null)
				{
					_floatView = new FloatView(dataProvider);
					rootVisualElement.Add(_floatView.VisualElement);
					_floatView.UpdateView();
				}
				// CompressedFloat
				if(_compressedFloatView == null)
				{
					_compressedFloatView = new CompressedFloatView(dataProvider);
					rootVisualElement.Add(_compressedFloatView.VisualElement);
					_compressedFloatView.UpdateView();
				}
			}
			else if(dataType == AttributeDataType.Int)
			{
				_floatView?.Remove();
				_compressedFloatView?.Remove();
				
				// TODO: Intの分析Viewを追加する
				// var dataAnalysis = m_IntDataAnalysisVisualTreeAsset.Instantiate();
				// rootVisualElement.Add(dataAnalysis);
				// var dataListView = dataAnalysis.Q<MultiColumnListView>("List");
				// var analysis = new IntAnalysisData(dataProvider.AsInt());
				// dataListView.RefreshItems();
			}
		}
	}
    
}
