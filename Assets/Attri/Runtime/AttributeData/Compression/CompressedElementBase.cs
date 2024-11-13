using System;
using System.Linq;
using UnityEngine;

namespace Attri.Runtime
{
	public class CompressedElementBase : ElementBase
	{
		// [component]
		[SerializeField, DisableOnInspector] 
		protected readonly ValueByte[] Components = Array.Empty<ValueByte>();
		protected readonly AttributeDataType AttributeDataType;
		protected readonly CompressorBase Compressor;
		
		public CompressedElementBase(ValueByte[] components, AttributeDataType attributeDataType, CompressorBase compressor)
		{
			Components = components;
			AttributeDataType = attributeDataType;
			Compressor = compressor;
		}
		
		public override AttributeDataType GetAttributeType() => AttributeDataType;
		
		public override float[] ComponentsAsFloat()
		{
			if (AttributeDataType != AttributeDataType.Float) throw new Exception("AttributeDataType is not Float");
			return Components.Select(component => Compressor.DecodeFloat(component)).ToArray();
		}
		public override int[] ComponentsAsInt()
		{
			if (AttributeDataType != AttributeDataType.Int) throw new Exception("AttributeDataType is not Int");
			return Components.Select(component => Compressor.DecodeInt(component)).ToArray();
		}
		
		public override string[] ComponentsAsString()
		{
			if (AttributeDataType != AttributeDataType.String) throw new Exception("AttributeDataType is not String");
			return Components.Select(component => Compressor.DecodeString(component)).ToArray();
		}
	}
}