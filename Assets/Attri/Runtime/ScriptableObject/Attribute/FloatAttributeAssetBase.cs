using System.Collections.Generic;
using System.Text.RegularExpressions;
using yutokun;

namespace Attri.Runtime
{
    [AttributeType(AttributeType.Float, 1)]
    public class FloatAttributeAssetBase : AttributeAssetBase
    {
        List<List<float>> values = new();

        protected override void SetFromPackedAttribute(PackedAttribute packedAttribute)
        {
            base.SetFromPackedAttribute(packedAttribute);
            values = new List<List<float>>();
            foreach (var valueCsv in packedAttribute.valueCsvList)
            {
                var onlineCsv = Regex.Replace(valueCsv, @"\r\n|\r|\n", "\r\n");
                var valueInFrame = CSVParser.LoadFromString(onlineCsv)[0].ConvertAll(float.Parse);
                values.Add(valueInFrame);
            }
        }
    }
}
