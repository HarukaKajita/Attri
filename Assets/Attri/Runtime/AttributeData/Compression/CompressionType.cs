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
				CompressionType.UnitVector3 => Array.Empty<string>(),
				CompressionType.FixedPrecisionFloat => new[] {"offset"},
				CompressionType.BiasedPrecisionFloat => new[] {"bias"},
				CompressionType.ShrinkLengthInt => new[] {"bitLength"},
				_ => Array.Empty<string>(),
			};
		}
	}
}