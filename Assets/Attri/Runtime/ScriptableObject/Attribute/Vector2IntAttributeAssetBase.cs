using System.Collections.Generic;
using UnityEngine;
using yutokun;

namespace Attri.Runtime
{
    [AttributeType(AttributeType.Integer, 2)]
    public class Vector2IntAttributeAssetBase : AttributeAssetBase
    {
        List<Vector2Int> values = new();

        protected override void SetFromPackedAttribute(PackedAttribute packedAttribute)
        {
            base.SetFromPackedAttribute(packedAttribute);
            values = new List<Vector2Int>();
            foreach (var valueCsv in packedAttribute.valueCsvList)
            {
                var oneLineCsv = valueCsv.ReplaceCrlf();
                var valueList = CSVParser.LoadFromString(oneLineCsv)[0].ConvertAll(int.Parse);
                // 値を2つずつ取り出してベクトルに変換
                for (var i = 0; i < valueList.Count; i += 2)
                    values.Add(new Vector2Int(valueList[i], valueList[i + 1]));
            }
        }
    }
}
