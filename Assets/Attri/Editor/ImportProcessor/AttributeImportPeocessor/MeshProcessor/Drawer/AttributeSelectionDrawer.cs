using System.Linq;
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
            position = EditorGUI.IndentedRect(position);
            var selection = property.boxedValue as AttributeSelection;
            var labelWidth = 100;
            var attributeFormatPopupWidth = 100;
            var remainedWidth = (position.width - labelWidth - attributeFormatPopupWidth);
            var attributeNamePopupWidth = remainedWidth*0.4f;
            var attributeDimensionSliderWidth = 50;

            var titleRect = new Rect(position.x, position.y, labelWidth, position.height);
            var optionRect = new Rect(titleRect.xMax+10, position.y, attributeNamePopupWidth, position.height);
            
            var iconName = selection.enabled ? "TestPassed" : "TestIgnored"; //"TestFailed"
            var toggleTitle = EditorGUIUtility.TrTextContentWithIcon(selection.AttributeName(), "State", iconName);
            var toggled = EditorGUI.LinkButton(titleRect, toggleTitle);
            if (toggled) selection.enabled = !selection.enabled;
            using (new EditorGUI.DisabledScope(!selection.enabled))
            {
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;
                selection.fetchAttributeName = EditorGUI.DelayedTextField(optionRect, selection.fetchAttributeName);
                optionRect.xMin = optionRect.xMax+10;
                optionRect.width = attributeFormatPopupWidth;
                var validFormats = selection.attribute.GetValidFormats();
                var displayFormatOptions = validFormats.Select(v=>v.ToString()).ToArray();
                var selectedIndex = ArrayUtility.IndexOf(validFormats, selection.format);
                selectedIndex = EditorGUI.Popup(optionRect, selectedIndex, displayFormatOptions);
                selection.format = validFormats[selectedIndex];
                optionRect.x += optionRect.width+10;
                optionRect.width = attributeDimensionSliderWidth;
                var dimensionArray = selection.GetDimensionArray();
                var dimensionIndex = ArrayUtility.IndexOf(dimensionArray, selection.dimension);
                if (dimensionIndex < 0) selection.dimension = dimensionArray[0];
                var displayOptions = dimensionArray.Select(v=>v.ToString()).ToArray();
                selection.dimension = EditorGUI.IntPopup(optionRect, selection.dimension ,displayOptions, dimensionArray);
                
                if (EditorGUI.EndChangeCheck())
                {
                    property.FindPropertyRelative("format").enumValueIndex = (int)selection.format;
                    property.FindPropertyRelative("dimension").intValue = selection.dimension;
                    property.FindPropertyRelative("fetchAttributeName").stringValue = selection.fetchAttributeName;
                }
                EditorGUI.indentLevel = indent;    
            }
            
            EditorGUI.EndProperty();
        }
    }
}
