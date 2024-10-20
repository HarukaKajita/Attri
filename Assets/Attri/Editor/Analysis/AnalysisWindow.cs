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
		
		// Data (= Container,Sequence)
		[SerializeReference] private IDataProvider dataProvider = default;
		EventCallback<ChangeEvent<Object>> assignDataCallback;

		private FloatView _floatView;
		private CompressedFloatView _compressedFloatView;
		private CompressedDirectionView _compressedDirectionView;

		[MenuItem("Tool/Attri/AnalysisWindow")]
		[Shortcut("Attribute Analysis Window", KeyCode.W, ShortcutModifiers.Shift | ShortcutModifiers.Control)]
		public static void ShowWindow()
		{
			AnalysisWindow wnd = CreateWindow<AnalysisWindow>("AnalysisWindow", typeof(AnalysisWindow));
			wnd.ShowUtility();
		}

		public void CreateGUI()
		{
			Debug.Log("CreateGUI");
			titleContent = new GUIContent("AnalysisWindow");
			
			// Windowの初期化
			var windowElement = m_WindowVisualTreeAsset.Instantiate();
			var dataProviderField = windowElement.Q<ObjectField>("DataProvider");
			dataProviderField.objectType = typeof(IDataProvider);
			assignDataCallback = OnDataProviderChanged;
			dataProviderField.RegisterValueChangedCallback(assignDataCallback);
			dataProviderField.searchContext = SearchService.CreateContext("p: union{t:Container, t:Sequence}");
			// dataProviderField.searchContext = SearchService.CreateContext("p: union{t:Container}");
			rootVisualElement.Add(windowElement);
			
			Debug.Log("CreateGUI End");
		}

		private void OnDataProviderChanged(ChangeEvent<Object> changeEvent)
		{
			dataProvider = changeEvent.newValue as IDataProvider;
			_floatView?.Reset(dataProvider);
			_compressedFloatView?.Reset(dataProvider);
			_compressedDirectionView?.Reset(dataProvider);
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
				_floatView.Remove();
				_compressedFloatView.Remove();
				_compressedDirectionView.Remove();
				// IntのViewを削除
				Debug.LogWarning("DataProvider is null");
				return;
			}
			var dataType = dataProvider.GetAttributeType();
			if(dataType == AttributeDataType.Float)
			{
				// IntのViewを削除
				
				// Float
				if(_floatView == null)
				{
					_floatView = new FloatView(dataProvider);
					rootVisualElement.Add(_floatView.VisualElement);
				}
				_floatView.UpdateView();
				// CompressedFloat
				if(_compressedFloatView == null)
				{
					_compressedFloatView = new CompressedFloatView(dataProvider);
					rootVisualElement.Add(_compressedFloatView.VisualElement);
				}
				_compressedFloatView.UpdateView();
				// CompressedDirection
				if (_compressedDirectionView == null)
				{
					_compressedDirectionView = new CompressedDirectionView(dataProvider);
					rootVisualElement.Add(_compressedDirectionView.VisualElement);
				}
				_compressedDirectionView.UpdateView();
			}
			else if(dataType == AttributeDataType.Int)
			{
				// FloatのViewを削除
				_floatView?.Remove();
				_compressedFloatView?.Remove();
				_compressedDirectionView?.Remove();
				
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
