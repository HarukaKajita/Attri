using System;
using Attri.Runtime.Extensions;
using UnityEngine;

namespace Attri.Runtime
{
	public class FixedPrecisionFloatCompressor : CompressorBase
	{
		// Encode Params
		[Serializable]
		private class EncodeParam
		{
			public float Center;
			public float ExpRangeCenterOffset;
			public int LossBitCount;

			public EncodeParam(float center, float expRangeCenterOffset, int lossBitCount)
			{
				Center = center;
				ExpRangeCenterOffset = expRangeCenterOffset;
				LossBitCount = lossBitCount;
			}
		}
		// Decode Params
		[Serializable]
		private class DecodeParam
		{
			public int ExponentBits;
			public int LossBitCount;
			public float Offset;
			
			public DecodeParam(int exponentBits, int lossBitCount, float offset)
			{
				ExponentBits = exponentBits;
				LossBitCount = lossBitCount;
				Offset = offset;
			}
		}
		
		[SerializeField] private EncodeParam _encodeParam;
		[SerializeField] private DecodeParam _decodeParam;
		public override ValueByte EncodeFloat(float value)
		{
			// Encode Preparation
			var center = _encodeParam.Center;
			var expRangeCenterOffset = _encodeParam.ExpRangeCenterOffset;
			var lossBitCount = _encodeParam.LossBitCount;
			
			// Encode ベイクする際は下位ビットを切り捨てる
			var M = (value-center+expRangeCenterOffset).AsInt() >> lossBitCount;
			return new ValueByte(BitConverter.GetBytes(M));
		}
		public override float DecodeFloat(ValueByte valueByte)
		{
			// TODO: byte配列の要素数が足らなくても機能するのか確認
			var mantissa = BitConverter.ToInt32(valueByte.Bytes, 0);
			return Decode(mantissa, _decodeParam);
		}
		private float Decode(int mantissa, DecodeParam decodeParam)
		{
			return Decode(mantissa, decodeParam.ExponentBits, decodeParam.LossBitCount, decodeParam.Offset);
		}
		private float Decode(int mantissa, int exponentBits, int lossBitCount, float offset)
		{
			mantissa <<= lossBitCount;
			var preFloat = BitConverter.ToSingle(BitConverter.GetBytes(exponentBits | mantissa), 0);
			return preFloat - offset;
		}
	}
}