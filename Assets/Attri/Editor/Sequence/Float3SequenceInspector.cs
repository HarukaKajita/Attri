using System;
using System.Collections.Generic;
using System.Linq;
using Attri.Runtime;
using Attri.Runtime.Extensions;
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

        private struct AnalysisData
        {
            public readonly float3[] values;
            public readonly float3 min;
            public readonly float3 max;
            public readonly float3 range;
            public readonly float lengthMin;
            public readonly float lengthMax;
            public readonly float lengthRange;
            public readonly uint3 union;
            public readonly uint lengthUnion;
            public AnalysisData(Float3Sequence sequence)
            {
                values = sequence.GetValues();
                min = new float3(values.Min(v => v.x), values.Min(v => v.y), values.Min(v => v.z));
                max = new float3(values.Max(v => v.x), values.Max(v => v.y), values.Max(v => v.z));
                range = max - min;
                var length = values.Select(math.length).ToArray();
                lengthMin = length.Min();
                lengthMax = length.Max();
                lengthRange = lengthMax - lengthMin;
                
                var uints = values.AsUint3();
                union = uints.Aggregate((a, b) => a | b);
                var lengthUints = length.AsUint();
                lengthUnion = lengthUints.Aggregate((a, b) => a | b);
            }
        }
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
            
            AnalysisData data = new AnalysisData(sequence);
            // データの特性を表示
            DrawVectorTable(data);
            DrawBitInfoTable(data);
        }
        private static void GatherContainers(Float3Sequence sequence)
        {
            sequence.GatherContainer();
        }

        private void DrawVectorTable(AnalysisData data)
        {
            // データの特性を表示\
            DrawRowLabels("      ", new []{"Min", "Max", "Range"});
            DrawVectorRow("     x", data.min.x, data.max.x, data.range.x);
            DrawVectorRow("     y", data.min.y, data.max.y, data.range.y);
            DrawVectorRow("     z", data.min.z, data.max.z, data.range.z);
            DrawVectorRow("length", data.lengthMin, data.lengthMax, data.lengthRange);
        }
        private void DrawVectorRow(string label, float min, float max, float range)
        {
            var minStr = StringFloat(min);
            var maxStr = StringFloat(max);
            var rangeStr = StringFloat(range);
            DrawRowLabels(label, new []{minStr, maxStr, rangeStr});
        }
        private string StringFloat(float value)
        {
            var pad = value < 0 ? "" : "+";
            return pad + value.ToString("0000.0000000");
        }
        private void DrawBitInfoTable(AnalysisData data)
        {
            // データの特性を表示
            DrawRowLabels("      ", new []{"Bit Union"});
            DrawBitInfoRow("     x", data.union.x);
            DrawBitInfoRow("     y", data.union.y);
            DrawBitInfoRow("     z", data.union.z);
            DrawBitInfoRow("length", data.lengthUnion);
        }
        private void DrawBitInfoRow(string label, uint bitUnion)
        {
            var bitUnionStr = Convert.ToString(bitUnion, 2).PadLeft(32, '0');
            bitUnionStr = bitUnionStr.Insert(1, " ");
            bitUnionStr = bitUnionStr.Insert(10, " ");
            DrawRowLabels(label, new []{bitUnionStr}, 2.2f);
        }
        
        private void DrawRowLabels(string label, string[] rowStrings, float widthScale = 1)
        {
            using (new EditorGUILayout.HorizontalScope(GUILayout.ExpandWidth(true)))
            {
                EditorGUILayout.SelectableLabel(label   , GUILayout.Width(ComponentLabelWidth), _singleHeightOption);
                foreach (var rowString in rowStrings)
                    EditorGUILayout.SelectableLabel(rowString, GUILayout.Width(ValueLabelWidth*widthScale), _singleHeightOption);
            }
        }
    }
}
