using UnityEditor.AssetImporters;

namespace Attri.Editor
{
    //.attrijsonファイルを読み込むインポーター
    //.jsonもimporter切り替えで扱えるようにしておく
    [ScriptedImporter(1, new[] { "attrijson", "attri" }, new[] { "json" })]
    public class AttributeImporter : StackableImporter<AttributeImportProcessor> { }
}
