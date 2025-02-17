using Internal.BulletSharp.Math;
using System;
using static Internal.BulletSharp.UnsafeNativeMethods;

namespace Internal.BulletSharp
{
	public class AxisSweep3 : BroadphaseInterface
	{
		private OverlappingPairCallback _overlappingPairUserCallback;

		public AxisSweep3(BVector3 worldAabbMin, BVector3 worldAabbMax, ushort maxHandles = 16384,
			OverlappingPairCache pairCache = null, bool disableRaycastAccelerator = false)
			: base(btAxisSweep3_new(ref worldAabbMin, ref worldAabbMax, maxHandles,
				(pairCache != null) ? pairCache.Native : IntPtr.Zero, disableRaycastAccelerator))
		{
			_overlappingPairCache = pairCache ?? new HashedOverlappingPairCache(
				btBroadphaseInterface_getOverlappingPairCache(Native), true);
		}

		public ushort AddHandle(BVector3 aabbMin, BVector3 aabbMax, IntPtr pOwner,
			int collisionFilterGroup, int collisionFilterMask, Dispatcher dispatcher,
			IntPtr multiSapProxy)
		{
			return btAxisSweep3_addHandle(Native, ref aabbMin, ref aabbMax, pOwner,
				collisionFilterGroup, collisionFilterMask, dispatcher.Native);
		}

		public ushort AddHandleRef(ref BVector3 aabbMin, ref BVector3 aabbMax, IntPtr pOwner,
			int collisionFilterGroup, int collisionFilterMask,
			Dispatcher dispatcher, IntPtr multiSapProxy)
		{
			return btAxisSweep3_addHandle(Native, ref aabbMin, ref aabbMax, pOwner,
				collisionFilterGroup, collisionFilterMask, dispatcher.Native);
		}

		public override BroadphaseProxy CreateProxy(ref BVector3 aabbMin,
			ref BVector3 aabbMax, int shapeType, IntPtr userPtr, int collisionFilterGroup,
			int collisionFilterMask, Dispatcher dispatcher)
		{
			//throw new NotImplementedException();
			return new BroadphaseProxy(btBroadphaseInterface_createProxy(Native, ref aabbMin, ref aabbMax, shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher.Native));
		}
        /*
		public Handle GetHandle(ushort index)
		{
			return new Handle(btAxisSweep3_getHandle(Native, index), true);
		}

		public void Quantize(ushort o, Vector3 point, int isMax)
		{
			btAxisSweep3_quantize(Native, out o, ref point, isMax);
		}
		*/
        public void RemoveHandle(ushort handle, Dispatcher dispatcher)
		{
			btAxisSweep3_removeHandle(Native, handle, dispatcher.Native);
		}

		public bool TestAabbOverlap(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return btAxisSweep3_testAabbOverlap(Native, proxy0.Native, proxy1.Native);
		}

		public void UnQuantize(BroadphaseProxy proxy, out BVector3 aabbMin, out BVector3 aabbMax)
		{
			btAxisSweep3_unQuantize(Native, proxy.Native, out aabbMin, out aabbMax);
		}

		public void UpdateHandle(ushort handle, BVector3 aabbMin, BVector3 aabbMax,
			Dispatcher dispatcher)
		{
			btAxisSweep3_updateHandle(Native, handle, ref aabbMin, ref aabbMax,
				dispatcher.Native);
		}

		public ushort NumHandles => btAxisSweep3_getNumHandles(Native);

		public OverlappingPairCallback OverlappingPairUserCallback
		{
			get => _overlappingPairUserCallback;
			set
			{
				btAxisSweep3_setOverlappingPairUserCallback(Native, (value != null) ? value.Native : IntPtr.Zero);
				_overlappingPairUserCallback = value;
			}
		}
	}

	public class AxisSweep3_32Bit : BroadphaseInterface
	{
		private OverlappingPairCallback _overlappingPairUserCallback;

		public AxisSweep3_32Bit(BVector3 worldAabbMin, BVector3 worldAabbMax, uint maxHandles = 1500000,
			OverlappingPairCache pairCache = null, bool disableRaycastAccelerator = false)
			: base(bt32BitAxisSweep3_new(ref worldAabbMin, ref worldAabbMax, maxHandles,
				(pairCache != null) ? pairCache.Native : IntPtr.Zero, disableRaycastAccelerator))
		{
			_overlappingPairCache = (pairCache != null) ? pairCache : new HashedOverlappingPairCache(
				btBroadphaseInterface_getOverlappingPairCache(Native), true);
		}

		public uint AddHandle(BVector3 aabbMin, BVector3 aabbMax, IntPtr pOwner, int collisionFilterGroup,
			int collisionFilterMask, Dispatcher dispatcher, IntPtr multiSapProxy)
		{
			return bt32BitAxisSweep3_addHandle(Native, ref aabbMin, ref aabbMax,
				pOwner, collisionFilterGroup, collisionFilterMask, dispatcher.Native);
		}

		public uint AddHandleRef(ref BVector3 aabbMin, ref BVector3 aabbMax, IntPtr pOwner,
			int collisionFilterGroup, int collisionFilterMask,
			Dispatcher dispatcher, IntPtr multiSapProxy)
		{
			return bt32BitAxisSweep3_addHandle(Native, ref aabbMin, ref aabbMax,
				pOwner, collisionFilterGroup, collisionFilterMask, dispatcher.Native);
		}

		public override BroadphaseProxy CreateProxy(ref BVector3 aabbMin, ref BVector3 aabbMax, int shapeType, IntPtr userPtr, int collisionFilterGroup, int collisionFilterMask, Dispatcher dispatcher)
		{
			//throw new NotImplementedException();
			return new BroadphaseProxy(btBroadphaseInterface_createProxy(Native, ref aabbMin, ref aabbMax, shapeType, userPtr, collisionFilterGroup, collisionFilterMask, dispatcher.Native));
		}
        /*
		public Handle GetHandle(uint index)
		{
			return new Handle(bt32BitAxisSweep3_getHandle(Native, index), true);
		}

		public void Quantize(uint o, Vector3 point, int isMax)
		{
			bt32BitAxisSweep3_quantize(Native, out o, ref point, isMax);
		}
		*/
        public void RemoveHandle(uint handle, Dispatcher dispatcher)
		{
			bt32BitAxisSweep3_removeHandle(Native, handle, dispatcher.Native);
		}

		public bool TestAabbOverlap(BroadphaseProxy proxy0, BroadphaseProxy proxy1)
		{
			return bt32BitAxisSweep3_testAabbOverlap(Native, proxy0.Native, proxy1.Native);
		}

		public void UnQuantize(BroadphaseProxy proxy, out BVector3 aabbMin, out BVector3 aabbMax)
		{
			bt32BitAxisSweep3_unQuantize(Native, proxy.Native, out aabbMin, out aabbMax);
		}

		public void UpdateHandle(uint handle, BVector3 aabbMin, BVector3 aabbMax, Dispatcher dispatcher)
		{
			bt32BitAxisSweep3_updateHandle(Native, handle, ref aabbMin, ref aabbMax,
				dispatcher.Native);
		}

		public uint NumHandles => bt32BitAxisSweep3_getNumHandles(Native);

		public OverlappingPairCallback OverlappingPairUserCallback
		{
			get => _overlappingPairUserCallback;
			set
			{
				bt32BitAxisSweep3_setOverlappingPairUserCallback(Native, (value != null) ? value.Native : IntPtr.Zero);
				_overlappingPairUserCallback = value;
			}
		}
	}
}
