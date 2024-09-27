using System.Runtime.InteropServices;
using System;
using UnityEngine;
namespace Attri.Runtime
{
	internal static class VertexDataUtility
	{
		public static void SetVertexData(Mesh mesh, byte[] byteArray, int vertexCount)
		{
			// 4の倍数にする
			if (byteArray.Length % 4 != 0)
				throw new ArgumentException("byteArray length must be multiple of 4");
			var byteSize = (int)Math.Ceiling(byteArray.Length / 4f) * 4 / vertexCount;
			switch (byteSize)
			{
			case 4:
			{
				var data = Byte4.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 8:
			{
				var data = Byte8.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 12:
			{
				var data = Byte12.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 16:
			{
				var data = Byte16.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 20:
			{
				var data = Byte20.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 24:
			{
				var data = Byte24.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 28:
			{
				var data = Byte28.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 32:
			{
				var data = Byte32.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 36:
			{
				var data = Byte36.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 40:
			{
				var data = Byte40.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 44:
			{
				var data = Byte44.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 48:
			{
				var data = Byte48.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 52:
			{
				var data = Byte52.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 56:
			{
				var data = Byte56.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 60:
			{
				var data = Byte60.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 64:
			{
				var data = Byte64.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 68:
			{
				var data = Byte68.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 72:
			{
				var data = Byte72.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 76:
			{
				var data = Byte76.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 80:
			{
				var data = Byte80.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 84:
			{
				var data = Byte84.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 88:
			{
				var data = Byte88.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 92:
			{
				var data = Byte92.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 96:
			{
				var data = Byte96.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 100:
			{
				var data = Byte100.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 104:
			{
				var data = Byte104.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 108:
			{
				var data = Byte108.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 112:
			{
				var data = Byte112.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 116:
			{
				var data = Byte116.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 120:
			{
				var data = Byte120.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 124:
			{
				var data = Byte124.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 128:
			{
				var data = Byte128.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 132:
			{
				var data = Byte132.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 136:
			{
				var data = Byte136.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 140:
			{
				var data = Byte140.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 144:
			{
				var data = Byte144.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 148:
			{
				var data = Byte148.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 152:
			{
				var data = Byte152.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 156:
			{
				var data = Byte156.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 160:
			{
				var data = Byte160.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 164:
			{
				var data = Byte164.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 168:
			{
				var data = Byte168.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 172:
			{
				var data = Byte172.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 176:
			{
				var data = Byte176.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 180:
			{
				var data = Byte180.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 184:
			{
				var data = Byte184.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 188:
			{
				var data = Byte188.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			case 192:
			{
				var data = Byte192.Convert(byteArray, vertexCount);
				mesh.SetVertexBufferData(data, 0, 0, vertexCount);
				break;
			}
			default:
			break;

			}
		}
	}
 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte4
	{
		public int byte4;
 
		public static Byte4[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte4[count];
			const int size = 4;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte4)Marshal.PtrToStructure(ptPoint, typeof(Byte4));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte8
	{
		public int byte4;
		public int byte8;
 
		public static Byte8[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte8[count];
			const int size = 8;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte8)Marshal.PtrToStructure(ptPoint, typeof(Byte8));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte12
	{
		public int byte4;
		public int byte8;
		public int byte12;
 
		public static Byte12[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte12[count];
			const int size = 12;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte12)Marshal.PtrToStructure(ptPoint, typeof(Byte12));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte16
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
 
		public static Byte16[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte16[count];
			const int size = 16;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte16)Marshal.PtrToStructure(ptPoint, typeof(Byte16));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte20
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
 
		public static Byte20[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte20[count];
			const int size = 20;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte20)Marshal.PtrToStructure(ptPoint, typeof(Byte20));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte24
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
 
		public static Byte24[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte24[count];
			const int size = 24;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte24)Marshal.PtrToStructure(ptPoint, typeof(Byte24));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte28
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
 
		public static Byte28[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte28[count];
			const int size = 28;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte28)Marshal.PtrToStructure(ptPoint, typeof(Byte28));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte32
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
 
		public static Byte32[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte32[count];
			const int size = 32;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte32)Marshal.PtrToStructure(ptPoint, typeof(Byte32));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte36
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
 
		public static Byte36[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte36[count];
			const int size = 36;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte36)Marshal.PtrToStructure(ptPoint, typeof(Byte36));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte40
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
 
		public static Byte40[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte40[count];
			const int size = 40;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte40)Marshal.PtrToStructure(ptPoint, typeof(Byte40));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte44
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
 
		public static Byte44[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte44[count];
			const int size = 44;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte44)Marshal.PtrToStructure(ptPoint, typeof(Byte44));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte48
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
 
		public static Byte48[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte48[count];
			const int size = 48;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte48)Marshal.PtrToStructure(ptPoint, typeof(Byte48));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte52
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
 
		public static Byte52[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte52[count];
			const int size = 52;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte52)Marshal.PtrToStructure(ptPoint, typeof(Byte52));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte56
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
 
		public static Byte56[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte56[count];
			const int size = 56;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte56)Marshal.PtrToStructure(ptPoint, typeof(Byte56));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte60
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
 
		public static Byte60[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte60[count];
			const int size = 60;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte60)Marshal.PtrToStructure(ptPoint, typeof(Byte60));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte64
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
 
		public static Byte64[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte64[count];
			const int size = 64;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte64)Marshal.PtrToStructure(ptPoint, typeof(Byte64));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte68
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
 
		public static Byte68[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte68[count];
			const int size = 68;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte68)Marshal.PtrToStructure(ptPoint, typeof(Byte68));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte72
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
 
		public static Byte72[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte72[count];
			const int size = 72;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte72)Marshal.PtrToStructure(ptPoint, typeof(Byte72));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte76
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
 
		public static Byte76[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte76[count];
			const int size = 76;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte76)Marshal.PtrToStructure(ptPoint, typeof(Byte76));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte80
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
 
		public static Byte80[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte80[count];
			const int size = 80;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte80)Marshal.PtrToStructure(ptPoint, typeof(Byte80));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte84
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
 
		public static Byte84[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte84[count];
			const int size = 84;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte84)Marshal.PtrToStructure(ptPoint, typeof(Byte84));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte88
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
 
		public static Byte88[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte88[count];
			const int size = 88;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte88)Marshal.PtrToStructure(ptPoint, typeof(Byte88));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte92
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
 
		public static Byte92[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte92[count];
			const int size = 92;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte92)Marshal.PtrToStructure(ptPoint, typeof(Byte92));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte96
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
 
		public static Byte96[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte96[count];
			const int size = 96;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte96)Marshal.PtrToStructure(ptPoint, typeof(Byte96));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte100
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
 
		public static Byte100[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte100[count];
			const int size = 100;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte100)Marshal.PtrToStructure(ptPoint, typeof(Byte100));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte104
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
 
		public static Byte104[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte104[count];
			const int size = 104;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte104)Marshal.PtrToStructure(ptPoint, typeof(Byte104));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte108
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
 
		public static Byte108[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte108[count];
			const int size = 108;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte108)Marshal.PtrToStructure(ptPoint, typeof(Byte108));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte112
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
 
		public static Byte112[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte112[count];
			const int size = 112;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte112)Marshal.PtrToStructure(ptPoint, typeof(Byte112));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte116
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
 
		public static Byte116[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte116[count];
			const int size = 116;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte116)Marshal.PtrToStructure(ptPoint, typeof(Byte116));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte120
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
 
		public static Byte120[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte120[count];
			const int size = 120;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte120)Marshal.PtrToStructure(ptPoint, typeof(Byte120));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte124
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
 
		public static Byte124[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte124[count];
			const int size = 124;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte124)Marshal.PtrToStructure(ptPoint, typeof(Byte124));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte128
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
 
		public static Byte128[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte128[count];
			const int size = 128;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte128)Marshal.PtrToStructure(ptPoint, typeof(Byte128));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte132
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
 
		public static Byte132[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte132[count];
			const int size = 132;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte132)Marshal.PtrToStructure(ptPoint, typeof(Byte132));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte136
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
 
		public static Byte136[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte136[count];
			const int size = 136;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte136)Marshal.PtrToStructure(ptPoint, typeof(Byte136));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte140
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
 
		public static Byte140[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte140[count];
			const int size = 140;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte140)Marshal.PtrToStructure(ptPoint, typeof(Byte140));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte144
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
 
		public static Byte144[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte144[count];
			const int size = 144;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte144)Marshal.PtrToStructure(ptPoint, typeof(Byte144));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte148
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
 
		public static Byte148[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte148[count];
			const int size = 148;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte148)Marshal.PtrToStructure(ptPoint, typeof(Byte148));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte152
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
		public int byte152;
 
		public static Byte152[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte152[count];
			const int size = 152;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte152)Marshal.PtrToStructure(ptPoint, typeof(Byte152));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					37 => byte152,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					case 37: byte152 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte156
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
		public int byte152;
		public int byte156;
 
		public static Byte156[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte156[count];
			const int size = 156;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte156)Marshal.PtrToStructure(ptPoint, typeof(Byte156));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					37 => byte152,
					38 => byte156,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					case 37: byte152 = value; break;
					case 38: byte156 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte160
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
		public int byte152;
		public int byte156;
		public int byte160;
 
		public static Byte160[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte160[count];
			const int size = 160;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte160)Marshal.PtrToStructure(ptPoint, typeof(Byte160));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					37 => byte152,
					38 => byte156,
					39 => byte160,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					case 37: byte152 = value; break;
					case 38: byte156 = value; break;
					case 39: byte160 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte164
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
		public int byte152;
		public int byte156;
		public int byte160;
		public int byte164;
 
		public static Byte164[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte164[count];
			const int size = 164;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte164)Marshal.PtrToStructure(ptPoint, typeof(Byte164));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					37 => byte152,
					38 => byte156,
					39 => byte160,
					40 => byte164,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					case 37: byte152 = value; break;
					case 38: byte156 = value; break;
					case 39: byte160 = value; break;
					case 40: byte164 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte168
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
		public int byte152;
		public int byte156;
		public int byte160;
		public int byte164;
		public int byte168;
 
		public static Byte168[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte168[count];
			const int size = 168;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte168)Marshal.PtrToStructure(ptPoint, typeof(Byte168));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					37 => byte152,
					38 => byte156,
					39 => byte160,
					40 => byte164,
					41 => byte168,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					case 37: byte152 = value; break;
					case 38: byte156 = value; break;
					case 39: byte160 = value; break;
					case 40: byte164 = value; break;
					case 41: byte168 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte172
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
		public int byte152;
		public int byte156;
		public int byte160;
		public int byte164;
		public int byte168;
		public int byte172;
 
		public static Byte172[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte172[count];
			const int size = 172;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte172)Marshal.PtrToStructure(ptPoint, typeof(Byte172));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					37 => byte152,
					38 => byte156,
					39 => byte160,
					40 => byte164,
					41 => byte168,
					42 => byte172,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					case 37: byte152 = value; break;
					case 38: byte156 = value; break;
					case 39: byte160 = value; break;
					case 40: byte164 = value; break;
					case 41: byte168 = value; break;
					case 42: byte172 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte176
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
		public int byte152;
		public int byte156;
		public int byte160;
		public int byte164;
		public int byte168;
		public int byte172;
		public int byte176;
 
		public static Byte176[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte176[count];
			const int size = 176;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte176)Marshal.PtrToStructure(ptPoint, typeof(Byte176));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					37 => byte152,
					38 => byte156,
					39 => byte160,
					40 => byte164,
					41 => byte168,
					42 => byte172,
					43 => byte176,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					case 37: byte152 = value; break;
					case 38: byte156 = value; break;
					case 39: byte160 = value; break;
					case 40: byte164 = value; break;
					case 41: byte168 = value; break;
					case 42: byte172 = value; break;
					case 43: byte176 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte180
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
		public int byte152;
		public int byte156;
		public int byte160;
		public int byte164;
		public int byte168;
		public int byte172;
		public int byte176;
		public int byte180;
 
		public static Byte180[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte180[count];
			const int size = 180;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte180)Marshal.PtrToStructure(ptPoint, typeof(Byte180));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					37 => byte152,
					38 => byte156,
					39 => byte160,
					40 => byte164,
					41 => byte168,
					42 => byte172,
					43 => byte176,
					44 => byte180,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					case 37: byte152 = value; break;
					case 38: byte156 = value; break;
					case 39: byte160 = value; break;
					case 40: byte164 = value; break;
					case 41: byte168 = value; break;
					case 42: byte172 = value; break;
					case 43: byte176 = value; break;
					case 44: byte180 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte184
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
		public int byte152;
		public int byte156;
		public int byte160;
		public int byte164;
		public int byte168;
		public int byte172;
		public int byte176;
		public int byte180;
		public int byte184;
 
		public static Byte184[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte184[count];
			const int size = 184;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte184)Marshal.PtrToStructure(ptPoint, typeof(Byte184));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					37 => byte152,
					38 => byte156,
					39 => byte160,
					40 => byte164,
					41 => byte168,
					42 => byte172,
					43 => byte176,
					44 => byte180,
					45 => byte184,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					case 37: byte152 = value; break;
					case 38: byte156 = value; break;
					case 39: byte160 = value; break;
					case 40: byte164 = value; break;
					case 41: byte168 = value; break;
					case 42: byte172 = value; break;
					case 43: byte176 = value; break;
					case 44: byte180 = value; break;
					case 45: byte184 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte188
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
		public int byte152;
		public int byte156;
		public int byte160;
		public int byte164;
		public int byte168;
		public int byte172;
		public int byte176;
		public int byte180;
		public int byte184;
		public int byte188;
 
		public static Byte188[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte188[count];
			const int size = 188;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte188)Marshal.PtrToStructure(ptPoint, typeof(Byte188));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					37 => byte152,
					38 => byte156,
					39 => byte160,
					40 => byte164,
					41 => byte168,
					42 => byte172,
					43 => byte176,
					44 => byte180,
					45 => byte184,
					46 => byte188,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					case 37: byte152 = value; break;
					case 38: byte156 = value; break;
					case 39: byte160 = value; break;
					case 40: byte164 = value; break;
					case 41: byte168 = value; break;
					case 42: byte172 = value; break;
					case 43: byte176 = value; break;
					case 44: byte180 = value; break;
					case 45: byte184 = value; break;
					case 46: byte188 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
	[StructLayout(LayoutKind.Sequential)]
	public struct Byte192
	{
		public int byte4;
		public int byte8;
		public int byte12;
		public int byte16;
		public int byte20;
		public int byte24;
		public int byte28;
		public int byte32;
		public int byte36;
		public int byte40;
		public int byte44;
		public int byte48;
		public int byte52;
		public int byte56;
		public int byte60;
		public int byte64;
		public int byte68;
		public int byte72;
		public int byte76;
		public int byte80;
		public int byte84;
		public int byte88;
		public int byte92;
		public int byte96;
		public int byte100;
		public int byte104;
		public int byte108;
		public int byte112;
		public int byte116;
		public int byte120;
		public int byte124;
		public int byte128;
		public int byte132;
		public int byte136;
		public int byte140;
		public int byte144;
		public int byte148;
		public int byte152;
		public int byte156;
		public int byte160;
		public int byte164;
		public int byte168;
		public int byte172;
		public int byte176;
		public int byte180;
		public int byte184;
		public int byte188;
		public int byte192;
 
		public static Byte192[] Convert(byte[] byteArray, int count)
		{
			var dataStruct = new Byte192[count];
			const int size = 192;
			for (var i = 0; i < count; i++)
			{
				IntPtr ptPoint = Marshal.AllocHGlobal(size);
				Marshal.Copy(byteArray, i * size, ptPoint, size);
				dataStruct[i] = (Byte192)Marshal.PtrToStructure(ptPoint, typeof(Byte192));
				Marshal.FreeHGlobal(ptPoint);
			}
			return dataStruct;
		}
 
		public int this[int index]
		{
		    get
		    {
		        return index switch
		        {
					0 => byte4,
					1 => byte8,
					2 => byte12,
					3 => byte16,
					4 => byte20,
					5 => byte24,
					6 => byte28,
					7 => byte32,
					8 => byte36,
					9 => byte40,
					10 => byte44,
					11 => byte48,
					12 => byte52,
					13 => byte56,
					14 => byte60,
					15 => byte64,
					16 => byte68,
					17 => byte72,
					18 => byte76,
					19 => byte80,
					20 => byte84,
					21 => byte88,
					22 => byte92,
					23 => byte96,
					24 => byte100,
					25 => byte104,
					26 => byte108,
					27 => byte112,
					28 => byte116,
					29 => byte120,
					30 => byte124,
					31 => byte128,
					32 => byte132,
					33 => byte136,
					34 => byte140,
					35 => byte144,
					36 => byte148,
					37 => byte152,
					38 => byte156,
					39 => byte160,
					40 => byte164,
					41 => byte168,
					42 => byte172,
					43 => byte176,
					44 => byte180,
					45 => byte184,
					46 => byte188,
					47 => byte192,
					_ => throw new IndexOutOfRangeException()

		        };
		    }
		    set
		    {
		        switch (index)
		        {
					case 0: byte4 = value; break;
					case 1: byte8 = value; break;
					case 2: byte12 = value; break;
					case 3: byte16 = value; break;
					case 4: byte20 = value; break;
					case 5: byte24 = value; break;
					case 6: byte28 = value; break;
					case 7: byte32 = value; break;
					case 8: byte36 = value; break;
					case 9: byte40 = value; break;
					case 10: byte44 = value; break;
					case 11: byte48 = value; break;
					case 12: byte52 = value; break;
					case 13: byte56 = value; break;
					case 14: byte60 = value; break;
					case 15: byte64 = value; break;
					case 16: byte68 = value; break;
					case 17: byte72 = value; break;
					case 18: byte76 = value; break;
					case 19: byte80 = value; break;
					case 20: byte84 = value; break;
					case 21: byte88 = value; break;
					case 22: byte92 = value; break;
					case 23: byte96 = value; break;
					case 24: byte100 = value; break;
					case 25: byte104 = value; break;
					case 26: byte108 = value; break;
					case 27: byte112 = value; break;
					case 28: byte116 = value; break;
					case 29: byte120 = value; break;
					case 30: byte124 = value; break;
					case 31: byte128 = value; break;
					case 32: byte132 = value; break;
					case 33: byte136 = value; break;
					case 34: byte140 = value; break;
					case 35: byte144 = value; break;
					case 36: byte148 = value; break;
					case 37: byte152 = value; break;
					case 38: byte156 = value; break;
					case 39: byte160 = value; break;
					case 40: byte164 = value; break;
					case 41: byte168 = value; break;
					case 42: byte172 = value; break;
					case 43: byte176 = value; break;
					case 44: byte180 = value; break;
					case 45: byte184 = value; break;
					case 46: byte188 = value; break;
					case 47: byte192 = value; break;
					default: throw new IndexOutOfRangeException();

		        }
		    }
		} 
	} 
 
}
