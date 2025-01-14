using System;
using Internal.BulletSharp.Math;
using static Internal.BulletSharp.UnsafeNativeMethods;

namespace Internal.BulletSharp
{
	public abstract class DiscreteCollisionDetectorInterface : IDisposable
	{
		public class ClosestPointInput : IDisposable
		{
			internal IntPtr Native;

			public ClosestPointInput()
			{
				Native = btDiscreteCollisionDetectorInterface_ClosestPointInput_new();
			}

			public double MaximumDistanceSquared
			{
				get => btDiscreteCollisionDetectorInterface_ClosestPointInput_getMaximumDistanceSquared(Native);
				set => btDiscreteCollisionDetectorInterface_ClosestPointInput_setMaximumDistanceSquared(Native, value);
			}

			public BMatrix TransformA
			{
				get
				{
					BMatrix value;
					btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformA(Native, out value);
					return value;
				}
				set => btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformA(Native, ref value);
			}

			public BMatrix TransformB
			{
				get
				{
					BMatrix value;
					btDiscreteCollisionDetectorInterface_ClosestPointInput_getTransformB(Native, out value);
					return value;
				}
				set => btDiscreteCollisionDetectorInterface_ClosestPointInput_setTransformB(Native, ref value);
			}

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (Native != IntPtr.Zero)
				{
					btDiscreteCollisionDetectorInterface_ClosestPointInput_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~ClosestPointInput()
			{
				Dispose(false);
			}
		}

		public abstract class Result : IDisposable
		{
			internal IntPtr Native;

			internal Result(IntPtr native)
			{
				Native = native;
			}

			public void AddContactPoint(BVector3 normalOnBInWorld, BVector3 pointInWorld,
				double depth)
			{
				btDiscreteCollisionDetectorInterface_Result_addContactPoint(Native,
					ref normalOnBInWorld, ref pointInWorld, depth);
			}

			public void SetShapeIdentifiersA(int partId0, int index0)
			{
				btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersA(
					Native, partId0, index0);
			}

			public void SetShapeIdentifiersB(int partId1, int index1)
			{
				btDiscreteCollisionDetectorInterface_Result_setShapeIdentifiersB(
					Native, partId1, index1);
			}

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (Native != IntPtr.Zero)
				{
					btDiscreteCollisionDetectorInterface_Result_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~Result()
			{
				Dispose(false);
			}
		}

		internal IntPtr Native;

		internal DiscreteCollisionDetectorInterface(IntPtr native)
		{
			Native = native;
		}

		public void GetClosestPoints(ClosestPointInput input, Result output, IDebugDraw debugDraw,
			bool swapResults = false)
		{
			btDiscreteCollisionDetectorInterface_getClosestPoints(Native, input.Native,
				output.Native, DebugDraw.GetUnmanaged(debugDraw), swapResults);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btDiscreteCollisionDetectorInterface_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~DiscreteCollisionDetectorInterface()
		{
			Dispose(false);
		}
	}

	public abstract class StorageResult : DiscreteCollisionDetectorInterface.Result
	{
		internal StorageResult(IntPtr native)
			: base(native)
		{
		}
		/*
		public StorageResult()
			: base(btStorageResultWrapper_new())
		{
		}
		*/
		public BVector3 ClosestPointInB
		{
			get
			{
				BVector3 value;
				btStorageResult_getClosestPointInB(Native, out value);
				return value;
			}
			set => btStorageResult_setClosestPointInB(Native, ref value);
		}

		public double Distance
		{
			get => btStorageResult_getDistance(Native);
			set => btStorageResult_setDistance(Native, value);
		}

		public BVector3 NormalOnSurfaceB
		{
			get
			{
				BVector3 value;
				btStorageResult_getNormalOnSurfaceB(Native, out value);
				return value;
			}
			set => btStorageResult_setNormalOnSurfaceB(Native, ref value);
		}
	}
}
