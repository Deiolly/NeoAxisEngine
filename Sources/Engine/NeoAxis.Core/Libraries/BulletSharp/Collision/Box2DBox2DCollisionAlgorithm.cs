//using System;
//using static Internal.BulletSharp.UnsafeNativeMethods;

//namespace Internal.BulletSharp
//{
//	public class Box2DBox2DCollisionAlgorithm : ActivatingCollisionAlgorithm
//	{
//		public class CreateFunc : CollisionAlgorithmCreateFunc
//		{
//			internal CreateFunc(IntPtr native)
//				: base(native, true)
//			{
//			}

//			public CreateFunc()
//				: base(btBox2dBox2dCollisionAlgorithm_CreateFunc_new(), false)
//			{
//			}

//			public override CollisionAlgorithm CreateCollisionAlgorithm(CollisionAlgorithmConstructionInfo __unnamed0,
//				CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
//			{
//				return new Box2DBox2DCollisionAlgorithm(btCollisionAlgorithmCreateFunc_CreateCollisionAlgorithm(
//					Native, __unnamed0.Native, body0Wrap.Native, body1Wrap.Native));
//			}
//		}

//		internal Box2DBox2DCollisionAlgorithm(IntPtr native)
//			: base(native)
//		{
//		}

//		public Box2DBox2DCollisionAlgorithm(CollisionAlgorithmConstructionInfo ci)
//			: base(btBox2dBox2dCollisionAlgorithm_new(ci.Native))
//		{
//		}

//		public Box2DBox2DCollisionAlgorithm(PersistentManifold mf, CollisionAlgorithmConstructionInfo ci,
//			CollisionObjectWrapper body0Wrap, CollisionObjectWrapper body1Wrap)
//			: base(btBox2dBox2dCollisionAlgorithm_new2(mf.Native, ci.Native, body0Wrap.Native,
//				body1Wrap.Native))
//		{
//		}
//	}
//}
