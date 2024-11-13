using System;

namespace Attri.Runtime
{
	public struct ValueByte
	{
		public byte[] Bytes;

		public ValueByte(byte[] bytes)
		{
			Bytes = bytes;
		}
		
		public static implicit operator int (ValueByte valueByte) => BitConverter.ToInt32(valueByte.Bytes, 0);
	}
}