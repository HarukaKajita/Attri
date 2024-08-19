using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
    [Serializable]
    class AttributeSelection
    {
        [HideInInspector]
        public int index;
        public string name;
        public AttributeSelection(int index, string name)
        {
            this.index = index;
            this.name = name;
        }
    }
    [Serializable]
    public class MeshProcessor :AttributeImportProcessor
    {
        [SerializeField, HideInInspector] List<Mesh> _meshes = new();
        
        internal string[] Keys = {"Position", "Index", "Normal", "UV"};
        internal AttributeSelection[] Values = {new (0, "P"), new (0, "index"), new (0, "N"), new (0, "uv")};
        
        public MeshProcessor():this("Mesh") {}
        public MeshProcessor(string prefix = "Mesh") : base(prefix) { }
        internal override Object[] RunProcessor(AssetImportContext ctx)
        {
            Debug.Log($"{GetType().Name}.RunProcessor()");
            _meshes.Clear();
            
            
            return _meshes.Cast<Object>().ToArray();
        }
    }

    // [System.AttributeUsage(System.AttributeTargets.Field)]
    // public class AttributeSelectionAttribute : PropertyAttribute
    // {
    //     internal MeshProcessor Processor;
    //     public AttributeSelectionAttribute(MeshProcessor processor)
    //     {
    //         Processor = processor;
    //     }
    // }
    [CustomPropertyDrawer(typeof(MeshProcessor))]
    public class MeshProcessorDrawer : PropertyDrawer
    {
        private string[] _attributeNames = Array.Empty<string>();
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();
            // var target = property.serializedObject.targetObject;
            var processor = property.GetValue<MeshProcessor>();
            Preparation(processor);
            // var selectionProperty = property.FindPropertyRelative(nameof(processor.attributeSelection));
            // Debug.Log($"{GetType().Name}.OnGUI() : {processor}");
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property, label, true);
            var defaultHeight = EditorGUI.GetPropertyHeight(property, label, true);
            position.y += defaultHeight;
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                var additionalRect = new Rect(position);
                additionalRect.height = EditorGUIUtility.singleLineHeight;
                additionalRect = EditorGUI.IndentedRect(additionalRect);
                // debug
                // EditorGUI.DrawRect(additionalRect, Color.red);
                // Debug.Log($"{processor.GetType().Name}.OnGUI() : processor.attributeSelection.Count:{processor.attributeSelection.Count}");
                for (var index = 0; index < processor.Keys.Length; index++)
                {
                    var key = processor.Keys[index];
                    var selection = processor.Values[index];
                    DrawAttributeSelection(key, selection, additionalRect);
                    additionalRect.y += EditorGUIUtility.singleLineHeight;
                }
                property.SetValue(processor);
                EditorGUI.indentLevel--;
            }
            
            EditorGUI.EndProperty();
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var processor = property.GetValue<MeshProcessor>();
            var h = EditorGUI.GetPropertyHeight(property, label, true);
            if(property.isExpanded)
                h += EditorGUIUtility.singleLineHeight * processor.Keys.Length;
            return h;
        }
        private void DrawAttributeSelection(string selectionKey, AttributeSelection selection, Rect position)
        {
            selection.index = EditorGUI.Popup(position, selectionKey, selection.index, _attributeNames);
            if (selection.index >= _attributeNames.Length || selection.index < 0)
                selection.index = 0;
            selection.name = _attributeNames[selection.index];
        }

        void Preparation(MeshProcessor processor)
        {
            if (processor == null) return;
            if (processor.attributes == null) return;
            // アトリビュート名の配列を作る
            var attributes = processor.attributes;
            // メッシュに含めないアトリビュート用に"_"を追加
            _attributeNames = attributes.Select(a => a.Name()).Prepend("_").ToArray();
            // fileのアトリビュートの順の変更や増減があった場合に、選択しているアトリビュートのindexがずれるので、それを修正する
            for (int index = 0; index < processor.Keys.Length; index++)
            {
                var key = processor.Keys[index];
                var selection = processor.Values[index];
                var selectedIndex = selection.index;
                var selectedName = selection.name;
                if (selectedIndex >= _attributeNames.Length || selectedIndex < 0) selectedIndex = 0;
                var currentAttributeName = _attributeNames[selectedIndex];
                if (currentAttributeName != selectedName)
                    processor.Values[index].index = Array.IndexOf(_attributeNames, selectedName);
                
            }
        }
    }
}