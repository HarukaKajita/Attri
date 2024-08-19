using System.Collections.Generic;
using Attri.Runtime;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditorInternal;
using UnityEngine;

namespace Attri.Editor
{
    //.attrijsonファイルを読み込むインポーター
    //.jsonもimporter切り替えで扱えるようにしておく
    [ScriptedImporter(1,new[] {"attrijson","attri"}, new[] { "json" })]
    public class AttributeImporter : StackableImporter<AttributeImportProcessor>
    {
        
        // カスタムインスペクタで内容を観れるようにする
        [CustomEditor(typeof(AttributeImporter))]
        public class AttributeImporterInspector : UnityEditor.Editor
        {
            IEnumerable<IAttribute> attributes;
            Dictionary<IAttribute,bool> attributeFoldoutFlags = new();
            Dictionary<object,bool> frameListFoldoutFlags = new();
            Dictionary<(object, int frameId),bool> frameFoldoutFlags = new();

            public void OnEnable()
            {
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
            public override void OnInspectorGUI()
            {
                var importer = target as AttributeImporter;
                EditorGUI.BeginChangeCheck();
                serializedObject.UpdateIfRequiredOrScript();
                // Loop through properties and create one field (including children) for each top level property.
                SerializedProperty property = serializedObject.GetIterator();
                bool expanded = true;
                while (property.NextVisible(expanded))
                {
                    var isScriptProperty = "m_Script" == property.propertyPath;
                    var isProcessorsProperty = nameof(importer.processors) == property.propertyPath;
                    if (isProcessorsProperty) continue;
                    using (new EditorGUI.DisabledScope(isScriptProperty))
                    {
                        EditorGUILayout.PropertyField(property, true);
                    }
                    expanded = false;
                }
                // --- list
                SerializedProperty processors = serializedObject.FindProperty(nameof(importer.processors));
                var list = new ReorderableList(serializedObject, processors);
                list.draggable = true;
                list.drawHeaderCallback = rect => EditorGUI.LabelField(rect, nameof(importer.processors));
                list.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    rect.x += 8;
                    var element = processors.GetArrayElementAtIndex(index);
                    // Debug.Log($"{element.type} {element.name} {element.propertyPath} {element.propertyType}");
                    if (element.managedReferenceValue == null) 
                        EditorGUI.PropertyField(rect, element, new GUIContent(element.displayName));
                    else
                    {
                        var value = element.managedReferenceValue;
                        if(value.GetType() == typeof(MeshProcessor))
                            new MeshProcessorDrawer().OnGUI(rect, element, new GUIContent(element.displayName));
                        else
                            EditorGUI.PropertyField(rect, element, new GUIContent(element.displayName));
                    }
                };
                list.elementHeightCallback = index =>
                {
                    var element = processors.GetArrayElementAtIndex(index);
                    if (element.managedReferenceValue == null) return EditorGUI.GetPropertyHeight(element);
                    var value = element.managedReferenceValue;
                    if(value.GetType() == typeof(MeshProcessor))
                        return new MeshProcessorDrawer().GetPropertyHeight(element, new GUIContent(element.displayName));
                    else
                        return EditorGUI.GetPropertyHeight(element);
                };
                list.DoLayoutList();
                // ---
                serializedObject.ApplyModifiedProperties();
                var changed = EditorGUI.EndChangeCheck();
                if (changed)
                {
                    // repaint
                    EditorUtility.SetDirty(importer);
                } 
                // Foldout Editor GUI
                if (attributes == null) return;
                
                foreach (var attribute in attributes)
                    DrawAttribute(attribute);
                
                // Reimport Self
                if (GUILayout.Button("Reimport"))
                        AssetDatabase.ImportAsset(importer?.assetPath, ImportAssetOptions.ForceUpdate);
            }
        
            private void DrawAttribute(IAttribute attribute)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                
                var title = $"{attribute.Name()}({attribute.GetType().Name})";
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
                EditorGUILayout.LabelField($"Dimension:[{GetDimension(attribute)}]");
                EditorGUILayout.LabelField(attribute.ToString());
                
                DrawValueInfo(attribute);
                DrawFrameList(attribute);
                
                EditorGUI.indentLevel--;
            }
            
            ushort GetDimension(IAttribute attribute)
            {
                var attributeDataType = attribute.GetDataType();
                if (attributeDataType == typeof(Vector3) || attributeDataType == typeof(Vector3Int)) return 3;
                if (attributeDataType == typeof(Vector2) || attributeDataType == typeof(Vector2Int)) return 2;
                return 1;
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
                var frames = attribute.GetObjectFrames();
                for (var i = 0; i < frames.Count; i++)
                    DrawFrame(attribute, frames, i);
            }
        
            private void DrawFrame(IAttribute attribute, List<List<object>> frames, int i)
            {
                EditorGUILayout.BeginHorizontal();
                // EditorStyles.helpBoxにインデントを効かせる記述
                GUILayout.Space(GetIndentSize());
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                
                var frameTitle = $"Frame[{i}][]: {frames[i].Count} Elements";
                // EditorStyles.helpBoxにインデントを効かせた影響を吸収する。これがないとGUIが右にずれていく
                var currentRect = GUILayoutUtility.GetRect(new GUIContent(frameTitle), EditorStyles.foldout);
                currentRect.xMin -= (EditorGUI.indentLevel-1)*15;
                frameFoldoutFlags[(attribute,i)] = EditorGUI.Foldout(currentRect, frameFoldoutFlags[(attribute,i)], frameTitle);
                if (frameFoldoutFlags[(attribute,i)])
                    DrawFrameContent(frames[i]);
                
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }
            
            private void DrawFrameContent(List<object> valuesInFrame)
            {
                foreach (var data in valuesInFrame)
                    EditorGUILayout.LabelField(data.ToString());
            }
        
            private float GetIndentSize()
            {
                return EditorGUI.indentLevel * 15;
            }
        }
    }
}
