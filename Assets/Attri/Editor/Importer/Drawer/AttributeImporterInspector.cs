using System.Collections.Generic;
using System.Linq;
using Attri.Runtime;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Attri.Editor
{
    // カスタムインスペクタで内容を観れるようにする
    [CustomEditor(typeof(AttributeImporter))]
    public class AttributeImporterInspector : ScriptedImporterEditor
    {
        IEnumerable<IAttribute> attributes;
        Dictionary<IAttribute,bool> attributeFoldoutFlags = new();
        Dictionary<object,bool> frameListFoldoutFlags = new();
        Dictionary<(object, int frameId),bool> frameFoldoutFlags = new();
        
        protected override void Awake()
        {
            base.Awake();
            var importer = target as AttributeImporter;
            attributes = importer.attributes;
            
            // Foldout Flags
            attributeFoldoutFlags.Clear();
            foreach (var attribute in attributes)
            {
                attributeFoldoutFlags.Add(attribute,false);
                frameListFoldoutFlags.Add(attribute,false);
                var frameCount = attribute.FrameCount();
                for(var i = 0; i < frameCount; i++)
                    frameFoldoutFlags.Add((attribute,i),false);
            }
        }
        bool DoDrawDefaultInspector(SerializedObject obj)
        {
            EditorGUI.BeginChangeCheck();
            obj.UpdateIfRequiredOrScript();

            // Loop through properties and create one field (including children) for each top level property.
            SerializedProperty property = obj.GetIterator();
            bool expanded = true;
            while (property.NextVisible(expanded))
            {
                using (new EditorGUI.DisabledScope("m_Script" == property.propertyPath))
                {
                    EditorGUILayout.PropertyField(property, true);
                }
                expanded = false;
            }

            obj.ApplyModifiedProperties();
            return EditorGUI.EndChangeCheck();
        }
        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            DoDrawDefaultInspector(serializedObject);
            if (extraDataType != null)
                DoDrawDefaultInspector(extraDataSerializedObject);
            
            foreach (var attribute in attributes)
                DrawAttribute(attribute);
            
            ApplyRevertGUI();
        }
    
        private void DrawAttribute(IAttribute attribute)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            var title = $"{attribute.Name()} ({attribute.GetAttributeType()})";
            attributeFoldoutFlags[attribute] = EditorGUILayout.Foldout(attributeFoldoutFlags[attribute], title);
            if (attributeFoldoutFlags[attribute])
                DrawAttributeContent(attribute);
            
            EditorGUILayout.EndVertical();
            EditorGUI.indentLevel--;
        }
    
        private void DrawAttributeContent(IAttribute attribute)
        {
            EditorGUI.indentLevel++;
            
            EditorGUILayout.LabelField($"Type:{attribute.GetType().Name}");
            EditorGUILayout.LabelField($"Data Type:{attribute.GetDataType().Name}");
            EditorGUILayout.LabelField($"Dimension:[{attribute.GetDimension()}]");
            EditorGUILayout.LabelField(attribute.ToString());
            
            DrawValueInfo(attribute);
            DrawFrameList(attribute);
            
            EditorGUI.indentLevel--;
        }
    
        private void DrawValueInfo(IAttribute attribute)
        {
            attribute.DrawAttributeDetailInspector();
        }
    
        private void DrawFrameList(IAttribute attribute)
        {
            EditorGUILayout.BeginHorizontal();
            // EditorStyles.helpBoxにインデントを効かせる記述
            GUILayout.Space(GetIndentSize());
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            var frameListTitle = $"Frame List : Total {attribute.FrameCount()} Frames";
            // EditorStyles.helpBoxにインデントを効かせた影響を吸収する。これがないとGUIが右にずれていく
            var currentRect = GUILayoutUtility.GetRect(new GUIContent(frameListTitle), EditorStyles.foldout);
            currentRect.xMin -= (EditorGUI.indentLevel-1)* 15;
            frameListFoldoutFlags[attribute] = EditorGUI.Foldout(currentRect, frameListFoldoutFlags[attribute], frameListTitle);
            if (frameListFoldoutFlags[attribute])
                DrawFrameListContent(attribute);
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
    
        private void DrawFrameListContent(IAttribute attribute)
        {
            var frameCount = attribute.FrameCount();
            for (var i = 0; i < frameCount; i++)
                DrawFrame(attribute, i);
        }
    
        private void DrawFrame(IAttribute attribute, int i)
        {
            EditorGUILayout.BeginHorizontal();
            // EditorStyles.helpBoxにインデントを効かせる記述
            GUILayout.Space(GetIndentSize());
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            var frames = attribute.GetObjectFrames();
            var frameTitle = $"Frame[{i}]: {frames[i].Count} Elements , {frames[i][0].Count} Components";
            // EditorStyles.helpBoxにインデントを効かせた影響を吸収する。これがないとGUIが右にずれていく
            var currentRect = GUILayoutUtility.GetRect(new GUIContent(frameTitle), EditorStyles.foldout);
            currentRect.xMin -= (EditorGUI.indentLevel-1)*15;
            frameFoldoutFlags[(attribute,i)] = EditorGUI.Foldout(currentRect, frameFoldoutFlags[(attribute,i)], frameTitle);
            if (frameFoldoutFlags[(attribute,i)])
                DrawFrameContent(frames[i]);
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
        
        private void DrawFrameContent(List<List<object>> valuesInFrame)
        {
            foreach (var data in valuesInFrame)
            {
                var txt = "";
                foreach (var component in data)
                    txt += component + ", ";
                EditorGUILayout.LabelField(txt);
            }
        }
    
        private float GetIndentSize()
        {
            return EditorGUI.indentLevel * 15;
        }
    }
}
