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
        // internal int PositionAttributeIndex = 0;
        // internal string PositionAttributeNameCache;
        [SerializeField, HideInInspector]
        // internal SerializedDictionary<string, AttributeSelection> attributeSelection = new();
        
        internal string[] Keys = {"Position", "Index", "Normal", "UV"};
        internal AttributeSelection[] Values = {new (0, "P"), new (0, "index"), new (0, "N"), new (0, "uv")};
        
        public MeshProcessor():this("Mesh") {}
        public MeshProcessor(string prefix = "Mesh") : base(prefix)
        {
            // attributeSelection.Clear();
            // if(!attributeSelection.ContainsKey("Position"))
            //     attributeSelection.Add("Position", new (0, "P"));
            // if(!attributeSelection.ContainsKey("Index"))
            //     attributeSelection.Add("Index", new(0, "index"));
            // if(!attributeSelection.ContainsKey("Normal"))
            //     attributeSelection.Add("Normal", new(0, "N"));
            // if(!attributeSelection.ContainsKey("UV"))
            //     attributeSelection.Add("UV", new(0, "uv"));
        }
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
            // base.OnGUI();
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
            // Debug.Log($"{processor.GetType().Name}.Preparation() : attributes[{attributes.Count}]");
            _attributeNames = attributes.Select(a => a.Name()).Prepend("Unused").ToArray();
            // foreach(var name in _attributeNames)
                // Debug.Log($"attributeNames:{name}");
            
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
                // {
                //     processor.attributeSelection[key].index = Array.IndexOf(_attributeNames, selectedName);
                // }
            }
            // foreach (var selection in processor.attributeSelection)
            // {
            //     var key = selection.Key;
            //     var selectedIndex = selection.Value.index;
            //     var selectedName = selection.Value.name;
            //     Debug.Log($"key:{key} index:{selectedIndex} name:{selectedName}");
            //     if (selectedIndex >= _attributeNames.Length || selectedIndex < 0)
            //         selectedIndex = 0;
            //     var currentAttributeName = _attributeNames[selectedIndex];
            //     if (currentAttributeName != selection.Value.name)
            //         processor.attributeSelection[key].index = Array.IndexOf(_attributeNames, selectedName);    
            // }
        }
    }
}