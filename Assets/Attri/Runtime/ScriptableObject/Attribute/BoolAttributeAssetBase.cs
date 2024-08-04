using System.Collections.Generic;
using System.Linq;
using System;
using yutokun;

namespace Attri.Runtime
{
    [AttributeType(AttributeType.Bool, 1)]
    public class BoolAttributeAssetBase : AttributeAssetBase
    {
        List<List<bool>> values = new ();
        
        protected override void SetFromPackedAttribute(PackedAttribute packedAttribute)
        {
            base.SetFromPackedAttribute(packedAttribute);
            values = new List<List<bool>>();
            foreach (var valueCsv in packedAttribute.valueCsvList)
            {
                // csvから改行を,に変換して読み込む
                var oneLineCsv = valueCsv.ReplaceCrlf();
                var strings = CSVParser.LoadFromString(oneLineCsv)[0];
                var valuesInFrame = strings.Select(x => int.Parse(x) != 0).ToList();
                values.Add(valuesInFrame);
            }
        }
    }
}
