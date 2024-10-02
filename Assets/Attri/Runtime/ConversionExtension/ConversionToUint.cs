using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

namespace Attri.Runtime.Extensions
{
	public static class ConversionToUint
	{
		#region UintConversion

		public static uint AsUint(this float value)
		{
			return BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
		}
		public static IEnumerable<uint> AsUint(this IEnumerable<float> values)
		{
			return values.Select(AsUint);
		}
		
		public static IEnumerable<uint3> AsUint3(this IEnumerable<float3> values)
		{
			return values.Select(v => new uint3(
				v.x.AsUint(),
				v.y.AsUint(),
				v.z.AsUint()
			));
		}

		#endregion
	}
}