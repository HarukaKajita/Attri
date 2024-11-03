using System;

namespace Attri.Runtime
{
	public class CompressedElementBase : ElementBase
	{
		// [component]
		public ValueByte[] ValueByte = Array.Empty<ValueByte>();
		
		public CompressedElementBase(ValueByte[] valueByte)
		{
			ValueByte = valueByte;
		}
	}
}