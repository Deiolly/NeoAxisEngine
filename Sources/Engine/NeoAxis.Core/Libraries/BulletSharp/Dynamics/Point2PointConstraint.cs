using System;
using System.Runtime.InteropServices;
using Internal.BulletSharp.Math;
using static Internal.BulletSharp.UnsafeNativeMethods;

namespace Internal.BulletSharp
{
	[Flags]
	public enum Point2PointFlags
	{
		None = 0,
		Erp = 1,
		Cfm = 2
	}

	public class ConstraintSetting
	{
		internal IntPtr Native;

		internal ConstraintSetting(IntPtr native)
		{
			Native = native;
		}

		public double Damping
		{
			get => btConstraintSetting_getDamping(Native);
			set => btConstraintSetting_setDamping(Native, value);
		}

		public double ImpulseClamp
		{
			get => btConstraintSetting_getImpulseClamp(Native);
			set => btConstraintSetting_setImpulseClamp(Native, value);
		}

		public double Tau
		{
			get => btConstraintSetting_getTau(Native);
			set => btConstraintSetting_setTau(Native, value);
		}
	}

	public class Point2PointConstraint : TypedConstraint
	{
		public Point2PointConstraint(RigidBody rigidBodyA, RigidBody rigidBodyB,
			BVector3 pivotInA, BVector3 pivotInB)
			: base(btPoint2PointConstraint_new(rigidBodyA.Native, rigidBodyB.Native,
				ref pivotInA, ref pivotInB))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = rigidBodyB;
		}

		public Point2PointConstraint(RigidBody rigidBodyA, BVector3 pivotInA)
			: base(btPoint2PointConstraint_new2(rigidBodyA.Native, ref pivotInA))
		{
			_rigidBodyA = rigidBodyA;
			_rigidBodyB = GetFixedBody();
		}

		public void GetInfo1NonVirtual(ConstraintInfo1 info)
		{
			btPoint2PointConstraint_getInfo1NonVirtual(Native, info._native);
		}

		public void GetInfo2NonVirtual(ConstraintInfo2 info, BMatrix body0Trans, BMatrix body1Trans)
		{
			btPoint2PointConstraint_getInfo2NonVirtual(Native, info._native, ref body0Trans,
				ref body1Trans);
		}

		public void UpdateRhs(double timeStep)
		{
			btPoint2PointConstraint_updateRHS(Native, timeStep);
		}

		public Point2PointFlags Flags => btPoint2PointConstraint_getFlags(Native);

		public BVector3 PivotInA
		{
			get
			{
				BVector3 value;
				btPoint2PointConstraint_getPivotInA(Native, out value);
				return value;
			}
			set => btPoint2PointConstraint_setPivotA(Native, ref value);
		}

		public BVector3 PivotInB
		{
			get
			{
				BVector3 value;
				btPoint2PointConstraint_getPivotInB(Native, out value);
				return value;
			}
			set => btPoint2PointConstraint_setPivotB(Native, ref value);
		}

		public ConstraintSetting Setting => new ConstraintSetting(btPoint2PointConstraint_getSetting(Native));

		public bool UseSolveConstraintObsolete
		{
			get => btPoint2PointConstraint_getUseSolveConstraintObsolete(Native);
			set => btPoint2PointConstraint_setUseSolveConstraintObsolete(Native, value);
		}
	}

    [StructLayout(LayoutKind.Sequential)]
    internal struct Point2PointConstraintFloatData
    {
        public TypedConstraintFloatData TypedConstraintData;
        public Vector3FloatData PivotInA;
        public Vector3FloatData PivotInB;

        public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Point2PointConstraintFloatData), fieldName).ToInt32(); }
    }

    [StructLayout(LayoutKind.Sequential)]
	internal struct Point2PointConstraintDoubleData
	{
		public TypedConstraintDoubleData TypedConstraintData;
		public Vector3DoubleData PivotInA;
		public Vector3DoubleData PivotInB;

		public static int Offset(string fieldName) { return Marshal.OffsetOf(typeof(Point2PointConstraintDoubleData), fieldName).ToInt32(); }
	}
}
