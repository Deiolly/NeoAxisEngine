using System;
using Internal.BulletSharp.Math;
using static Internal.BulletSharp.UnsafeNativeMethods;

namespace Internal.BulletSharp
{
	public class ConvexCast : IDisposable
	{
		public class CastResult : IDisposable
		{
			internal IntPtr Native;

			internal CastResult(IntPtr native)
			{
				Native = native;
			}

			public CastResult()
			{
				Native = btConvexCast_CastResult_new();
			}

			public void DebugDraw(double fraction)
			{
				btConvexCast_CastResult_DebugDraw(Native, fraction);
			}

			public void DrawCoordSystem(BMatrix trans)
			{
				btConvexCast_CastResult_drawCoordSystem(Native, ref trans);
			}

			public void ReportFailure(int errNo, int numIterations)
			{
				btConvexCast_CastResult_reportFailure(Native, errNo, numIterations);
			}

			public double AllowedPenetration
			{
				get => btConvexCast_CastResult_getAllowedPenetration(Native);
				set => btConvexCast_CastResult_setAllowedPenetration(Native, value);
			}

			public IDebugDraw DebugDrawer
			{
				get => Internal.BulletSharp.DebugDraw.GetManaged(btConvexCast_CastResult_getDebugDrawer(Native));
				set => btConvexCast_CastResult_setDebugDrawer(Native, Internal.BulletSharp.DebugDraw.GetUnmanaged(value));
			}

			public double Fraction
			{
				get => btConvexCast_CastResult_getFraction(Native);
				set => btConvexCast_CastResult_setFraction(Native, value);
			}

			public BVector3 HitPoint
			{
				get
				{
					BVector3 value;
					btConvexCast_CastResult_getHitPoint(Native, out value);
					return value;
				}
				set => btConvexCast_CastResult_setHitPoint(Native, ref value);
			}

			public BMatrix HitTransformA
			{
				get
				{
					BMatrix value;
					btConvexCast_CastResult_getHitTransformA(Native, out value);
					return value;
				}
				set => btConvexCast_CastResult_setHitTransformA(Native, ref value);
			}

			public BMatrix HitTransformB
			{
				get
				{
					BMatrix value;
					btConvexCast_CastResult_getHitTransformB(Native, out value);
					return value;
				}
				set => btConvexCast_CastResult_setHitTransformB(Native, ref value);
			}

			public BVector3 Normal
			{
				get
				{
					BVector3 value;
					btConvexCast_CastResult_getNormal(Native, out value);
					return value;
				}
				set => btConvexCast_CastResult_setNormal(Native, ref value);
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
					btConvexCast_CastResult_delete(Native);
					Native = IntPtr.Zero;
				}
			}

			~CastResult()
			{
				Dispose(false);
			}
		}

		internal IntPtr Native;

		internal ConvexCast(IntPtr native)
		{
			Native = native;
		}

		public bool CalcTimeOfImpact(BMatrix fromA, BMatrix toA, BMatrix fromB, BMatrix toB,
			CastResult result)
		{
			return btConvexCast_calcTimeOfImpact(Native, ref fromA, ref toA, ref fromB,
				ref toB, result.Native);
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
				btConvexCast_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~ConvexCast()
		{
			Dispose(false);
		}
	}
}
