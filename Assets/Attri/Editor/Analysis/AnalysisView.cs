using Attri.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Attri.Editor
{
    public abstract class AnalysisView
    {
        protected abstract string VisualTreePath();
        protected readonly VisualElement Self;
        public VisualElement VisualElement => Self; 
            
        protected IDataProvider _dataProvider;
        protected bool DEBUG = false;
        
        protected void DebugLog(string message)
        {
            if (DEBUG) Debug.Log(message);
        }

        protected AnalysisView(IDataProvider dataProvider)
        {
            var compressedFloatViewAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(VisualTreePath());
            _dataProvider = dataProvider;
            Self = compressedFloatViewAsset.Instantiate();
        }
        public void Reset(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Remove()
        {
            Self.parent.Remove(Self);
        }

        public abstract void UpdateView();
    }
}
