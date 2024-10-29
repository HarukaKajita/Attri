using System.Collections;
using System.Collections.Generic;
using Attri.Runtime;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace Attri.Editor
{
    
    [CustomEditor(typeof(CompressedDataProvider), true)]
    public class CompressedDataProviderInspector : UnityEditor.Editor   
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("OnInspectorGUI Start");
            base.OnInspectorGUI();
            EditorGUILayout.LabelField("OnInspectorGUI End");
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            var compressedDataProvider = (IDataProvider)target;
            DrawAdditionalGUI(root, compressedDataProvider);
            return root;
        }
        private void DrawAdditionalGUI(VisualElement root, IDataProvider dataProvider)
        {
            var compressedDataProvider = (IDataProvider)target;
            var isInvalid = dataProvider == null;
            var pushedCollectButton = false;
            var pushedCompressButton = false;
            using (new EditorGUI.DisabledScope(isInvalid))
            {
                pushedCollectButton = GUILayout.Button("Collect Src Data");
                pushedCompressButton = GUILayout.Button("Compress");
            }

            if(isInvalid) return;
            // 値のビューを表示
            // 圧縮後の値のビューを表示
            var attributeType = compressedDataProvider.GetAttributeType();
            if (attributeType == AttributeDataType.Float)
            {
                var floatView = new FloatView(dataProvider);
                root.Add(floatView.VisualElement);
                var compressedFloatView = new CompressedFloatView(dataProvider);
                root.Add(compressedFloatView.VisualElement);
            }else if (attributeType == AttributeDataType.Int)
            {
                // var intView = new IntView(dataProvider);
            }
            
            if (pushedCollectButton) Debug.Log("Collect Source Data");
            if (pushedCompressButton) Debug.Log("Compress");
        }
    }
}
