using System;
using System.Collections.Generic;
using System.Linq;
using MessagePack;
using UnityEditor;
#if UNITY_EDITOR
using UnityEngine;
#endif

namespace Attri.Runtime
{
    [MessagePackObject(true)]
    [Serializable]
    public class Vector2Attribute : AttributeBase<Vector2>
    {
        public override AttributeType GetAttributeType() => AttributeType.Float;
        public override ushort GetDimension() => 2;
        public Vector2Attribute() : base(nameof(Vector2Attribute)) {}
        public Vector2Attribute(string name) : base(name) {}
        public override List<byte[]> GeByte(int frameIndex)
        {
            var frame = frames[frameIndex];
            var data = new List<byte[]>();
            foreach (var v in frame.data)
            {
                var array = new byte[GetByteSize()];
                var x = BitConverter.GetBytes(v.x);
                var y = BitConverter.GetBytes(v.y);
                array[0] = x[0];
                array[1] = x[1];
                array[2] = x[2];
                array[3] = x[3];
                array[4] = y[0];
                array[5] = y[1];
                array[6] = y[2];
                array[7] = y[3];
                data.Add(array);
            }
            return data;
        }

        public override int GetByteSize()
        {
            return sizeof(float) * 2;
        }

        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<Vector2AttributeAsset>();
            asset.name = name;
            asset.attribute = this;
            return asset;
        }
#if UNITY_EDITOR
        private Vector2Int _divisionPow = new Vector2Int(4, 4);
        public override void DrawAttributeDetailInspector()
        {
            Vector2 max = new Vector2(float.MinValue, float.MinValue);
            Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
            frames.ForEach(frame =>
            {
                frame.data.ForEach(data =>
                {
                    max.x = Mathf.Max(max.x, data.x);
                    max.y = Mathf.Max(max.y, data.y);
                    min.x = Mathf.Min(min.x, data.x);
                    min.y = Mathf.Min(min.y, data.y);
                });
            });
            var range = new Vector3(max.x - min.x, max.y - min.y);
            var total = frames.Sum(frame => frame.data.Count);
            var height = 50;
            var axisLabel = new [] {"X", "Y"};
            for (var axis = 0; axis < axisLabel.Length; axis++)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField($"{axisLabel[axis]}: [{min[axis]:F4} ~ {max[axis]:F4}] : Range: {range[axis]:F4}");
                var div = (int)Mathf.Pow(2, _divisionPow[axis]);
                var distribution = new int[div];
                frames.ForEach(frame =>
                {
                    frame.data.ForEach(data =>
                    {
                        var index = (int)((data[axis] - min[axis]) / range[axis] * div);
                        if(index == div) index--;
                        distribution[index]++;
                    });
                });
                // Label & Slider
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"{axisLabel[axis]} Distribution");
                _divisionPow[axis] = EditorGUILayout.IntSlider(_divisionPow[axis], 0, 8);
                EditorGUILayout.EndHorizontal();
                // Draw background
                var rect = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect(false, height));
                EditorGUI.DrawRect(rect, Color.gray);
                var distributionRect = new Rect(rect);
                distributionRect.width = rect.width / div -1;
                var counterRect = EditorGUI.IndentedRect(EditorGUILayout.GetControlRect(true));
                var counterLabelStyle = new GUIStyle(EditorStyles.label);
                counterLabelStyle.alignment = TextAnchor.MiddleCenter;
                counterLabelStyle.overflow = new RectOffset(10, 10, 10, 10);
                // Draw distribution
                EditorGUILayout.BeginHorizontal();
                for (var i = 0; i < div; i++)
                {
                    var h = rect.height * ((float)distribution[i] / total);
                    distributionRect.height = h;
                    distributionRect.x = rect.x + distributionRect.width * i + i;
                    distributionRect.y = rect.y + rect.height - h;
                    counterRect.x = rect.x + distributionRect.width * i + i;
                    counterRect.width = Mathf.Max(distributionRect.width,5);
                    EditorGUILayout.BeginVertical();
                    EditorGUI.DrawRect(distributionRect, Color.white);
                    if(distribution[i] != 0) 
                        GUI.Label(counterRect, $"{distribution[i]}", counterLabelStyle);
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
        }
#endif
    }
}
