using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Attri.Runtime
{
    public class Sequence : ScriptableObject, IDataProvider
    {
#if UNITY_EDITOR
        public DefaultAsset containerFolder;
#endif
        public string attributeName;
        [DisableOnInspector]
        public List<Container> containers = new();
        [SerializeField, DisableOnInspector]
        private AttributeDataType attributeType;
        public int ContainerCount()
        {
            return containers.Count;
        }
        public int FrameCount => ContainerCount();
        #region IDataProvider
        public AttributeDataType GetAttributeType() => attributeType;
        public int[][][] AsInt() => containers.Select(c => c.ElementsAsInt()).ToArray();
        public float[][][] AsFloat() => containers.Select(c => c.ElementsAsFloat()).ToArray();
        public string[][][] AsString() => containers.Select(c => c.ElementsAsString()).ToArray();
        public ScriptableObject GetScriptableObject() => this;
        #endregion
        
#if UNITY_EDITOR
        [ContextMenu("Setup")]
        public void GatherContainer()
        {
            if (containerFolder == null) return;
            SerializedObject so = new SerializedObject(this);
            so.Update();
            containers.Clear();
            // 検索
            var guids = AssetDatabase.FindAssets($"t:{nameof(Container)}", new []{AssetDatabase.GetAssetPath(containerFolder)});
            var containerDictionary = new Dictionary<float, Container>();
            // 名前が一致する物だけを取得
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
                var splits = fileName.Split("_");
                var dstAttributeName = splits[0];
                if (dstAttributeName != attributeName) continue;
                var frameIndex = float.Parse(splits[1]);
                var container = AssetDatabase.LoadAssetAtPath<Container>(path);
                containerDictionary.Add(frameIndex, container);
            }
            // フレーム番号順にソート
            containers = containerDictionary.OrderBy(x => x.Key).Select(x => x.Value).ToList();
            attributeType = containers[0].GetAttributeType();
            so.FindProperty("containers").arraySize = containers.Count;
            for (int i = 0; i < containers.Count; i++)
            {
                so.FindProperty("containers").GetArrayElementAtIndex(i).objectReferenceValue = containers[i];
            }
            so.FindProperty("attributeType").enumValueIndex = (int)attributeType;
            so.FindProperty("attributeName").stringValue = attributeName;
            so.FindProperty("containerFolder").objectReferenceValue = containerFolder;
            so.ApplyModifiedProperties();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this));
        }
#endif
    }
}
