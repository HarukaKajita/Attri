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
            DrawVectorTable(sequence);
            DrawBitInfoTable(sequence);
        }
        private static void GatherContainers(Float3Sequence sequence)
        {
            sequence.GatherContainer();
        }

        private void DrawVectorTable(Float3Sequence sequence)
        {
            // データの特性を表示
            var values = sequence.GetValues();
            var min = new float3(values.Min(v => v.x), values.Min(v => v.y), values.Min(v => v.z));
            var max = new float3(values.Max(v => v.x), values.Max(v => v.y), values.Max(v => v.z));
            var lenMin = values.Min(math.length);
            var lenMax = values.Max(math.length);
            
            DrawRowLabels("      ", new []{"Min", "Max", "Range"});
            DrawVectorRow("     x", min.x, max.x);
            DrawVectorRow("     y", min.y, max.y);
            DrawVectorRow("     z", min.z, max.z);
            DrawVectorRow("length", lenMin, lenMax);
        }
        private void DrawVectorRow(string label, float min, float max)
        {
            var range = max - min;
            var minPad = min < 0 ? "" : "+";
            var maxPad = max < 0 ? "" : "+";
            var rangePad = range < 0 ? "" : "+";
            var minStr = minPad + min.ToString("0000.0000000");
            var maxStr = maxPad + max.ToString("0000.0000000");
            var rangeStr = rangePad + range.ToString("0000.0000000");
            DrawRowLabels(label, new []{minStr, maxStr, rangeStr});
        }
        private void DrawBitInfoTable(Float3Sequence sequence)
        {
            // データの特性を表示
            var values = sequence.GetValues();
            
            var uints = values.AsUint3();
            var union = uints.Aggregate((a, b) => a | b);
            
            var lengthUints = values.Select(math.length).ToArray().AsUint();
            var lengthUnion = lengthUints.Aggregate((a, b) => a | b);
            
            DrawRowLabels("      ", new []{"Bit Union"});
            DrawBitInfoRow("     x", union.x);
            DrawBitInfoRow("     y", union.y);
            DrawBitInfoRow("     z", union.z);
            DrawBitInfoRow("length", lengthUnion);
        }
        private void DrawBitInfoRow(string label, uint bitUnion)
        {
            var bitUnionStr = Convert.ToString(bitUnion, 2).PadLeft(32, '0');
            bitUnionStr = bitUnionStr.Insert(1, " ");
            bitUnionStr = bitUnionStr.Insert(10, " ");
            DrawRowLabels(label, new []{bitUnionStr});
        }
        

        private void DrawRowLabels(string label, string[] rowStrings)
        {
            using (new EditorGUILayout.HorizontalScope(GUILayout.ExpandWidth(true)))
            {
                EditorGUILayout.SelectableLabel(label   , GUILayout.Width(ComponentLabelWidth), _singleHeightOption);
                foreach (var rowString in rowStrings)
                    EditorGUILayout.SelectableLabel(rowString, GUILayout.Width(ValueLabelWidth), _singleHeightOption);
            }
        }
    }
}
