using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Attri.Editor
{
    [CustomPropertyDrawer(typeof(MeshProcessor))]
    public class MeshProcessorDrawer : ImportProcessorDrawer
    {
        private string[] _attributeNames = Array.Empty<string>();
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
            var processor = property.managedReferenceValue as MeshProcessor;
            Preparation(processor);
            var defaultHeight = base.GetPropertyHeight(property, label);
            position.y += defaultHeight;
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                var additionalRect = new Rect(position);
                additionalRect.height = EditorGUIUtility.singleLineHeight;
                additionalRect = EditorGUI.IndentedRect(additionalRect);
                // debug
                // EditorGUI.DrawRect(additionalRect, Color.red);
                foreach (var selection in processor.attributeSelections)
                {
                    DrawAttributeSelection(selection, additionalRect);
                    additionalRect.y += EditorGUIUtility.singleLineHeight;
                }
                property.SetValue(processor);
                EditorGUI.indentLevel--;
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var h = base.GetPropertyHeight(property, label);
            var processor = property.managedReferenceValue as MeshProcessor;
            if(property.isExpanded)
                h += EditorGUIUtility.singleLineHeight * processor.attributeSelections.Length;
            return h;
        }
        private void DrawAttributeSelection( AttributeSelection selection, Rect position)
        {
            var labelWidth = 100;//EditorGUIUtility.labelWidth/2;
            var attributeFormatPopupWidth = 95;
            var remainedWidth = (position.width - labelWidth - attributeFormatPopupWidth);
            var attributeNamePopupWidth = remainedWidth*0.4f;//80;
            var attributeDimensionSliderWidth = remainedWidth*0.6f;//120;
            
            var labelRect = new Rect(position.x, position.y, labelWidth, position.height);
            var optionRect = new Rect(position.x + labelWidth, position.y, attributeNamePopupWidth, position.height);
            EditorGUI.LabelField(labelRect, selection.AttributeName());
            selection.index = EditorGUI.Popup(optionRect, "", selection.index, _attributeNames);
            optionRect.x += optionRect.width-12;
            optionRect.width = attributeFormatPopupWidth;
            selection.format = (VertexAttributeFormat)EditorGUI.EnumPopup(optionRect, selection.format);
            optionRect.x += optionRect.width;
            optionRect.width = attributeDimensionSliderWidth;
            selection.dimension = EditorGUI.IntSlider(optionRect, "", selection.dimension, 1, 4);
            
            if (selection.index >= _attributeNames.Length || selection.index < 0)
                selection.index = 0;
            selection.fetchAttributeName = _attributeNames[selection.index];
        }

        void Preparation(MeshProcessor processor)
        {
            if (processor == null) return;
            if (processor.attributes == null) return;
            // アトリビュート名の配列を作る
            var attributes = processor.attributes;
            // メッシュに含めないアトリビュート用に"_"を追加
            _attributeNames = attributes.Select(a => a.Name()).Prepend("_").ToArray();
            // fileのアトリビュートの順の変更や増減があった場合に、選択しているアトリビュートのindexがずれるので、それを修正する
            foreach (var selection in processor.attributeSelections)
            {
                var selectedIndex = selection.index;
                var selectedName = selection.fetchAttributeName;
                if (selectedIndex >= _attributeNames.Length || selectedIndex < 0) selectedIndex = 0;
                var currentAttributeName = _attributeNames[selectedIndex];
                if (currentAttributeName != selectedName)
                    selection.index = Array.IndexOf(_attributeNames, selectedName);
            }
        }
    }
}
