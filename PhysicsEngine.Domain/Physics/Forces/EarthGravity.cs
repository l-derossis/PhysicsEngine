using PhysicsEngine.Core.Model;
using UnitsNet.Units;

namespace PhysicsEngine.Core.Physics.Forces
{
    /// <summary>
    /// This is a naive implementation of the gravity on Earth. The formula applied to compute
    /// the force applied by A to B is :
    /// F(A -> B) = 9.81 * mass of A
    /// </summary>
    public class EarthGravity : Force
    {
        public EarthGravity(ModelObject appliedBy, ModelObject appliedTo) : base(appliedBy, appliedTo)
        {
        }

        public override ForceAction ForceAction => ForceAction.Attraction;

        public override UnitsNet.Force Magnitude =>
            new UnitsNet.Force(AppliedTo.Mass.Kilograms * 9.81, ForceUnit.Newton);
    }
}
