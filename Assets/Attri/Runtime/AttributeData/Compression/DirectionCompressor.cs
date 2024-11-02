using System;
using System.Linq;
using Unity.Mathematics;

namespace Attri.Runtime
{
	public class DirectionCompressor
	{
		private const int Resolution24BIT = 2046;
		private const double HalfPI = 1.5707963267948966192313216916398;
		
		public int Precision;
		// [frame][element]
		public readonly float3[][] OriginalVectors;// IDataProviderでもいいかも
		private float3[][] Compressed;
		public DirectionCompressor(float[][][] originalData, int precision)
		{
			Precision = precision;
			OriginalVectors = originalData.Select(e => e.Select(v => new float3(v[0], v[1], v[2])).Select(math.normalize).ToArray()).ToArray();
			// [エレメント][成分]を[成分][エレメント]に変換
			// OriginalComponents = OriginalElements.ElementsToComponents();
		}
		
		public float3[][] Compress()
		{
			// 成分ごとに圧縮
			Compressed = new float3[OriginalVectors.Length][];
			for(var frame = 0; frame < Compressed.Length; frame++)
			{
				var vectors = OriginalVectors[frame];
				Compressed[frame] = new float3[vectors.Length];
				for (var element = 0; element < vectors.Length; element++)
				{
					var vec = vectors[element];
					var encoded = EncodeUnitVectorTo24bit(vec);
					var decoded = DecodeUnitVectorFrom24bit(encoded);
					Compressed[frame][element] = decoded;
				}
			}
			
			return Compressed;
		}
		// TODO:任意のbit数で圧縮出来るようにする
		private uint EncodeUnitVectorTo24bit(float3 vec)
		{
			uint compressedValue = 0;
			const double delta_phi = HalfPI / Resolution24BIT;
			// 先頭3bitに符号を格納
			if (vec.x < 0)
			{
				compressedValue |= 1 << 23;
				vec.x *= -1;
			}
			if (vec.y < 0)
			{
				compressedValue |= 1 << 22;
				vec.y *= -1;
			}
			if (vec.z < 0)
			{
				compressedValue |= 1 << 21;
				vec.z *= -1;
			}
			// ベクトルの向きを立体角に変換
			double2 thetaPhi = VectorToSolidAngle(vec);
			// 立体角を解像度で量子化
			uint i = (uint)Math.Round(thetaPhi.y / delta_phi);
			uint j = (uint)Math.Round(thetaPhi.x * i * 2 / math.PI);

			uint n = (i + 1) * i / 2 + j;
			compressedValue |= n;
			return compressedValue;
		}
		static double2 VectorToSolidAngle(double3 cartesian)
		{
			//work for vector -z
			double theta = math.atan2(cartesian.z, cartesian.x); //-pi~pi 
			//float theta = (float)Math.Atan(cartesian.z /cartesian.x);
			
			double phi = math.acos(cartesian.y); //0~pi
			return new double2(theta, phi);
		}

		private float3 DecodeUnitVectorFrom24bit(uint encode)
		{
			uint n = encode & 0x1FFFFF;
			uint i = (uint)((math.sqrt(1 + 8 * n) - 1) / 2);
			uint j = n - (i + 1) * i / 2;

			double delta_phi = HalfPI / Resolution24BIT;
			double phi = i * delta_phi;
			double theta = i > 0 ? j * HalfPI / i : 0;

			double sinePhi = math.sin(phi);
			double3 normal = new double3(math.cos(theta) * sinePhi, math.cos(phi), math.sin(theta) * sinePhi);

			if ((encode & 0x800000) != 0) normal.x *= -1;
			if ((encode & 0x400000) != 0) normal.y *= -1;
			if ((encode & 0x200000) != 0) normal.z *= -1;

			// if(DEBUG_CLUSTERING_NORMAL_COLOR_OUTPUT)
			// 	return normalize(float3(i%2==0, j%2==0, n%2==0));
			return (float3)math.normalize(normal);
		}
	}
}