using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Attri.Runtime
{
    public class Sequence : ScriptableObject
    {
#if UNITY_EDITOR
        public DefaultAsset containerFolder;
#endif
        public string attributeName;
        public List<Container> containers = new();
        private Type _attributeType;
        public int ContainerCount()
        {
            return containers.Count;
        }

        public int FrameCount => ContainerCount();
        
        public Type AttributeType()
        {
            return _attributeType;
        }

#if UNITY_EDITOR
        [ContextMenu("Gather Container")]
        public void GatherContainer()
        {
            if (containerFolder == null) return;
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
            _attributeType = containers[0].GetAttributeType();
        }
#endif
    }
}
