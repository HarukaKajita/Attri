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

		public static VertexAttributeFormat[] GetValidFormats(this VertexAttribute attribute)
		{
			if(attribute is VertexAttribute.Position or VertexAttribute.Normal)
				return new[] {VertexAttributeFormat.Float32, VertexAttributeFormat.Float16};
			
			return new[]
			{
				VertexAttributeFormat.Float32,
				VertexAttributeFormat.Float16,
				VertexAttributeFormat.UNorm8,
				VertexAttributeFormat.SNorm8,
				VertexAttributeFormat.UNorm16,
				VertexAttributeFormat.SNorm16,
				VertexAttributeFormat.UInt8,
				VertexAttributeFormat.SInt8,
				VertexAttributeFormat.UInt16,
				VertexAttributeFormat.SInt16,
				VertexAttributeFormat.UInt32,
				VertexAttributeFormat.SInt32
			};
		}
		
		public static int[] GetValidDimensionArray(this VertexAttributeFormat format)
		{
			// 頂点アトリビュートは4の倍数byteでないといけないのでフォーマットに依って使用できる次元を制限する
			return format switch
			{
				VertexAttributeFormat.Float32 => new[] {1, 2, 3, 4},
				VertexAttributeFormat.Float16 => new[] {2, 4},
				VertexAttributeFormat.UNorm8 => new[] {4},
				VertexAttributeFormat.SNorm8 => new[] {4},
				VertexAttributeFormat.UNorm16 => new[] {2, 4},
				VertexAttributeFormat.SNorm16 => new[] {2, 4},
				VertexAttributeFormat.UInt8 => new[] {4},
				VertexAttributeFormat.SInt8 => new[] {4},
				VertexAttributeFormat.UInt16 => new[] {2, 4},
				VertexAttributeFormat.SInt16 => new[] {2, 4},
				VertexAttributeFormat.UInt32 => new[] {1, 2, 3, 4},
				VertexAttributeFormat.SInt32 => new[] {1, 2, 3, 4},
				_ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
			};
		}

		public static int GetDefaultDimension(this VertexAttribute attribute)
		{
			return attribute switch
			{
				VertexAttribute.Position => 3,
				VertexAttribute.Normal => 3,
				VertexAttribute.Tangent => 3,
				VertexAttribute.Color => 3,
				VertexAttribute.TexCoord0 => 2,
				VertexAttribute.TexCoord1 => 2,
				VertexAttribute.TexCoord2 => 2,
				VertexAttribute.TexCoord3 => 2,
				VertexAttribute.TexCoord4 => 2,
				VertexAttribute.TexCoord5 => 2,
				VertexAttribute.TexCoord6 => 2,
				VertexAttribute.TexCoord7 => 2,
				VertexAttribute.BlendWeight => 4,
				VertexAttribute.BlendIndices => 4,
				_ => throw new ArgumentOutOfRangeException(nameof(attribute), attribute, null)
			};
		}
	}
}