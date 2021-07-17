using System.Numerics;
using PhysicsEngine.Core.Model;

namespace PhysicsEngine.Core.Physics.Forces
{
    public class PendulumForce : Force
    {
        public PendulumForce(ModelObject fixedPoint, ModelObject attachedObject) : base(fixedPoint, attachedObject)
        {
        }

        public override ForceAction ForceAction => ForceAction.Attraction;

        public override UnitsNet.Force Magnitude => ComputeMagnitude();
        
        private UnitsNet.Force ComputeMagnitude()
        {
            // For a pendulum, the tension force (force applied by the link on the suspended item) is :
            // Tension = Centripedal force + gravity force
            // With :
            // Centripedal force = mass * (speed² / trajectory radius), the trajectory radius being equal to the link length
            // Gravity force = weight * cos(angle between link & vertical)

            var pendulumDirection = AppliedTo.Transform.Position - AppliedBy.Transform.Position;
            var pendulumLength = pendulumDirection.Length();
            var cForce = AppliedTo.Mass.Kilograms * (AppliedTo.Motion.Speed.MetersPerSecond * AppliedTo.Motion.Speed.MetersPerSecond / pendulumLength);

            var weight = Vector3.Normalize(new Vector3(0, 1, 0)) * (float)(AppliedTo.Mass.Kilograms * 9.81f);
            var dotProduct = Vector3.Dot(pendulumDirection, weight);
            var denominator = pendulumDirection.Length() * weight.Length();
            var angleCosinus = dotProduct / denominator;
            var gForce = weight * angleCosinus;

            var tension = UnitsNet.Force.FromNewtons(cForce + gForce.Length());

            return tension;
        }
    }
}
