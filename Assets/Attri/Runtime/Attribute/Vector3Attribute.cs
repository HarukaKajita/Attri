using System;
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
    public class Vector3Attribute : AttributeBase<Vector3>
    {
        public override AttributeType GetAttributeType() => AttributeType.Float;
        public override ushort GetDimension() => 3;
        public Vector3Attribute() : base(nameof(Vector3Attribute)) {}
        public Vector3Attribute(string name) : base(name) {}
        
        public override AttributeAsset CreateAsset()
        {
            var asset = ScriptableObject.CreateInstance<Vector3AttributeAsset>();
            asset.name = name;
            asset._attribute = this;
            return asset;
        }

#if UNITY_EDITOR
        private Vector3Int _divisionPow = new Vector3Int(4, 4, 4);
        public override void DrawAttributeDetailInspector()
        {
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            frames.ForEach(frame =>
            {
                frame.data.ForEach(data =>
                {
                    max.x = Mathf.Max(max.x, data.x);
                    max.y = Mathf.Max(max.y, data.y);
                    max.z = Mathf.Max(max.z, data.z);
                    min.x = Mathf.Min(min.x, data.x);
                    min.y = Mathf.Min(min.y, data.y);
                    min.z = Mathf.Min(min.z, data.z);
                });
            });
            var range = new Vector3(max.x - min.x, max.y - min.y, max.z - min.z);
            var total = frames.Sum(frame => frame.data.Count);
            var height = 50;
            var axisLabel = new [] {"X", "Y", "Z"};
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
