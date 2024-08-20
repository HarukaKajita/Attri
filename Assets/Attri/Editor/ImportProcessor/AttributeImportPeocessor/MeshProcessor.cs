using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Attri.Editor
{
    [Serializable]
    class AttributeSelection
    {
        public VertexAttribute? Attribute { get; }
        public VertexAttributeFormat format;
        public int dimension = 3;//[1,4]
        [HideInInspector]
        public int index = 0;
        public string fetchAttributeName;
        
        public string AttributeName()
        {
            return Attribute== null ? "Index" : Attribute.Value.ToString();
        }

        public AttributeSelection(VertexAttribute? attribute, VertexAttributeFormat format, string fetchAttributeName)
        {
            this.Attribute = attribute;
            this.format = format;
            this.fetchAttributeName = fetchAttributeName;
        }
    }
    [Serializable]
    public class MeshProcessor :AttributeImportProcessor
    {
        [SerializeField, HideInInspector] List<Mesh> _meshes = new();
        
        // TODO: Support SubMesh
        internal AttributeSelection[] attributeSelections =
        {
            new (VertexAttribute.Position , VertexAttributeFormat.Float16, "P"),
            new (VertexAttribute.Normal   , VertexAttributeFormat.Float16, "N"),
            new (VertexAttribute.Tangent  , VertexAttributeFormat.Float16, "tangent"),
            new (VertexAttribute.Color    , VertexAttributeFormat.UNorm8 , "Cd"),
            new (VertexAttribute.TexCoord0, VertexAttributeFormat.UNorm8 , "uv"),
            new (VertexAttribute.TexCoord1, VertexAttributeFormat.UNorm8 , "uv1"),
            new (VertexAttribute.TexCoord2, VertexAttributeFormat.UNorm8 , "uv2"),
            new (VertexAttribute.TexCoord3, VertexAttributeFormat.UNorm8 , "uv3"),
            new (VertexAttribute.TexCoord4, VertexAttributeFormat.UNorm8 , "uv4"),
            new (VertexAttribute.TexCoord5, VertexAttributeFormat.UNorm8 , "uv5"),
            new (VertexAttribute.TexCoord6, VertexAttributeFormat.UNorm8 , "uv6"),
            new (VertexAttribute.TexCoord7, VertexAttributeFormat.UNorm8 , "uv7"),
            new (null, VertexAttributeFormat.UInt32, "index")
        };
        
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
                foreach (var selection in processor.attributeSelections)
                {
                    DrawAttributeSelection(selection, additionalRect);
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
                h += EditorGUIUtility.singleLineHeight * processor.attributeSelections.Length;
            return h;
        }
        private void DrawAttributeSelection( AttributeSelection selection, Rect position)
        {
            var labelWidth = EditorGUIUtility.labelWidth/2;
            var w = position.width;
            w = (position.width - labelWidth)/3;
            
            var labelRect = new Rect(position.x, position.y, labelWidth, position.height);
            var optionRect = new Rect(position.x + labelWidth, position.y, w, position.height);
            EditorGUI.LabelField(labelRect, selection.AttributeName());
            selection.index = EditorGUI.Popup(optionRect, "", selection.index, _attributeNames);
            optionRect.x += w;
            selection.format = (VertexAttributeFormat)EditorGUI.EnumPopup(optionRect, selection.format);
            optionRect.x += w;
            selection.dimension = EditorGUI.IntSlider(optionRect, "", selection.dimension, 1, 4);
            
            if (selection.index >= _attributeNames.Length || selection.index < 0)
                selection.index = 0;
            selection.fetchAttributeName = _attributeNames[selection.index];
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
            foreach (var selection in processor.attributeSelections)
            {
                var selectedIndex = selection.index;
                var selectedName = selection.fetchAttributeName;
                if (selectedIndex >= _attributeNames.Length || selectedIndex < 0) selectedIndex = 0;
                var currentAttributeName = _attributeNames[selectedIndex];
                if (currentAttributeName != selectedName)
                    selection.index = Array.IndexOf(_attributeNames, selectedName);
            }
        }
    }
}