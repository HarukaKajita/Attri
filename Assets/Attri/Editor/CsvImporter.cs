using UnityEditor.AssetImporters;

namespace Attri.Editor
{
    //.attricsvファイルを読み込むインポーター
    //.csvもimporter切り替えで扱えるようにしておく
    [ScriptedImporter(1,new[] {"attricsv"}, new[] { "csv" })]
    public class CsvImporter : StackableImporter<CsvImportProcessor>
    {
    }
}
