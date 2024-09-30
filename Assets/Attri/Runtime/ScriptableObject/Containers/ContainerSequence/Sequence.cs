using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Attri.Runtime
{
    public class Sequence<T> : ScriptableObject where T : Object
    {
#if UNITY_EDITOR
        public DefaultAsset containerFolder;
#endif
	    public string targetAttribute;
        public List<T> containers = new ();
        
#if UNITY_EDITOR
		public void GatherContainer()
		{
			if (containerFolder == null) return;
			containers.Clear();
			// 検索
			var guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", new []{AssetDatabase.GetAssetPath(containerFolder)});
			var containerDictionary = new Dictionary<float, T>();
			// 名前が一致する物だけを取得
			foreach (var guid in guids)
			{
				var path = AssetDatabase.GUIDToAssetPath(guid);
				var fileName = System.IO.Path.GetFileNameWithoutExtension(path);
				var splits = fileName.Split("_");
				var attributeName = splits[0];
				if (attributeName != targetAttribute) continue;
				var frameIndex = float.Parse(splits[1]);
				var container = AssetDatabase.LoadAssetAtPath<T>(path);
				containerDictionary.Add(frameIndex, container);
			}
			// フレーム番号順にソート
			containers = containerDictionary.OrderBy(x => x.Key).Select(x => x.Value).ToList();
		}
#endif
    }
}
