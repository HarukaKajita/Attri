using System.Collections.Generic;
using Attri.Runtime;
using Csv;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Attri.Editor
{
    public class Vector3AttributeImportProcessor : AttributeImportProcessor
    {
        internal override void Parse(AssetImportContext ctx, List<string> data)
        {
            base.Parse(ctx, data);
            foreach (var d in data)
            {
                CsvSerializer.Deserialize<Vector3>(d);
            }
            
        }
    }
}