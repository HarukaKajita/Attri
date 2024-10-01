using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Attri.Runtime;
using UnityEditor;
using UnityEngine;

namespace Attri.Editor
{
    
    [CustomEditor(typeof(FloatSequence), true) /*, CanEditMultipleObjects*/]
    public class FloatSequenceInspector : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            // 通常のInspectorを表示
            base.OnInspectorGUI();
            // base.hasUnsavedChanges
            
            var sequence = target as FloatSequence;
            if (sequence == null) return;
            
            // セットアップボタン
            if(EditorGUILayout.LinkButton("Gather Containers"))
                GatherContainers(sequence);
            
            var containers = sequence.containers;
            if (containers == null) return;
            
            // データの特性を表示
            var values = GetValues(sequence);
            var min = values.Min();
            var max = values.Max();
            var average = values.Average();
            EditorGUILayout.LabelField($"Min: {min}");
            EditorGUILayout.LabelField($"Max: {max}");
            EditorGUILayout.LabelField($"Average: {average}");
            
        }
        
        private void GatherContainers(FloatSequence sequence)
        {
            sequence.GatherContainer();
        }
        
        private float[] GetValues(FloatSequence sequence)
        {
            var valuesCount = sequence.containers.Select(container => container.values.Count).Sum();
            var values = new List<float>(valuesCount);
            foreach (var container in sequence.containers)
            {
                if (container == null) continue;
                values.AddRange(container.values);
            }
            return values.ToArray();
        } 
    }
}
