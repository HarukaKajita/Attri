using System;

namespace Attri.Runtime
{
	public enum CompressionType
	{
		UnCompressed,
		Half,
		UnitVector3,
		FixedPrecisionFloat,
		BiasedPrecisionFloat,
		ShrinkLengthInt,
	}

	public static class CompressionParamNames
	{
		public static string[] GetParamNames(CompressionType type)
		{
			return type switch
			{
				CompressionType.Half => Array.Empty<string>(),
				CompressionType.UnitVector3 => new string[]{"precision"},
				CompressionType.FixedPrecisionFloat => new[] {"precision", "offset"},
				CompressionType.BiasedPrecisionFloat => new[] {"precision", "bias"},
				CompressionType.ShrinkLengthInt => new[] {"bitLength"},
				_ => Array.Empty<string>(),
			};
		}
		public static int PrecisionParam(this CompressedDataProvider provider)
		{
			return provider._intParams["precision"];
		}
	}
}