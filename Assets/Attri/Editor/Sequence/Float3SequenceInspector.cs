using System;
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
        private readonly GUILayoutOption _singleHeightOption = GUILayout.Height(EditorGUIUtility.singleLineHeight);
        private const float ComponentLabelWidth = 50f;
        private const float ValueLabelWidth = 100;

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
            DrawComponentTable(sequence);
        }

        private void DrawComponentTable(Float3Sequence sequence)
        {
            // データの特性を表示
            var values = GetValues(sequence);
            var min = new float3(values.Min(v => v.x), values.Min(v => v.y), values.Min(v => v.z));
            var max = new float3(values.Max(v => v.x), values.Max(v => v.y), values.Max(v => v.z));
            var lenMin = values.Min(math.length);
            var lenMax = values.Max(math.length);
            
            var uints = GetUInt3Values(values);
            var union = uints.Aggregate((a, b) => a | b);
            
            var lengthUints = GetUInt3Values(values.Select(math.length).ToArray());
            var lengthUnion = lengthUints.Aggregate((a, b) => a | b);
            
            DrawComponentRow("      ", "Min", "Max", "Range", "bit Union");
            DrawComponentRow("     x", min.x, max.x, union.x);
            DrawComponentRow("     y", min.y, max.y, union.y);
            DrawComponentRow("     z", min.z, max.z, union.z);
            DrawComponentRow("length", lenMin, lenMax, lengthUnion);
        }

        private static uint3[] GetUInt3Values(float3[] values)
        {
            // floatのビット列をuintで解釈
            return values.Select(v => new uint3(
                GetUIntValue(v.x),
                GetUIntValue(v.y),
                GetUIntValue(v.z)
            )).ToArray();
        }
        private static uint[] GetUInt3Values(float[] values)
        {
            // floatのビット列をuintで解釈
            return values.Select(GetUIntValue).ToArray();
        }
        private static uint GetUIntValue(float value)
        {
            // floatのビット列をuintで解釈
            return BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
        }

        private void DrawComponentRow(string label, float min, float max, uint bitUnion)
        {
            var range = max - min;
            var minPad = min < 0 ? "" : "+";
            var maxPad = max < 0 ? "" : "+";
            var rangePad = range < 0 ? "" : "+";
            var minStr = minPad + min.ToString("0000.0000000");
            var maxStr = maxPad + max.ToString("0000.0000000");
            var rangeStr = rangePad + range.ToString("0000.0000000");
            var bitUnionStr = Convert.ToString(bitUnion, 2).PadLeft(32, '0');
            bitUnionStr = bitUnionStr.Insert(1, " ");
            bitUnionStr = bitUnionStr.Insert(10, " ");
            if (bitUnion == 0) bitUnionStr = "All 0";
            DrawComponentRow(label, minStr, maxStr, rangeStr, bitUnionStr);
        }

        private void DrawComponentRow(string label, string min, string max, string range, string bitUnion)
        {
            using (new EditorGUILayout.HorizontalScope(GUILayout.ExpandWidth(true)))
            {
                EditorGUILayout.SelectableLabel(label   , GUILayout.Width(ComponentLabelWidth), _singleHeightOption);
                EditorGUILayout.SelectableLabel(min     , GUILayout.Width(ValueLabelWidth), _singleHeightOption);
                EditorGUILayout.SelectableLabel(max     , GUILayout.Width(ValueLabelWidth), _singleHeightOption);
                EditorGUILayout.SelectableLabel(range   , GUILayout.Width(ValueLabelWidth), _singleHeightOption);
                EditorGUILayout.SelectableLabel(bitUnion, GUILayout.Width(ValueLabelWidth*2.2f), _singleHeightOption);
            }
        }
        
        private static void GatherContainers(Float3Sequence sequence)
        {
            sequence.GatherContainer();
        }
        
        private static float3[] GetValues(Float3Sequence sequence)
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
