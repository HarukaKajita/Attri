using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Attri.Editor
{
    [CustomPropertyDrawer(typeof(AttributeSelection))]
    public class AttributeSelectionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            position.height = EditorGUIUtility.singleLineHeight;
            // Debug.Log($"{property.propertyPath} {GetType().Name} {property.type} {property.isArray}");
            var selection = property.boxedValue as AttributeSelection;
            var labelWidth = 100;
            var attributeFormatPopupWidth = 80;
            var remainedWidth = (position.width - labelWidth - attributeFormatPopupWidth);
            var attributeNamePopupWidth = remainedWidth*0.4f;
            var attributeDimensionSliderWidth = remainedWidth*0.6f;

            var labelRect = new Rect(position.x, position.y, labelWidth, position.height);
            var optionRect = new Rect(position.x + labelWidth, position.y, attributeNamePopupWidth, position.height);
            EditorGUI.LabelField(labelRect, selection.AttributeName());
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            selection.fetchAttributeName = EditorGUI.DelayedTextField(optionRect, selection.fetchAttributeName);
            optionRect.xMin = optionRect.xMax;
            optionRect.width = attributeFormatPopupWidth;
            selection.format = (VertexAttributeFormat)EditorGUI.EnumPopup(optionRect, selection.format);
            optionRect.x += optionRect.width;
            optionRect.width = attributeDimensionSliderWidth;
            selection.dimension = EditorGUI.IntSlider(optionRect, selection.dimension, 1, 4);
            
            if (EditorGUI.EndChangeCheck())
            {
                property.FindPropertyRelative("format").enumValueIndex = (int)selection.format;
                property.FindPropertyRelative("dimension").intValue = selection.dimension;
                property.FindPropertyRelative("fetchAttributeName").stringValue = selection.fetchAttributeName;
            }
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
