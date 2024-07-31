using System;
using System.Collections.Generic;
using UnityEditor.AssetImporters;

namespace Attri.Runtime
{
    [Serializable]
    public abstract class AttributeImportProcessor
    {
        internal virtual void Parse(AssetImportContext ctx, List<string> data)
        {
            // foreach (var d in data)
            // {
            //     CsvSerializer.Deserialize<float>(d);
            // }
        }
    }
}