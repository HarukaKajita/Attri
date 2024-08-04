using System.Collections.Generic;
using UnityEngine;
using yutokun;

namespace Attri.Runtime
{
    [AttributeType(AttributeType.Integer, 3)]
    public class Vector3IntAttributeAssetBase : AttributeAssetBase
    {
        List<Vector3Int> values = new();

        protected override void SetFromPackedAttribute(PackedAttribute packedAttribute)
        {
            base.SetFromPackedAttribute(packedAttribute);
            values = new List<Vector3Int>();
            foreach (var valueCsv in packedAttribute.valueCsvList)
            {
                var oneLineCsv = valueCsv.ReplaceCrlf();
                var valueList = CSVParser.LoadFromString(oneLineCsv)[0].ConvertAll(int.Parse);
                // 値を3つずつ取り出してベクトルに変換
                for (var i = 0; i < valueList.Count; i += 3)
                    values.Add(new Vector3Int(valueList[i], valueList[i + 1], valueList[i + 2]));
            }
        }
    }
}
