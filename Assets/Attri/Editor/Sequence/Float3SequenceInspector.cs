using System;
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

        private struct FloatData
        {
            public readonly uint Sign;
            public readonly uint Exponent;
            public readonly uint Mantissa;
            public FloatData(uint bits)
            {
                Sign = bits >> 31;
                Exponent = (bits >> 23) & 0xff;
                Mantissa = bits & 0x7fffff;
            }
        }
        private struct Float3Data
        {
            public readonly uint3 Sign;
            public readonly uint3 Exponent;
            public readonly uint3 Mantissa;
            public Float3Data(uint3 bits)
            {
                Sign = new uint3(bits.x >> 31, bits.y >> 31, bits.z >> 31);
                Exponent = new uint3((bits.x >> 23) & 0xff, (bits.y >> 23) & 0xff, (bits.z >> 23) & 0xff);
                Mantissa = new uint3(bits.x & 0x7fffff, bits.y & 0x7fffff, bits.z & 0x7fffff);
            }
        }
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
            
            public readonly bool3 signed;
            public readonly uint3 exponentMin;
            public readonly uint3 exponentMax;
            public readonly uint3 exponentRange;
            public readonly uint3 exponentBitDepth;
            public readonly bool lengthSigned;
            public readonly uint lengthExponentMin;
            public readonly uint lengthExponentMax;
            public readonly uint lengthExponentRange;
            public readonly uint lengthExponentBitDepth;
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
                
                var uints = values.AsUint().ToArray();
                var lengthUints = length.AsUint().ToArray();
                union = uints.Aggregate((a, b) => a | b);
                lengthUnion = lengthUints.Aggregate((a, b) => a | b);
                // var bitData = uints.Select(u => new Float3Data(u));
                // var lengthBitData = lengthUints.Select(u => new FloatData(u));
                
                var minBits = new Float3Data(min.AsUint());
                var maxBits = new Float3Data(max.AsUint());
                var lengthMinBits = new FloatData(lengthMin.AsUint());
                var lengthMaxBits = new FloatData(lengthMax.AsUint());
                signed = new bool3(minBits.Sign.x != maxBits.Sign.x, minBits.Sign.y != maxBits.Sign.y, minBits.Sign.z != maxBits.Sign.z);
                exponentMin = new uint3(minBits.Exponent.x, minBits.Exponent.y, minBits.Exponent.z)-127;
                exponentMax = new uint3(maxBits.Exponent.x, maxBits.Exponent.y, maxBits.Exponent.z)-127;
                exponentRange = exponentMax - exponentMin;
                exponentBitDepth = new uint3(exponentRange.x.BitDepth(), exponentRange.y.BitDepth(), exponentRange.z.BitDepth());
                lengthSigned = lengthMinBits.Sign != lengthMaxBits.Sign;
                lengthExponentMin = lengthMinBits.Exponent-127;
                lengthExponentMax = lengthMaxBits.Exponent-127;
                lengthExponentRange = lengthExponentMax - lengthExponentMin;
                lengthExponentBitDepth = lengthExponentRange.BitDepth();
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
            DrawRowLabels("      ", new []{"Bit Union", "Signed", "Exponent Min", "Exponent Max", "Exponent Range", "Exponent Bit Depth"}, new []{2.1f, 0.5f});
            DrawBitInfoRow("     x", data.union.x, data.signed.x, data.exponentMin.x, data.exponentMax.x, data.exponentRange.x, data.exponentBitDepth.x);
            DrawBitInfoRow("     y", data.union.y, data.signed.y, data.exponentMin.y, data.exponentMax.y, data.exponentRange.y, data.exponentBitDepth.y);
            DrawBitInfoRow("     z", data.union.z, data.signed.z, data.exponentMin.z, data.exponentMax.z, data.exponentRange.z, data.exponentBitDepth.z);
            DrawBitInfoRow("length", data.lengthUnion, data.lengthSigned, data.lengthExponentMin, data.lengthExponentMax, data.lengthExponentRange, data.lengthExponentBitDepth);
        }
        private void DrawBitInfoRow(string label, uint bitUnion, bool signed, uint exponentMin, uint exponentMax, uint exponentRange, uint exponentBitDepth)
        {
            var bitUnionStr = Convert.ToString(bitUnion, 2).PadLeft(32, '0');
            bitUnionStr = bitUnionStr.Insert(1, " ");
            bitUnionStr = bitUnionStr.Insert(10, " ");
            var signedStr = signed ? "True" : "False";
            var exponentMinStr = exponentMin.ToString();
            var exponentMaxStr = exponentMax.ToString();
            var exponentRangeStr = exponentRange.ToString();
            var exponentBitDepthStr = exponentBitDepth.ToString();
            DrawRowLabels(label,
                new []{bitUnionStr, signedStr, exponentMinStr, exponentMaxStr, exponentRangeStr, exponentBitDepthStr },
                new []{2.1f, 0.5f}
                );
        }
        
        private void DrawRowLabels(string label, string[] rowStrings, float[] widthScale = null)
        {
            using (new EditorGUILayout.HorizontalScope(GUILayout.ExpandWidth(true)))
            {
                EditorGUILayout.SelectableLabel(label   , GUILayout.Width(ComponentLabelWidth), _singleHeightOption);
                widthScale ??= Array.Empty<float>();
                {
                    for (int i = 0; i < rowStrings.Length; i++)
                    {
                        var scale = widthScale.Length <= i ? 1 : widthScale[i];
                        EditorGUILayout.SelectableLabel(rowStrings[i], GUILayout.Width(ValueLabelWidth*scale), _singleHeightOption);
                    }
                        
                }
            }
        }
    }
}
