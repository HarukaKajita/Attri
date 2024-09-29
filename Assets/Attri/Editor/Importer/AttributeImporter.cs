using System.IO;
using System.Linq;
using Attri.Runtime;
using UnityEditor.AssetImporters;

namespace Attri.Editor
{
    //.attrijsonファイルを読み込むインポーター
    //.jsonもimporter切り替えで扱えるようにしておく
    [ScriptedImporter(1, new[] { "attrijson", "attri" }, new[] { "json" })]
    public class AttributeImporter : StackableImporter<AttributeImportProcessor>
    {
        protected override void Init(bool forceDeserialization = false)
        {
            if (forceDeserialization)
            {
                var jsonText = File.ReadAllText(assetPath);
                var data = File.ReadAllBytes(assetPath);
                var extension = Path.GetExtension(assetPath);
                if (extension is ".json" or ".attrijson") 
                    data = AttributeSerializer.ConvertFromJson(jsonText);
                attributes = AttributeSerializer.DeserializeAsArray(data).ToList();
            }
            base.Init(forceDeserialization);
        }
    }
}
