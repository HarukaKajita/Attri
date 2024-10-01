using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Attri.Runtime;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

namespace Attri.Editor
{
    
    [CustomEditor(typeof(Float3Sequence), true) , CanEditMultipleObjects]
    public class Float3SequenceInspector : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            // 通常のInspectorを表示
            base.OnInspectorGUI();
            // base.hasUnsavedChanges
            
            var sequence = target as Float3Sequence;
            if (sequence == null) return;
            
            // セットアップボタン
            if(GUILayout.Button("Gather Containers"))
                GatherContainers(sequence);
            
            var containers = sequence.containers;
            if (containers == null) return;
            
            // データの特性を表示
            var values = GetValues(sequence);
            var min = new float3(values.Min(v => v.x), values.Min(v => v.y), values.Min(v => v.z));
            var max = new float3(values.Max(v => v.x), values.Max(v => v.y), values.Max(v => v.z));
            // var average = new float3(values.Average(v => v.x), values.Average(v => v.y), values.Average(v => v.z));
            var singleHeightOption = GUILayout.Height(EditorGUIUtility.singleLineHeight);
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.SelectableLabel("", GUILayout.Width(30), singleHeightOption);
                EditorGUILayout.SelectableLabel("Min", GUILayout.Width(100), singleHeightOption);
                EditorGUILayout.SelectableLabel("Max", GUILayout.Width(100), singleHeightOption);
                EditorGUILayout.SelectableLabel("Range", GUILayout.Width(100), singleHeightOption);
            }
            
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.SelectableLabel("x:", GUILayout.Width(30), singleHeightOption);
                EditorGUILayout.SelectableLabel($"{min.x}", GUILayout.Width(100), singleHeightOption);
                EditorGUILayout.SelectableLabel($"{max.x}", GUILayout.Width(100), singleHeightOption);
                EditorGUILayout.SelectableLabel($"{max.x - min.x}", GUILayout.Width(100), singleHeightOption);
            }
            
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.SelectableLabel("y:", GUILayout.Width(30), singleHeightOption);
                EditorGUILayout.SelectableLabel($"{min.y}", GUILayout.Width(100), singleHeightOption);
                EditorGUILayout.SelectableLabel($"{max.y}", GUILayout.Width(100), singleHeightOption);
                EditorGUILayout.SelectableLabel($"{max.y - min.y}", GUILayout.Width(100), singleHeightOption);
            }
            
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.SelectableLabel("z:", GUILayout.Width(30), singleHeightOption);
                EditorGUILayout.SelectableLabel($"{min.z}", GUILayout.Width(100), singleHeightOption);
                EditorGUILayout.SelectableLabel($"{max.z}", GUILayout.Width(100), singleHeightOption);
                EditorGUILayout.SelectableLabel($"{max.z - min.z}", GUILayout.Width(100), singleHeightOption);
            }
            
        }
        
        private void GatherContainers(Float3Sequence sequence)
        {
            sequence.GatherContainer();
        }
        
        private float3[] GetValues(Float3Sequence sequence)
        {
            var valuesCount = sequence.containers.Select(container => container.values.Count).Sum();
            var values = new List<float3>(valuesCount);
            foreach (var container in sequence.containers)
            {
                if (container == null) continue;
                values.AddRange(container.values);
            }
            return values.ToArray();
        } 
    }
}
