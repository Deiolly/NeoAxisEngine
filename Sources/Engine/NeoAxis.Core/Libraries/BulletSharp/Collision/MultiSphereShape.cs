using System;
using System.IO;
using Internal.BulletSharp.Math;
using static Internal.BulletSharp.UnsafeNativeMethods;

namespace Internal.BulletSharp
{
	public class MultiSphereShape : ConvexInternalAabbCachingShape
	{
		public MultiSphereShape(BVector3[] positions, double[] radi)
			: base(btMultiSphereShape_new(positions, radi, (radi.Length < positions.Length) ? radi.Length : positions.Length))
		{
		}

		public MultiSphereShape(Vector3Array positions, double[] radi)
			: base(btMultiSphereShape_new2(positions._native, radi, (radi.Length < positions.Count) ? radi.Length : positions.Count))
		{
		}

		public BVector3 GetSpherePosition(int index)
		{
			BVector3 value;
			btMultiSphereShape_getSpherePosition(Native, index, out value);
			return value;
		}

		public double GetSphereRadius(int index)
		{
			return btMultiSphereShape_getSphereRadius(Native, index);
		}
		/*
		public unsafe override string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			base.Serialize(dataBuffer, serializer);

			int numElem = SphereCount;
			if (numElem != 0)
			{
				Chunk chunk = serializer.Allocate(16 + sizeof(int), numElem);
				Marshal.WriteInt64(dataBuffer, 0, serializer.GetUniquePointer(_native + 4));
				using (var stream = new UnmanagedMemoryStream((byte*)chunk.OldPtr.ToPointer(), chunk.Length, chunk.Length, FileAccess.Write))
				{
					using (var writer = new BulletWriter(stream))
					{
						for (int i = 0; i < SphereCount; i++)
						{
							writer.Write(GetSpherePosition(i));
							writer.Write(GetSphereRadius(i));
						}
					}
				}
				serializer.FinalizeChunk(chunk, "btPositionAndRadius", DnaID.Array, _native + 4);
			}
			else
			{
				Marshal.WriteInt64(dataBuffer, 0, 0);
			}
			Marshal.WriteInt32(dataBuffer, 4, numElem);

			return "btMultiSphereShapeData";
		}
		*/
		public int SphereCount => btMultiSphereShape_getSphereCount(Native);
	}
}
