using System;
using System.Collections.Generic;
using yutokun;

namespace Attri.Runtime
{
    [AttributeType(AttributeType.String, 0)]
    public class StringAttributeAssetBase : AttributeAssetBase
    {
        public List<string> values = new();

        protected override void SetFromPackedAttribute(PackedAttribute packedAttribute)
        {
            base.SetFromPackedAttribute(packedAttribute);
            values = new List<string>();
            foreach (var valueCsv in packedAttribute.valueCsvList)
            {
                var oneLineCsv = valueCsv.ReplaceCrlf();
                values = CSVParser.LoadFromString(oneLineCsv)[0];
            }
        }
    }
}
