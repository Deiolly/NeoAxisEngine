using System;
using System.Runtime.InteropServices;
using Internal.BulletSharp.Math;
using static Internal.BulletSharp.UnsafeNativeMethods;

namespace Internal.BulletSharp
{
	public class MultiBody : IDisposable
	{
		internal IntPtr Native;
		private MultiBodyLink[] _links;

		internal MultiBody(IntPtr native)
		{
			Native = native;
		}

		public MultiBody(int nLinks, double mass, BVector3 inertia, bool fixedBase,
			bool canSleep)
		{
			Native = btMultiBody_new(nLinks, mass, ref inertia, fixedBase, canSleep);
		}

		public void AddBaseConstraintForce(BVector3 f)
		{
			btMultiBody_addBaseConstraintForce(Native, ref f);
		}

		public void AddBaseConstraintTorque(BVector3 t)
		{
			btMultiBody_addBaseConstraintTorque(Native, ref t);
		}

		public void AddBaseForce(BVector3 f)
		{
			btMultiBody_addBaseForce(Native, ref f);
		}

		public void AddBaseTorque(BVector3 t)
		{
			btMultiBody_addBaseTorque(Native, ref t);
		}

		public void AddJointTorque(int i, double q)
		{
			btMultiBody_addJointTorque(Native, i, q);
		}

		public void AddJointTorqueMultiDof(int i, double[] q)
		{
			btMultiBody_addJointTorqueMultiDof(Native, i, q);
		}

		public void AddJointTorqueMultiDof(int i, int dof, double q)
		{
			btMultiBody_addJointTorqueMultiDof2(Native, i, dof, q);
		}

		public void AddLinkConstraintForce(int i, BVector3 f)
		{
			btMultiBody_addLinkConstraintForce(Native, i, ref f);
		}

		public void AddLinkConstraintTorque(int i, BVector3 t)
		{
			btMultiBody_addLinkConstraintTorque(Native, i, ref t);
		}

		public void AddLinkForce(int i, BVector3 f)
		{
			btMultiBody_addLinkForce(Native, i, ref f);
		}

		public void AddLinkTorque(int i, BVector3 t)
		{
			btMultiBody_addLinkTorque(Native, i, ref t);
		}
		/*

		public void ApplyDeltaVeeMultiDof(double deltaVee, double multiplier)
		{
			btMultiBody_applyDeltaVeeMultiDof(Native, deltaVee.Native, multiplier);
		}

		public void ApplyDeltaVeeMultiDof2(double deltaVee, double multiplier)
		{
			btMultiBody_applyDeltaVeeMultiDof2(Native, deltaVee.Native, multiplier);
		}

		public void CalcAccelerationDeltasMultiDof(double force, double output, AlignedObjectArray<double> scratchR,
			AlignedObjectArray<btVector3> scratchV)
		{
			btMultiBody_calcAccelerationDeltasMultiDof(Native, force.Native, output.Native,
				scratchR.Native, scratchV.Native);
		}
		*/
		public int CalculateSerializeBufferSize()
		{
			return btMultiBody_calculateSerializeBufferSize(Native);
		}

		public void CheckMotionAndSleepIfRequired(double timestep)
		{
			btMultiBody_checkMotionAndSleepIfRequired(Native, timestep);
		}

		public void ClearConstraintForces()
		{
			btMultiBody_clearConstraintForces(Native);
		}

		public void ClearForcesAndTorques()
		{
			btMultiBody_clearForcesAndTorques(Native);
		}

		public void ClearVelocities()
		{
			btMultiBody_clearVelocities(Native);
		}
		/*
		public void ComputeAccelerationsArticulatedBodyAlgorithmMultiDof(double deltaTime,
			AlignedObjectArray<double> scratchR, AlignedObjectArray<btVector3> scratchV,
			AlignedObjectArray<btMatrix3x3> scratchM, bool isConstraintPass = false)
		{
			btMultiBody_computeAccelerationsArticulatedBodyAlgorithmMultiDof(Native,
				deltaTime, scratchR.Native, scratchV.Native, scratchM.Native, isConstraintPass);
		}

		public void FillConstraintJacobianMultiDof(int link, Vector3 contactPoint,
			Vector3 normalAng, Vector3 normalLin, double jac, AlignedObjectArray<double> scratchR,
			AlignedObjectArray<btVector3> scratchV, AlignedObjectArray<btMatrix3x3> scratchM)
		{
			btMultiBody_fillConstraintJacobianMultiDof(Native, link, ref contactPoint,
				ref normalAng, ref normalLin, jac.Native, scratchR.Native, scratchV.Native,
				scratchM.Native);
		}

		public void FillContactJacobianMultiDof(int link, Vector3 contactPoint, Vector3 normal,
			double jac, AlignedObjectArray<double> scratchR, AlignedObjectArray<btVector3> scratchV,
			AlignedObjectArray<btMatrix3x3> scratchM)
		{
			btMultiBody_fillContactJacobianMultiDof(Native, link, ref contactPoint,
				ref normal, jac.Native, scratchR.Native, scratchV.Native, scratchM.Native);
		}
		*/
		public void FinalizeMultiDof()
		{
			btMultiBody_finalizeMultiDof(Native);
		}
		/*
		public void ForwardKinematics(btAlignedObjectArray<btQuaternion> scratchQ,
			AlignedObjectArray<btVector3> scratchM)
		{
			btMultiBody_forwardKinematics(Native, scratchQ.Native, scratchM.Native);
		}
		*/
		public double GetJointPos(int i)
		{
			return btMultiBody_getJointPos(Native, i);
		}
		/*
		public double GetJointPosMultiDof(int i)
		{
			return btMultiBody_getJointPosMultiDof(Native, i);
		}
		*/
		public double GetJointTorque(int i)
		{
			return btMultiBody_getJointTorque(Native, i);
		}
		/*
		public double GetJointTorqueMultiDof(int i)
		{
			return btMultiBody_getJointTorqueMultiDof(Native, i);
		}
		*/
		public double GetJointVel(int i)
		{
			return btMultiBody_getJointVel(Native, i);
		}
		/*
		public double GetJointVelMultiDof(int i)
		{
			return btMultiBody_getJointVelMultiDof(Native, i);
		}
		*/
		public MultiBodyLink GetLink(int index)
		{
			if (_links == null) {
				_links = new MultiBodyLink[NumLinks];
			}
			if (_links[index] == null) {
				_links[index] = new MultiBodyLink(btMultiBody_getLink(Native, index));
			}
			return _links[index];
		}

		public BVector3 GetLinkForce(int i)
		{
			BVector3 value;
			btMultiBody_getLinkForce(Native, i, out value);
			return value;
		}

		public BVector3 GetLinkInertia(int i)
		{
			BVector3 value;
			btMultiBody_getLinkInertia(Native, i, out value);
			return value;
		}

		public double GetLinkMass(int i)
		{
			return btMultiBody_getLinkMass(Native, i);
		}

		public BVector3 GetLinkTorque(int i)
		{
			BVector3 value;
			btMultiBody_getLinkTorque(Native, i, out value);
			return value;
		}

		public int GetParent(int linkNum)
		{
			return btMultiBody_getParent(Native, linkNum);
		}

		public BQuaternion GetParentToLocalRot(int i)
		{
			BQuaternion value;
			btMultiBody_getParentToLocalRot(Native, i, out value);
			return value;
		}

		public BVector3 GetRVector(int i)
		{
			BVector3 value;
			btMultiBody_getRVector(Native, i, out value);
			return value;
		}

		public void GoToSleep()
		{
			btMultiBody_goToSleep(Native);
		}

		public bool InternalNeedsJointFeedback()
		{
			return btMultiBody_internalNeedsJointFeedback(Native);
		}

		public BVector3 LocalDirToWorld(int i, BVector3 vec)
		{
			BVector3 value;
			btMultiBody_localDirToWorld(Native, i, ref vec, out value);
			return value;
		}

		public BMatrix LocalFrameToWorld(int i, BMatrix mat)
		{
			BMatrix value;
			btMultiBody_localFrameToWorld(Native, i, ref mat, out value);
			return value;
		}

		public BVector3 LocalPosToWorld(int i, BVector3 vec)
		{
			BVector3 value;
			btMultiBody_localPosToWorld(Native, i, ref vec, out value);
			return value;
		}

		public void ProcessDeltaVeeMultiDof2()
		{
			btMultiBody_processDeltaVeeMultiDof2(Native);
		}

		public string Serialize(IntPtr dataBuffer, Serializer serializer)
		{
			return Marshal.PtrToStringAnsi(btMultiBody_serialize(Native, dataBuffer, serializer._native));
		}

		public void SetJointPos(int i, double q)
		{
			btMultiBody_setJointPos(Native, i, q);
		}

		public void SetJointPosMultiDof(int i, double[] q)
		{
			btMultiBody_setJointPosMultiDof(Native, i, q);
		}

		public void SetJointVel(int i, double qdot)
		{
			btMultiBody_setJointVel(Native, i, qdot);
		}

		public void SetJointVelMultiDof(int i, double[] qdot)
		{
			btMultiBody_setJointVelMultiDof(Native, i, qdot);
		}

		public void SetPosUpdated(bool updated)
		{
			btMultiBody_setPosUpdated(Native, updated);
		}

		public void SetupFixed(int linkIndex, double mass, BVector3 inertia, int parent,
			BQuaternion rotParentToThis, BVector3 parentComToThisPivotOffset, BVector3 thisPivotToThisComOffset,
			bool deprecatedDisableParentCollision = true)
		{
			btMultiBody_setupFixed(Native, linkIndex, mass, ref inertia, parent,
				ref rotParentToThis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset,
				deprecatedDisableParentCollision);
		}

		public void SetupPlanar(int i, double mass, BVector3 inertia, int parent, BQuaternion rotParentToThis,
			BVector3 rotationAxis, BVector3 parentComToThisComOffset, bool disableParentCollision = false)
		{
			btMultiBody_setupPlanar(Native, i, mass, ref inertia, parent, ref rotParentToThis,
				ref rotationAxis, ref parentComToThisComOffset, disableParentCollision);
		}

		public void SetupPrismatic(int i, double mass, BVector3 inertia, int parent,
			BQuaternion rotParentToThis, BVector3 jointAxis, BVector3 parentComToThisPivotOffset,
			BVector3 thisPivotToThisComOffset, bool disableParentCollision)
		{
			btMultiBody_setupPrismatic(Native, i, mass, ref inertia, parent, ref rotParentToThis,
				ref jointAxis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset,
				disableParentCollision);
		}

		public void SetupRevolute(int linkIndex, double mass, BVector3 inertia, int parentIndex,
			BQuaternion rotParentToThis, BVector3 jointAxis, BVector3 parentComToThisPivotOffset,
			BVector3 thisPivotToThisComOffset, bool disableParentCollision = false)
		{
			btMultiBody_setupRevolute(Native, linkIndex, mass, ref inertia, parentIndex,
				ref rotParentToThis, ref jointAxis, ref parentComToThisPivotOffset,
				ref thisPivotToThisComOffset, disableParentCollision);
		}

		public void SetupSpherical(int linkIndex, double mass, BVector3 inertia, int parent,
			BQuaternion rotParentToThis, BVector3 parentComToThisPivotOffset, BVector3 thisPivotToThisComOffset,
			bool disableParentCollision = false)
		{
			btMultiBody_setupSpherical(Native, linkIndex, mass, ref inertia, parent,
				ref rotParentToThis, ref parentComToThisPivotOffset, ref thisPivotToThisComOffset,
				disableParentCollision);
		}

		public void StepPositionsMultiDof(double deltaTime, double[] pq = null, double[] pqd = null)
		{
			btMultiBody_stepPositionsMultiDof(Native, deltaTime, pq, pqd);
		}
		/*
		public void StepVelocitiesMultiDof(double deltaTime, AlignedObjectArray<double> scratchR,
			AlignedObjectArray<btVector3> scratchV, AlignedObjectArray<btMatrix3x3> scratchM,
			bool isConstraintPass = false)
		{
			btMultiBody_stepVelocitiesMultiDof(Native, deltaTime, scratchR.Native,
				scratchV.Native, scratchM.Native, isConstraintPass);
		}

		public void UpdateCollisionObjectWorldTransforms(btAlignedObjectArray<btQuaternion> scratchQ,
			AlignedObjectArray<btVector3> scratchM)
		{
			btMultiBody_updateCollisionObjectWorldTransforms(Native, scratchQ.Native,
				scratchM.Native);
		}
		*/
		public void WakeUp()
		{
			btMultiBody_wakeUp(Native);
		}

		public BVector3 WorldDirToLocal(int i, BVector3 vec)
		{
			BVector3 value;
			btMultiBody_worldDirToLocal(Native, i, ref vec, out value);
			return value;
		}

		public BVector3 WorldPosToLocal(int i, BVector3 vec)
		{
			BVector3 value;
			btMultiBody_worldPosToLocal(Native, i, ref vec, out value);
			return value;
		}

		public double AngularDamping
		{
			get => btMultiBody_getAngularDamping(Native);
			set => btMultiBody_setAngularDamping(Native, value);
		}

		public BVector3 AngularMomentum
		{
			get
			{
				BVector3 value;
				btMultiBody_getAngularMomentum(Native, out value);
				return value;
			}
		}

		public MultiBodyLinkCollider BaseCollider
		{
			get => CollisionObject.GetManaged(btMultiBody_getBaseCollider(Native)) as MultiBodyLinkCollider;
			set => btMultiBody_setBaseCollider(Native, value.Native);
		}

		public BVector3 BaseForce
		{
			get
			{
				BVector3 value;
				btMultiBody_getBaseForce(Native, out value);
				return value;
			}
		}

		public BVector3 BaseInertia
		{
			get
			{
				BVector3 value;
				btMultiBody_getBaseInertia(Native, out value);
				return value;
			}
			set => btMultiBody_setBaseInertia(Native, ref value);
		}

		public double BaseMass
		{
			get => btMultiBody_getBaseMass(Native);
			set => btMultiBody_setBaseMass(Native, value);
		}
		/*
		public char BaseName
		{
			get { return btMultiBody_getBaseName(Native); }
			set { btMultiBody_setBaseName(Native, value.Native); }
		}
		*/
		public BVector3 BaseOmega
		{
			get
			{
				BVector3 value;
				btMultiBody_getBaseOmega(Native, out value);
				return value;
			}
			set => btMultiBody_setBaseOmega(Native, ref value);
		}

		public BVector3 BasePosition
		{
			get
			{
				BVector3 value;
				btMultiBody_getBasePos(Native, out value);
				return value;
			}
			set => btMultiBody_setBasePos(Native, ref value);
		}

		public BVector3 BaseTorque
		{
			get
			{
				BVector3 value;
				btMultiBody_getBaseTorque(Native, out value);
				return value;
			}
		}

		public BVector3 BaseVelocity
		{
			get
			{
				BVector3 value;
				btMultiBody_getBaseVel(Native, out value);
				return value;
			}
			set => btMultiBody_setBaseVel(Native, ref value);
		}

		public BMatrix BaseWorldTransform
		{
			get
			{
				BMatrix value;
				btMultiBody_getBaseWorldTransform(Native, out value);
				return value;
			}
			set => btMultiBody_setBaseWorldTransform(Native, ref value);
		}

		public bool CanSleep
		{
			get => btMultiBody_getCanSleep(Native);
			set => btMultiBody_setCanSleep(Native, value);
		}

		public int CompanionId
		{
			get => btMultiBody_getCompanionId(Native);
			set => btMultiBody_setCompanionId(Native, value);
		}

		public bool HasFixedBase => btMultiBody_hasFixedBase(Native);
		public bool HasSelfCollision
		{
			get => btMultiBody_hasSelfCollision(Native);
			set => btMultiBody_setHasSelfCollision(Native, value);
		}

		public bool IsAwake => btMultiBody_isAwake(Native);

		public bool IsPosUpdated => btMultiBody_isPosUpdated(Native);

		public bool IsUsingGlobalVelocities
		{
			get => btMultiBody_isUsingGlobalVelocities(Native);
			set => btMultiBody_useGlobalVelocities(Native, value);
		}

		public bool IsUsingRK4Integration
		{
			get => btMultiBody_isUsingRK4Integration(Native);
			set => btMultiBody_useRK4Integration(Native, value);
		}

		public double KineticEnergy => btMultiBody_getKineticEnergy(Native);

		public double LinearDamping
		{
			get => btMultiBody_getLinearDamping(Native);
			set => btMultiBody_setLinearDamping(Native, value);
		}

		public double MaxAppliedImpulse
		{
			get => btMultiBody_getMaxAppliedImpulse(Native);
			set => btMultiBody_setMaxAppliedImpulse(Native, value);
		}

		public double MaxCoordinateVelocity
		{
			get => btMultiBody_getMaxCoordinateVelocity(Native);
			set => btMultiBody_setMaxCoordinateVelocity(Native, value);
		}

		public int NumDofs => btMultiBody_getNumDofs(Native);

		public int NumLinks
		{
			get => btMultiBody_getNumLinks(Native);
			set
			{
				btMultiBody_setNumLinks(Native, value);
				if (_links != null)
				{
					Array.Resize(ref _links, value);
				}
			}
		}

		public int NumPosVars => btMultiBody_getNumPosVars(Native);

		public bool UseGyroTerm
		{
			get => btMultiBody_getUseGyroTerm(Native);
			set => btMultiBody_setUseGyroTerm(Native, value);
		}

		public int UserIndex
		{
			get => btMultiBody_getUserIndex(Native);
			set => btMultiBody_setUserIndex(Native, value);
		}

		public int UserIndex2
		{
			get => btMultiBody_getUserIndex2(Native);
			set => btMultiBody_setUserIndex2(Native, value);
		}

		public IntPtr UserPointer
		{
			get => btMultiBody_getUserPointer(Native);
			set => btMultiBody_setUserPointer(Native, value);
		}
		/*
		public double VelocityVector
		{
			get { return btMultiBody_getVelocityVector(Native); }
		}
		*/
		public BQuaternion WorldToBaseRot
		{
			get
			{
				BQuaternion value;
				btMultiBody_getWorldToBaseRot(Native, out value);
				return value;
			}
			set => btMultiBody_setWorldToBaseRot(Native, ref value);
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
				btMultiBody_delete(Native);
				Native = IntPtr.Zero;
			}
		}

		~MultiBody()
		{
			Dispose(false);
		}
	}
}
