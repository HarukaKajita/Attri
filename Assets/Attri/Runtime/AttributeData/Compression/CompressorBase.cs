using System;
using UnityEngine;

namespace Attri.Runtime
{
	public abstract class CompressorBase
	{
		[SerializeField, DisableOnInspector] private CompressionType _compressionType;
		public CompressionType CompressionType()
		{
			return _compressionType;
		}
		
		// Arbitrary Parameter for decode in inherited class
		// ex) int precision
		
		public virtual ValueByte EncodeInt(int value)  => throw new NotImplementedException();
		public virtual ValueByte EncodeFloat(float value)  => throw new NotImplementedException();
		public virtual ValueByte EncodeString(string value)  => throw new NotImplementedException();
		
		public virtual int DecodeInt(ValueByte valueByte) => throw new NotImplementedException();
		public virtual float DecodeFloat(ValueByte valueByte) => throw new NotImplementedException();
		public virtual string DecodeString(ValueByte valueByte) => throw new NotImplementedException();
	}
}