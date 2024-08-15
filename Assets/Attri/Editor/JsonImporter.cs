using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Attri.Editor
{
    //.attrijsonファイルを読み込むインポーター
    //.jsonもimporter切り替えで扱えるようにしておく
    [ScriptedImporter(1,new[] {"attrijson"}, new[] { "json" })]
    public class JsonImporter : StackableImporter
    {
        // カスタムインスペクタで内容を観れるようにする
        [CustomEditor(typeof(JsonImporter))]
        public class JsonImporterInspector : UnityEditor.Editor
        {
            IEnumerable<IAttribute> attributes;
            Dictionary<IAttribute,bool> attributeFoldoutFlags = new();
            Dictionary<object,bool> frameListFoldoutFlags = new();
            Dictionary<(object, int frameId),bool> frameFoldoutFlags = new();
            
            
            private void OnEnable()
            {
                var importer = target as JsonImporter;
                var assetPath = importer.assetPath;
                var jsonText = File.ReadAllText(assetPath);
                byte[] data = File.ReadAllBytes(assetPath);
                var extension = Path.GetExtension(assetPath);
                if (extension is ".json" or ".attrijson")
                    data = AttributeSerializer.ConvertFromJson(jsonText);
                
                attributes = AttributeSerializer.DeserializeAsArray(data);
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
                base.OnInspectorGUI();
                var importer = target as JsonImporter;
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
                
                // DrawValueInfo(attribute);
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
                // var attributeGenericType = attributes.GetType();
                // var typeParameter = attributeGenericType.GetGenericArguments()[0];
                // var isNumeric = typeParameter == typeof(float) || typeParameter == typeof(int);
                //
                // if (!isNumeric) return;
                // var frames = attribute.frames();
                // if(frames.Count == 0) return;
                //
                // // 全フレームのデータを1次元リストに変換
                // var values = new List<object>();
                // foreach (var t in frames)
                //     for (var elementIndex = 0; elementIndex < t.data.Count; elementIndex++)
                //         values.Add(t.data[elementIndex]);
                //
                // var first = values.FirstOrDefault();
                // if(first == null) return;
                // var type = first.GetType();
                // EditorGUILayout.LabelField($"Deserialized Type :{type.Name}");
                // EditorGUILayout.LabelField($"IsArray :{type.IsArray}");
                    
                // EditorGUILayout.LabelField($"IComparable :{type.IsSubclassOf(typeof(IComparable))}");
                //
                // var isVector3 = type == typeof(Vector3);
                // var isVector3Int = type == typeof(Vector3Int);
                // var isVector2 = type == typeof(Vector2);
                // var isVector2Int = type == typeof(Vector2Int);
                // EditorGUILayout.LabelField($"IsVector3 :{isVector3}");
                // EditorGUILayout.LabelField($"IsVector3Int :{isVector3Int}");
                // EditorGUILayout.LabelField($"IsVector2 :{isVector2}");
                // EditorGUILayout.LabelField($"IsVector2Int :{isVector2Int}");
                return;
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
