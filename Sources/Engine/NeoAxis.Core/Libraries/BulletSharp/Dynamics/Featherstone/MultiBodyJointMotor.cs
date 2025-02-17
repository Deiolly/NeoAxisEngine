using static Internal.BulletSharp.UnsafeNativeMethods;

namespace Internal.BulletSharp
{
	public class MultiBodyJointMotor : MultiBodyConstraint
	{
		public MultiBodyJointMotor(MultiBody body, int link, double desiredVelocity,
			double maxMotorImpulse)
			: base(btMultiBodyJointMotor_new(body.Native, link, desiredVelocity,
				maxMotorImpulse), body, body)
		{
		}

		public MultiBodyJointMotor(MultiBody body, int link, int linkDoF, double desiredVelocity,
			double maxMotorImpulse)
			: base(btMultiBodyJointMotor_new2(body.Native, link, linkDoF, desiredVelocity,
				maxMotorImpulse), body, body)
		{
		}

		public void SetVelocityTarget(double velTarget)
		{
			btMultiBodyJointMotor_setVelocityTarget(Native, velTarget);
		}
	}
}
