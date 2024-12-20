using UnityEngine;

namespace Attri.Runtime
{
    #if UNITY_EDITOR
    using UnityEditor;
    [CustomPropertyDrawer(typeof(DisableOnInspectorAttribute))]
    public class DisableOnInspectorAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
    #endif
}
