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
		public static uint3 AsUint(this float3 value)
		{
			return new uint3(value.x.AsUint(), value.y.AsUint(), value.z.AsUint());
		}
		public static IEnumerable<uint3> AsUint(this IEnumerable<float3> values)
		{
			return values.Select(value => value.AsUint());
		}
		
		public static uint BitDepth(this uint value)
		{
			uint count = 0;
			while (value > 0)
			{
				value >>= 1;
				count++;
			}
			return count;
		}
		#endregion
	}
}