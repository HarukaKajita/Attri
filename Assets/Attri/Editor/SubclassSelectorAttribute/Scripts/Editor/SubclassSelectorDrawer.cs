//based on https://qiita.com/tsukimi_neko/items/7922b2433ed4d8616cce#%E5%8F%82%E8%80%83
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
public class SubclassSelectorDrawer : PropertyDrawer
{
    bool initialized = false;
    Type[] inheritedTypes;
    string[] typePopupNameArray;
    string[] typeFullNameArray;
    int currentTypeIndex;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.ManagedReference) return;
        if(!initialized) {
            Initialize(property);
            GetCurrentTypeIndex(property.managedReferenceFullTypename);
            initialized = true;
        }
        int selectedTypeIndex = EditorGUI.Popup(GetPopupPosition(position), currentTypeIndex, typePopupNameArray);
        UpdatePropertyToSelectedTypeIndex(property, selectedTypeIndex);
        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true);
    }

    private void Initialize(SerializedProperty property)
    {
        GetAllInheritedTypes(GetType(property));
        GetInheritedTypeNameArrays();
    }

    private void GetCurrentTypeIndex(string typeFullName)
    {
        currentTypeIndex = Array.IndexOf(typeFullNameArray, typeFullName);
    }

    /// <summary>
    /// Assemblyを検索して、インターフェイスや親クラスを実装、継承した子クラスのリストを取得
    /// </summary>
    /// <param name="baseType"></param>
    void GetAllInheritedTypes(Type baseType)
    {
        inheritedTypes = AppDomain.CurrentDomain.GetAssemblies()//アセンブリ
            .SelectMany(s => s.GetTypes())//型
            .Where(p => baseType.IsAssignableFrom(p) && p.IsClass)//条件に合う型に絞り込み
            .Where(p => !p.IsAbstract)//抽象クラスを除外(baseTypeそのものは除外したい)
            .Prepend(null)//nullを先頭に追加
            .ToArray();
    }

    private void GetInheritedTypeNameArrays()
    {
        typePopupNameArray = inheritedTypes.Select(type => type == null ? "null" : type.Name.ToString()).ToArray();
        typeFullNameArray = inheritedTypes.Select(type => type == null ? "" : $"{type.Assembly.ToString().Split(',')[0]} {type.FullName}").ToArray();
    }

    public void UpdatePropertyToSelectedTypeIndex(SerializedProperty property, int selectedTypeIndex)
    {
        if (currentTypeIndex == selectedTypeIndex) return;
        currentTypeIndex = selectedTypeIndex;
        Type selectedType = inheritedTypes[selectedTypeIndex];
        property.managedReferenceValue =
            selectedType == null ? null : Activator.CreateInstance(selectedType);
    }

    Rect GetPopupPosition(Rect currentPosition)
    {
        Rect popupPosition = new Rect(currentPosition);
        popupPosition.width -= EditorGUIUtility.labelWidth;
        popupPosition.x += EditorGUIUtility.labelWidth;
        popupPosition.height = EditorGUIUtility.singleLineHeight;
        return popupPosition;
    }

    public static Type GetType( SerializedProperty property )
    {
	    const BindingFlags bindingAttr =
			    BindingFlags.NonPublic |
			    BindingFlags.Public |
			    BindingFlags.FlattenHierarchy |
			    BindingFlags.Instance
		    ;

	    var propertyPaths = property.propertyPath.Split( '.' );
	    var parentType    = property.serializedObject.targetObject.GetType();
	    var fieldInfo     = parentType.GetField( propertyPaths[ 0 ], bindingAttr );
	    var fieldType     = fieldInfo.FieldType;

	    // 配列もしくはリストの場合
	    if ( propertyPaths.Contains( "Array" ) )
	    {
		    // 配列の場合
		    if ( fieldType.IsArray )
		    {
			    // GetElementType で要素の型を取得する
			    var elementType = fieldType.GetElementType();
			    return elementType;
		    }
		    // リストの場合
		    else
		    {
			    // GetGenericArguments で要素の型を取得する
			    var genericArguments = fieldType.GetGenericArguments();
			    var elementType      = genericArguments[ 0 ];
			    return elementType;
		    }
	    }

	    return fieldType;
    }
}
