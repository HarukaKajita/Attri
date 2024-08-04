using System.Collections.Generic;
using yutokun;

namespace Attri.Runtime
{
    [AttributeType(AttributeType.Integer, 1)]
    public class IntegerAttributeAssetBase : AttributeAssetBase
    {
        List<int> values = new();

        protected override void SetFromPackedAttribute(PackedAttribute packedAttribute)
        {
            base.SetFromPackedAttribute(packedAttribute);
            values = new List<int>();
            foreach (var valueCsv in packedAttribute.valueCsvList)
            {
                var oneLineCsv = valueCsv.ReplaceCrlf();
                values = CSVParser.LoadFromString(oneLineCsv)[0].ConvertAll(int.Parse);
            }
        }
    }
}
