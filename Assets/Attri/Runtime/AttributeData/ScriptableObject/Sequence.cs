using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

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
        public List<Container> containers = new();
        [SerializeField]
        private AttributeDataType attributeType;
        [SerializeField]
        private int attributeDimension;
        public int ContainerCount()
        {
            return containers.Count;
        }
        public int FrameCount => ContainerCount();
        #region IDataProvider
        public int Dimension() => attributeDimension;
        public AttributeDataType GetAttributeType() => attributeType;
        public float[][] AsFloat() => containers.SelectMany(c => c.AsFloat()).ToArray();
        public int[][] AsInt() => containers.SelectMany(c => c.AsInt()).ToArray();
        public string[][] AsString() => containers.SelectMany(c => c.AsString()).ToArray();
        public ScriptableObject GetScriptableObject() => this;
        #endregion
        
#if UNITY_EDITOR
        [ContextMenu("Setup")]
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
            attributeType = containers[0].GetAttributeType();
            // アトリビュートの次元が一致しているか確認
            var dimensions = containers.Select(c => c.Dimension()).Distinct().ToArray();
            if (dimensions.Count() > 1) //"Dimension is not consistent"
                attributeDimension = -1;
            else
                attributeDimension = dimensions.First();
        }
#endif
    }
}
