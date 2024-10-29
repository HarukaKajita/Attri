using UnityEngine;

namespace Attri.Runtime
{
	public class CompressedFloatProvider : CompressedDataProvider
	{
		public CompressedFloatProvider(CompressionType compressionType, ValueByte[][] compressedValueBytes) : base(AttributeDataType.Float, compressionType, compressedValueBytes)
		{
		}
		
		private int ExponentParam => _intParams["Exponent"];
		private float OffsetParam => _floatParams["Offset"];
		private int PrecisionParam => _intParams["Precision"];

		public override float[][] AsFloat()
		{
			// 圧縮されたデータをデコードして返す
			// if (_compressionType == CompressionType.Uncompressed)
			// {
			// 	var elemNum = _compressedValueBytes.Length;
			// 	var result = new float[elemNum][];
			// 	for (var i = 0; i < elemNum; i++)
			// 	{
			// 		var attribute = _compressedValueBytes[i];
			// 		var componentNum = attribute.Length;
			// 		result[i] = new float[componentNum];
			// 		for (var j = 0; j < componentNum; j++)
			// 		{
			// 			var floatValue = System.BitConverter.ToSingle(attribute[j].Value, 0);
			// 			result[i][j] = floatValue;
			// 		}
			// 	}
			// 	return result;
			// }
			// else
			if (_compressionType == CompressionType.Half)
			{
				var elemNum = _compressedValueBytes.Length;
				var result = new float[elemNum][];
				for (var i = 0; i < elemNum; i++)
				{
					var attribute = _compressedValueBytes[i];
					var componentNum = attribute.Length;
					result[i] = new float[componentNum];
					for (var j = 0; j < componentNum; j++)
					{
						var halfBytes = System.BitConverter.ToUInt16(attribute[j].Value, 0);
						result[i][j] = Mathf.HalfToFloat(halfBytes);
					}
				}
				return result;
			}
			else if (_compressionType == CompressionType.FixedPrecisionFloat)
			{
				var exp = ExponentParam;
				var offset = OffsetParam;
				var precision = OffsetParam;
				var elemNum = _compressedValueBytes.Length;
				var result = new float[elemNum][];
				for (var i = 0; i < elemNum; i++)
				{
					var attribute = _compressedValueBytes[i];
					var componentNum = attribute.Length;
					result[i] = new float[componentNum];
					for (var j = 0; j < componentNum; j++)
					{
						var mantissa = attribute[j].Value[0];
						System.BitConverter.ToInt32(attribute[j].Value);
						FloatCompressor.Decode(exp, mantissa, PrecisionParam, offset);
					}
				}
				return result;
			}
			else if (_compressionType == CompressionType.BiasedPrecisionFloat)
			{
				// あとから実装
				throw new System.NotImplementedException();
			}
			// 他の圧縮タイプに対する処理
			else
			{
				throw new System.NotImplementedException();
			}
		}
	}
}