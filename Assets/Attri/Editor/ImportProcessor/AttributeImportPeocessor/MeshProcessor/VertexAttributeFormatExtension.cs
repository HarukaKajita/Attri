using System;
using UnityEngine.Rendering;

namespace Attri.Editor
{
	public static class VertexAttributeFormatExtension
	{
		public static int AttributeFormatByteSize(this VertexAttributeFormat format)
		{
			return format switch
			{
				VertexAttributeFormat.Float32 => 4,
				VertexAttributeFormat.Float16 => 2,
				VertexAttributeFormat.UNorm8 => 1,
				VertexAttributeFormat.SNorm8 => 1,
				VertexAttributeFormat.UNorm16 => 2,
				VertexAttributeFormat.SNorm16 => 2,
				VertexAttributeFormat.UInt8 => 1,
				VertexAttributeFormat.SInt8 => 1,
				VertexAttributeFormat.UInt16 => 2,
				VertexAttributeFormat.SInt16 => 2,
				VertexAttributeFormat.UInt32 => 4,
				VertexAttributeFormat.SInt32 => 4,
				_ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
			};
		}
	}
}