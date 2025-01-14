using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using Internal.BulletSharp.Math;
using static Internal.BulletSharp.UnsafeNativeMethods;

namespace Internal.BulletSharp
{
	public class AllHitsRayResultCallback : RayResultCallback
	{
		public AllHitsRayResultCallback(BVector3 rayFromWorld, BVector3 rayToWorld)
		{
			RayFromWorld = rayFromWorld;
			RayToWorld = rayToWorld;

			CollisionObjects = new List<CollisionObject>();
			HitFractions = new List<double>();
			HitNormalWorld = new List<BVector3>();
			HitPointWorld = new List<BVector3>();
		}

		public override double AddSingleResult(LocalRayResult rayResult, bool normalInWorldSpace)
		{
			CollisionObject = rayResult.CollisionObject;
			CollisionObjects.Add(rayResult.CollisionObject);
			if (normalInWorldSpace)
			{
				HitNormalWorld.Add(rayResult.HitNormalLocal);
			}
			else
			{
				// need to transform normal into worldspace
				HitNormalWorld.Add(BVector3.TransformCoordinate(rayResult.HitNormalLocal, CollisionObject.WorldTransform.Basis));
			}
			HitPointWorld.Add(BVector3.Lerp(RayFromWorld, RayToWorld, rayResult.HitFraction));
			HitFractions.Add(rayResult.HitFraction);
			return ClosestHitFraction;
		}

		public List<CollisionObject> CollisionObjects { get; set; }
		public List<double> HitFractions { get; set; }
		public List<BVector3> HitNormalWorld { get; set; }
		public List<BVector3> HitPointWorld { get; set; }
		public BVector3 RayFromWorld { get; set; }
		public BVector3 RayToWorld { get; set; }
	}

	public class ClosestConvexResultCallback : ConvexResultCallback
	{
		public ClosestConvexResultCallback()
		{
		}

		public ClosestConvexResultCallback(ref BVector3 convexFromWorld, ref BVector3 convexToWorld)
		{
			ConvexFromWorld = convexFromWorld;
			ConvexToWorld = convexToWorld;
		}

		public override double AddSingleResult(LocalConvexResult convexResult, bool normalInWorldSpace)
		{
			//caller already does the filter on the m_closestHitFraction
			Debug.Assert(convexResult.HitFraction <= ClosestHitFraction);

			ClosestHitFraction = convexResult.HitFraction;
			HitCollisionObject = convexResult.HitCollisionObject;
			if (normalInWorldSpace)
			{
				HitNormalWorld = convexResult.HitNormalLocal;
			}
			else
			{
				// need to transform normal into worldspace
				HitNormalWorld = BVector3.TransformCoordinate(convexResult.HitNormalLocal, HitCollisionObject.WorldTransform.Basis);
			}
			HitPointWorld = convexResult.HitPointLocal;
			return convexResult.HitFraction;
		}

		public BVector3 ConvexFromWorld { get; set; }
		public BVector3 ConvexToWorld { get; set; }
		public CollisionObject HitCollisionObject { get; set; }
		public BVector3 HitNormalWorld { get; set; }
		public BVector3 HitPointWorld { get; set; }
	}

	public class ClosestRayResultCallback : RayResultCallback
	{
		public ClosestRayResultCallback(ref BVector3 rayFromWorld, ref BVector3 rayToWorld)
		{
			RayFromWorld = rayFromWorld;
			RayToWorld = rayToWorld;
		}

		public override double AddSingleResult(LocalRayResult rayResult, bool normalInWorldSpace)
		{
			//caller already does the filter on the m_closestHitFraction
			Debug.Assert(rayResult.HitFraction <= ClosestHitFraction);

			ClosestHitFraction = rayResult.HitFraction;
			CollisionObject = rayResult.CollisionObject;
			if (normalInWorldSpace)
			{
				HitNormalWorld = rayResult.HitNormalLocal;
			}
			else
			{
				// need to transform normal into worldspace
				HitNormalWorld = BVector3.TransformCoordinate(rayResult.HitNormalLocal, CollisionObject.WorldTransform.Basis);
			}
			HitPointWorld = BVector3.Lerp(RayFromWorld, RayToWorld, rayResult.HitFraction);
			return rayResult.HitFraction;
		}

		public BVector3 RayFromWorld { get; set; } //used to calculate hitPointWorld from hitFraction
		public BVector3 RayToWorld { get; set; }

		public BVector3 HitNormalWorld { get; set; }
		public BVector3 HitPointWorld { get; set; }
	}

	public abstract class ContactResultCallback : IDisposable
	{
		internal IntPtr Native;

		[UnmanagedFunctionPointer(Internal.BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate double AddSingleResultUnmanagedDelegate(IntPtr cp, IntPtr colObj0Wrap, int partId0, int index0, IntPtr colObj1Wrap, int partId1, int index1);
		[UnmanagedFunctionPointer(Internal.BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate bool NeedsCollisionUnmanagedDelegate(IntPtr proxy0);

		private AddSingleResultUnmanagedDelegate _addSingleResult;
		private NeedsCollisionUnmanagedDelegate _needsCollision;

		public ContactResultCallback()
		{
			_addSingleResult = AddSingleResultUnmanaged;
			_needsCollision = NeedsCollisionUnmanaged;
			Native = btCollisionWorld_ContactResultCallbackWrapper_new(
				Marshal.GetFunctionPointerForDelegate(_addSingleResult),
				Marshal.GetFunctionPointerForDelegate(_needsCollision));
		}

		private double AddSingleResultUnmanaged(IntPtr cp, IntPtr colObj0Wrap, int partId0, int index0, IntPtr colObj1Wrap, int partId1, int index1)
		{
			return AddSingleResult(new ManifoldPoint(cp, true),
				new CollisionObjectWrapper(colObj0Wrap), partId0, index0,
				new CollisionObjectWrapper(colObj1Wrap), partId1, index1);
		}

		public abstract double AddSingleResult(ManifoldPoint cp, CollisionObjectWrapper colObj0Wrap, int partId0, int index0, CollisionObjectWrapper colObj1Wrap, int partId1, int index1);

		private bool NeedsCollisionUnmanaged(IntPtr proxy0)
		{
			return NeedsCollision(BroadphaseProxy.GetManaged(proxy0));
		}

		public virtual bool NeedsCollision(BroadphaseProxy proxy0)
		{
			return btCollisionWorld_ContactResultCallbackWrapper_needsCollision(Native, proxy0.Native);
		}

		public double ClosestDistanceThreshold
		{
			get => btCollisionWorld_ContactResultCallback_getClosestDistanceThreshold(Native);
			set => btCollisionWorld_ContactResultCallback_setClosestDistanceThreshold(Native, value);
		}

		public int CollisionFilterGroup
		{
			get => btCollisionWorld_ContactResultCallback_getCollisionFilterGroup(Native);
			set => btCollisionWorld_ContactResultCallback_setCollisionFilterGroup(Native, value);
		}

		public int CollisionFilterMask
		{
			get => btCollisionWorld_ContactResultCallback_getCollisionFilterMask(Native);
			set => btCollisionWorld_ContactResultCallback_setCollisionFilterMask(Native, value);
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
				btCollisionWorld_ContactResultCallback_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ContactResultCallback()
		{
			Dispose(false);
		}
	}

	public abstract class ConvexResultCallback : IDisposable
	{
		internal IntPtr Native;

		[UnmanagedFunctionPointer(Internal.BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate double AddSingleResultUnmanagedDelegate(IntPtr convexResult, bool normalInWorldSpace);
		[UnmanagedFunctionPointer(Internal.BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate bool NeedsCollisionUnmanagedDelegate(IntPtr proxy0);

		private AddSingleResultUnmanagedDelegate _addSingleResult;
		private NeedsCollisionUnmanagedDelegate _needsCollision;

		protected ConvexResultCallback()
		{
			_addSingleResult = AddSingleResultUnmanaged;
			_needsCollision = NeedsCollisionUnmanaged;
			Native = btCollisionWorld_ConvexResultCallbackWrapper_new(
				Marshal.GetFunctionPointerForDelegate(_addSingleResult),
				Marshal.GetFunctionPointerForDelegate(_needsCollision));
		}

		private double AddSingleResultUnmanaged(IntPtr convexResult, bool normalInWorldSpace)
		{
			return AddSingleResult(new LocalConvexResult(convexResult), normalInWorldSpace);
		}

		public abstract double AddSingleResult(LocalConvexResult convexResult, bool normalInWorldSpace);

		private bool NeedsCollisionUnmanaged(IntPtr proxy0)
		{
			return NeedsCollision(BroadphaseProxy.GetManaged(proxy0));
		}

		public virtual bool NeedsCollision(BroadphaseProxy proxy0)
		{
			return btCollisionWorld_ConvexResultCallbackWrapper_needsCollision(Native,
				proxy0.Native);
		}

		public double ClosestHitFraction
		{
			get => btCollisionWorld_ConvexResultCallback_getClosestHitFraction(Native);
			set => btCollisionWorld_ConvexResultCallback_setClosestHitFraction(Native, value);
		}

		public int CollisionFilterGroup
		{
			get => btCollisionWorld_ConvexResultCallback_getCollisionFilterGroup(Native);
			set => btCollisionWorld_ConvexResultCallback_setCollisionFilterGroup(Native, value);
		}

		public int CollisionFilterMask
		{
			get => btCollisionWorld_ConvexResultCallback_getCollisionFilterMask(Native);
			set => btCollisionWorld_ConvexResultCallback_setCollisionFilterMask(Native, value);
		}

		public bool HasHit => btCollisionWorld_ConvexResultCallback_hasHit(Native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btCollisionWorld_ConvexResultCallback_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ConvexResultCallback()
		{
			Dispose(false);
		}
	}

	public class LocalConvexResult : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		private CollisionObject _hitCollisionObject;
		private LocalShapeInfo _localShapeInfo;

		internal LocalConvexResult(IntPtr native)
		{
			Native = native;
			_preventDelete = true;
			_hitCollisionObject = CollisionObject.GetManaged(btCollisionWorld_LocalConvexResult_getHitCollisionObject(Native));
			_localShapeInfo = new LocalShapeInfo(btCollisionWorld_LocalConvexResult_getLocalShapeInfo(Native), true);
		}

		public LocalConvexResult(CollisionObject hitCollisionObject, LocalShapeInfo localShapeInfo,
			BVector3 hitNormalLocal, BVector3 hitPointLocal, double hitFraction)
		{
			Native = btCollisionWorld_LocalConvexResult_new(hitCollisionObject.Native,
				localShapeInfo.Native, ref hitNormalLocal, ref hitPointLocal,
				hitFraction);
			_hitCollisionObject = hitCollisionObject;
			_localShapeInfo = localShapeInfo;
		}

		public CollisionObject HitCollisionObject
		{
			get => _hitCollisionObject;
			set
			{
				btCollisionWorld_LocalConvexResult_setHitCollisionObject(Native, value.Native);
				_hitCollisionObject = value;
			}
		}

		public double HitFraction
		{
			get => btCollisionWorld_LocalConvexResult_getHitFraction(Native);
			set => btCollisionWorld_LocalConvexResult_setHitFraction(Native, value);
		}

		public BVector3 HitNormalLocal
		{
			get
			{
				BVector3 value;
				btCollisionWorld_LocalConvexResult_getHitNormalLocal(Native, out value);
				return value;
			}
			set => btCollisionWorld_LocalConvexResult_setHitNormalLocal(Native, ref value);
		}

		public BVector3 HitPointLocal
		{
			get
			{
				BVector3 value;
				btCollisionWorld_LocalConvexResult_getHitPointLocal(Native, out value);
				return value;
			}
			set => btCollisionWorld_LocalConvexResult_setHitPointLocal(Native, ref value);
		}

		public LocalShapeInfo LocalShapeInfo
		{
			get => _localShapeInfo;
			set
			{
				btCollisionWorld_LocalConvexResult_setLocalShapeInfo(Native, (value != null) ? value.Native : IntPtr.Zero);
				_localShapeInfo = value;
			}
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
				if (!_preventDelete)
				{
					btCollisionWorld_LocalConvexResult_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~LocalConvexResult()
		{
			Dispose(false);
		}
	}

	public class LocalRayResult : IDisposable
	{
		internal IntPtr Native;
		private bool _preventDelete;

		private CollisionObject _collisionObject;
		private LocalShapeInfo _localShapeInfo;

		internal LocalRayResult(IntPtr native)
		{
			Native = native;
			_preventDelete = true;
			_collisionObject = CollisionObject.GetManaged(btCollisionWorld_LocalRayResult_getCollisionObject(Native));
			_localShapeInfo = new LocalShapeInfo(btCollisionWorld_LocalRayResult_getLocalShapeInfo(Native), true);
		}

		public LocalRayResult(CollisionObject collisionObject, LocalShapeInfo localShapeInfo,
			BVector3 hitNormalLocal, double hitFraction)
		{
			Native = btCollisionWorld_LocalRayResult_new(collisionObject.Native,
				localShapeInfo.Native, ref hitNormalLocal, hitFraction);
			_collisionObject = collisionObject;
			_localShapeInfo = localShapeInfo;
		}

		public CollisionObject CollisionObject
		{
			get => _collisionObject;
			set
			{
				btCollisionWorld_LocalRayResult_setCollisionObject(Native, value.Native);
				_collisionObject = value;
			}
		}

		public double HitFraction
		{
			get => btCollisionWorld_LocalRayResult_getHitFraction(Native);
			set => btCollisionWorld_LocalRayResult_setHitFraction(Native, value);
		}

		public BVector3 HitNormalLocal
		{
			get
			{
				BVector3 value;
				btCollisionWorld_LocalRayResult_getHitNormalLocal(Native, out value);
				return value;
			}
			set => btCollisionWorld_LocalRayResult_setHitNormalLocal(Native, ref value);
		}

		public LocalShapeInfo LocalShapeInfo
		{
			get => _localShapeInfo;
			set
			{
				btCollisionWorld_LocalRayResult_setLocalShapeInfo(Native, (value != null) ? value.Native : IntPtr.Zero);
				_localShapeInfo = value;
			}
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
				if (!_preventDelete)
				{
					btCollisionWorld_LocalRayResult_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~LocalRayResult()
		{
			Dispose(false);
		}
	}

	public class LocalShapeInfo : IDisposable
	{
		//!!!!betauser
		public IntPtr Native;
		//internal IntPtr Native;
		bool _preventDelete;

		internal LocalShapeInfo(IntPtr native, bool preventDelete)
		{
			Native = native;
			_preventDelete = preventDelete;
		}

		public LocalShapeInfo()
		{
			Native = btCollisionWorld_LocalShapeInfo_new();
		}

		public int ShapePart
		{
			get => btCollisionWorld_LocalShapeInfo_getShapePart(Native);
			set => btCollisionWorld_LocalShapeInfo_setShapePart(Native, value);
		}

		public int TriangleIndex
		{
			get => btCollisionWorld_LocalShapeInfo_getTriangleIndex(Native);
			set => btCollisionWorld_LocalShapeInfo_setTriangleIndex(Native, value);
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
				if (!_preventDelete)
				{
					btCollisionWorld_LocalShapeInfo_delete(Native);
				}
				Native = IntPtr.Zero;
			}
		}

		~LocalShapeInfo()
		{
			Dispose(false);
		}
	}

	public abstract class RayResultCallback : IDisposable
	{
		internal IntPtr Native;

		[UnmanagedFunctionPointer(Internal.BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate double AddSingleResultUnmanagedDelegate(IntPtr rayResult, bool normalInWorldSpace);
		[UnmanagedFunctionPointer(Internal.BulletSharp.Native.Conv), SuppressUnmanagedCodeSecurity]
		private delegate bool NeedsCollisionUnmanagedDelegate(IntPtr proxy0);

		private AddSingleResultUnmanagedDelegate _addSingleResult;
		private NeedsCollisionUnmanagedDelegate _needsCollision;

		protected RayResultCallback()
		{
			_addSingleResult = AddSingleResultUnmanaged;
			_needsCollision = NeedsCollisionUnmanaged;
			Native = btCollisionWorld_RayResultCallbackWrapper_new(
				Marshal.GetFunctionPointerForDelegate(_addSingleResult),
				Marshal.GetFunctionPointerForDelegate(_needsCollision));
		}

		private double AddSingleResultUnmanaged(IntPtr rayResult, bool normalInWorldSpace)
		{
			return AddSingleResult(new LocalRayResult(rayResult), normalInWorldSpace);
		}

		public abstract double AddSingleResult(LocalRayResult rayResult, bool normalInWorldSpace);

		private bool NeedsCollisionUnmanaged(IntPtr proxy0)
		{
			return NeedsCollision(BroadphaseProxy.GetManaged(proxy0));
		}

		public virtual bool NeedsCollision(BroadphaseProxy proxy0)
		{
			return btCollisionWorld_RayResultCallbackWrapper_needsCollision(Native, proxy0.Native);
		}

		public double ClosestHitFraction
		{
			get => btCollisionWorld_RayResultCallback_getClosestHitFraction(Native);
			set => btCollisionWorld_RayResultCallback_setClosestHitFraction(Native, value);
		}

		public int CollisionFilterGroup
		{
			get => btCollisionWorld_RayResultCallback_getCollisionFilterGroup(Native);
			set => btCollisionWorld_RayResultCallback_setCollisionFilterGroup(Native, value);
		}

		public int CollisionFilterMask
		{
			get => btCollisionWorld_RayResultCallback_getCollisionFilterMask(Native);
			set => btCollisionWorld_RayResultCallback_setCollisionFilterMask(Native, value);
		}

		public CollisionObject CollisionObject
		{
			get => CollisionObject.GetManaged(btCollisionWorld_RayResultCallback_getCollisionObject(Native));
			set => btCollisionWorld_RayResultCallback_setCollisionObject(Native, (value != null) ? value.Native : IntPtr.Zero);
		}

		public uint Flags
		{
			get => btCollisionWorld_RayResultCallback_getFlags(Native);
			set => btCollisionWorld_RayResultCallback_setFlags(Native, value);
		}

		public bool HasHit => btCollisionWorld_RayResultCallback_hasHit(Native);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btCollisionWorld_RayResultCallback_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~RayResultCallback()
		{
			Dispose(false);
		}
	}

	public class CollisionWorld : IDisposable
	{
		internal IntPtr Native;

		internal IDebugDraw _debugDrawer;
		private BroadphaseInterface _broadphase;
		private Dispatcher _dispatcher;
		private DispatcherInfo _dispatchInfo;

		internal CollisionWorld(IntPtr native, Dispatcher dispatcher, BroadphaseInterface broadphase)
		{
			_dispatcher = dispatcher;
			Broadphase = broadphase;

			if (native == IntPtr.Zero)
			{
				return;
			}
			Native = native;
			CollisionObjectArray = new AlignedCollisionObjectArray(btCollisionWorld_getCollisionObjectArray(native), this);
		}

		public CollisionWorld(Dispatcher dispatcher, BroadphaseInterface broadphasePairCache,
			CollisionConfiguration collisionConfiguration)
			: this(btCollisionWorld_new(dispatcher.Native, broadphasePairCache.Native,
				collisionConfiguration.Native), dispatcher, broadphasePairCache)
		{
		}

		public void AddCollisionObject(CollisionObject collisionObject)
		{
			CollisionObjectArray.Add(collisionObject);
		}

		public void AddCollisionObject(CollisionObject collisionObject, CollisionFilterGroups collisionFilterGroup,
			CollisionFilterGroups collisionFilterMask)
		{
			CollisionObjectArray.Add(collisionObject, (int)collisionFilterGroup, (int)collisionFilterMask);
		}

		public void AddCollisionObject(CollisionObject collisionObject, int collisionFilterGroup,
			int collisionFilterMask)
		{
			CollisionObjectArray.Add(collisionObject, collisionFilterGroup, collisionFilterMask);
		}

		public void ComputeOverlappingPairs()
		{
			btCollisionWorld_computeOverlappingPairs(Native);
		}

		public void ContactPairTest(CollisionObject colObjA, CollisionObject colObjB,
			ContactResultCallback resultCallback)
		{
			btCollisionWorld_contactPairTest(Native, colObjA.Native, colObjB.Native,
				resultCallback.Native);
		}

		public void ContactTest(CollisionObject colObj, ContactResultCallback resultCallback)
		{
			btCollisionWorld_contactTest(Native, colObj.Native, resultCallback.Native);
		}

		public void ConvexSweepTestRef(ConvexShape castShape, ref BMatrix from, ref BMatrix to,
			ConvexResultCallback resultCallback, double allowedCcdPenetration = 0)
		{
			btCollisionWorld_convexSweepTest(Native, castShape.Native, ref from, ref to, resultCallback.Native, allowedCcdPenetration);
		}

		public void ConvexSweepTest(ConvexShape castShape, BMatrix from, BMatrix to,
			ConvexResultCallback resultCallback, double allowedCcdPenetration = 0)
		{
			btCollisionWorld_convexSweepTest(Native, castShape.Native, ref from,
				ref to, resultCallback.Native, allowedCcdPenetration);
		}

		public void DebugDrawObjectRef(ref BMatrix worldTransform, CollisionShape shape, ref BVector3 color)
		{
			btCollisionWorld_debugDrawObject(Native, ref worldTransform, shape.Native, ref color);
		}

		public void DebugDrawObject(BMatrix worldTransform, CollisionShape shape,
			BVector3 color)
		{
			btCollisionWorld_debugDrawObject(Native, ref worldTransform, shape.Native,
				ref color);
		}

		public void DebugDrawWorld()
		{
			btCollisionWorld_debugDrawWorld(Native);
		}

		public static void ObjectQuerySingleRef(ConvexShape castShape, ref BMatrix rayFromTrans,
			ref BMatrix rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape,
			ref BMatrix colObjWorldTransform, ConvexResultCallback resultCallback, double allowedPenetration)
		{
			btCollisionWorld_objectQuerySingle(castShape.Native, ref rayFromTrans,
				ref rayToTrans, collisionObject.Native, collisionShape.Native, ref colObjWorldTransform,
				resultCallback.Native, allowedPenetration);
		}

		public static void ObjectQuerySingle(ConvexShape castShape, BMatrix rayFromTrans,
			BMatrix rayToTrans, CollisionObject collisionObject, CollisionShape collisionShape,
			BMatrix colObjWorldTransform, ConvexResultCallback resultCallback, double allowedPenetration)
		{
			btCollisionWorld_objectQuerySingle(castShape.Native, ref rayFromTrans,
				ref rayToTrans, collisionObject.Native, collisionShape.Native, ref colObjWorldTransform,
				resultCallback.Native, allowedPenetration);
		}

		public static void ObjectQuerySingleInternalRef(ConvexShape castShape, ref BMatrix convexFromTrans,
			ref BMatrix convexToTrans, CollisionObjectWrapper colObjWrap, ConvexResultCallback resultCallback,
			double allowedPenetration)
		{
			btCollisionWorld_objectQuerySingleInternal(castShape.Native, ref convexFromTrans,
				ref convexToTrans, colObjWrap.Native, resultCallback.Native, allowedPenetration);
		}

		public static void ObjectQuerySingleInternal(ConvexShape castShape, BMatrix convexFromTrans,
			BMatrix convexToTrans, CollisionObjectWrapper colObjWrap, ConvexResultCallback resultCallback,
			double allowedPenetration)
		{
			btCollisionWorld_objectQuerySingleInternal(castShape.Native, ref convexFromTrans,
				ref convexToTrans, colObjWrap.Native, resultCallback.Native, allowedPenetration);
		}

		public void PerformDiscreteCollisionDetection()
		{
			btCollisionWorld_performDiscreteCollisionDetection(Native);
		}

		public void RayTestRef(ref BVector3 rayFromWorld, ref BVector3 rayToWorld, RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTest(Native, ref rayFromWorld, ref rayToWorld, resultCallback.Native);
		}

		public void RayTest(BVector3 rayFromWorld, BVector3 rayToWorld, RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTest(Native, ref rayFromWorld, ref rayToWorld, resultCallback.Native);
		}

		public static void RayTestSingleRef(ref BMatrix rayFromTrans, ref BMatrix rayToTrans,
			CollisionObject collisionObject, CollisionShape collisionShape, ref BMatrix colObjWorldTransform,
			RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTestSingle(ref rayFromTrans, ref rayToTrans, collisionObject.Native, collisionShape.Native, ref colObjWorldTransform, resultCallback.Native);
		}

		public static void RayTestSingle(BMatrix rayFromTrans, BMatrix rayToTrans,
			CollisionObject collisionObject, CollisionShape collisionShape, BMatrix colObjWorldTransform,
			RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTestSingle(ref rayFromTrans, ref rayToTrans, collisionObject.Native,
				collisionShape.Native, ref colObjWorldTransform, resultCallback.Native);
		}

		public static void RayTestSingleInternalRef(ref BMatrix rayFromTrans, ref BMatrix rayToTrans,
			CollisionObjectWrapper collisionObjectWrap, RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTestSingleInternal(ref rayFromTrans, ref rayToTrans,
				collisionObjectWrap.Native, resultCallback.Native);
		}

		public static void RayTestSingleInternal(BMatrix rayFromTrans, BMatrix rayToTrans,
			CollisionObjectWrapper collisionObjectWrap, RayResultCallback resultCallback)
		{
			btCollisionWorld_rayTestSingleInternal(ref rayFromTrans, ref rayToTrans,
				collisionObjectWrap.Native, resultCallback.Native);
		}

		public void RemoveCollisionObject(CollisionObject collisionObject)
		{
			CollisionObjectArray.Remove(collisionObject);
		}

		protected void SerializeCollisionObjects(Serializer serializer)
		{
			// keep track of shapes already serialized
			var serializedShapes = new Dictionary<CollisionShape, int>();

			foreach (var colObj in CollisionObjectArray)
			{
				var shape = colObj.CollisionShape;
				if (!serializedShapes.ContainsKey(shape))
				{
					serializedShapes.Add(shape, 0);
					shape.SerializeSingleShape(serializer);
				}
			}

			// serialize all collision objects
			foreach (var colObj in CollisionObjectArray)
			{
				if (colObj.InternalType == CollisionObjectTypes.CollisionObject)
				{
					colObj.SerializeSingleObject(serializer);
				}
			}
		}

		public virtual void Serialize(Serializer serializer)
		{
			serializer.StartSerialization();
			SerializeCollisionObjects(serializer);
			serializer.FinishSerialization();
		}

		public void UpdateAabbs()
		{
			btCollisionWorld_updateAabbs(Native);
		}

		public void UpdateSingleAabb(CollisionObject colObj)
		{
			btCollisionWorld_updateSingleAabb(Native, colObj.Native);
		}

		public BroadphaseInterface Broadphase
		{
			get => _broadphase;
			set
			{
				if (_broadphase != null)
				{
					_broadphase._worldRefs.Remove(this);
				}
				// Native can be zero from a constructor argument
				if (Native != IntPtr.Zero)
				{
					btCollisionWorld_setBroadphase(Native, value.Native);
				}
				_broadphase = value;
				value._worldRefs.Add(this);
			}
		}

		public AlignedCollisionObjectArray CollisionObjectArray { get; protected set; }

		public IDebugDraw DebugDrawer
		{
			get => _debugDrawer;
			set
			{
				if (_debugDrawer != null)
				{
					if (_debugDrawer == value) {
						return;
					}

					// Clear IDebugDraw wrapper
					if (!(_debugDrawer is DebugDraw)) {
						//btIDebugDrawer_delete(btCollisionWorld_getDebugDrawer(Native));
					}
				}

				_debugDrawer = value;
				if (value == null) {
					btCollisionWorld_setDebugDrawer(Native, IntPtr.Zero);
					return;
				}

				DebugDraw cast = value as DebugDraw;
				if (cast != null) {
					btCollisionWorld_setDebugDrawer(Native, cast._native);
				} else {
					// Create IDebugDraw wrapper, remember to delete it
					IntPtr wrapper = DebugDraw.CreateWrapper(value, false);
					btCollisionWorld_setDebugDrawer(Native, wrapper);
				}
			}
		}

		public Dispatcher Dispatcher
		{
			get => _dispatcher;
			internal set
			{
				_dispatcher = value;
				_dispatcher._worldRefs.Add(this);
			}
		}

		public DispatcherInfo DispatchInfo
		{
			get
			{
				if (_dispatchInfo == null)
				{
					_dispatchInfo = new DispatcherInfo(btCollisionWorld_getDispatchInfo(Native));
				}
				return _dispatchInfo;
			}
		}

		public bool ForceUpdateAllAabbs
		{
			get => btCollisionWorld_getForceUpdateAllAabbs(Native);
			set => btCollisionWorld_setForceUpdateAllAabbs(Native, value);
		}

		public int NumCollisionObjects => btCollisionWorld_getNumCollisionObjects(Native);

		public OverlappingPairCache PairCache => Broadphase.OverlappingPairCache;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (Native != IntPtr.Zero)
			{
				btCollisionWorld_delete(Native);
				Native = IntPtr.Zero;

				_broadphase._worldRefs.Remove(this);
				if (_broadphase._worldDeferredCleanup && _broadphase._worldRefs.Count == 0)
				{
					_broadphase.Dispose();
				}

				_dispatcher._worldRefs.Remove(this);
				if (_dispatcher._worldDeferredCleanup && _dispatcher._worldRefs.Count == 0)
				{
					_dispatcher.Dispose();
				}
			}
		}

		~CollisionWorld()
		{
			Dispose(false);
		}

		//!!!!betauser
		public IntPtr CallCustomMethod( int message, IntPtr param1, IntPtr param2 )
		{
			return btCollisionWorld_callCustomMethod( Native, message, param1, param2 );
		}
	}
}
