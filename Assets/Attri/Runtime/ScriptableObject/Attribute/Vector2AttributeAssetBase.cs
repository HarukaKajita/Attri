using System.Collections.Generic;
using UnityEngine;
using yutokun;

namespace Attri.Runtime
{
    [AttributeType(AttributeType.Float, 2)]
    public class Vector2AttributeAssetBase : AttributeAssetBase
    {
        List<Vector2> values = new();

        protected override void SetFromPackedAttribute(PackedAttribute packedAttribute)
        {
            base.SetFromPackedAttribute(packedAttribute);
            values = new List<Vector2>();
            foreach (var valueCsv in packedAttribute.valueCsvList)
            {
                var oneLineCsv = valueCsv.ReplaceCrlf();
                var valueList = CSVParser.LoadFromString(oneLineCsv)[0].ConvertAll(float.Parse);
                // 値を2つずつ取り出してベクトルに変換
                for (var i = 0; i < valueList.Count; i += 2)
                    values.Add(new Vector2(valueList[i], valueList[i + 1]));
            }
        }
    }
}
