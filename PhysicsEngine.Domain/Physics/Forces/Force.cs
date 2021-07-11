using System;
using System.Numerics;
using PhysicsEngine.Core.Model;

namespace PhysicsEngine.Core.Physics.Forces
{
    public abstract class Force 
    {
        public Force(ModelObject appliedBy, ModelObject appliedTo)
        {
            AppliedTo = appliedTo ?? throw new ArgumentNullException(nameof(appliedTo));
            AppliedBy= appliedBy ?? throw new ArgumentNullException(nameof(appliedBy));
        }

        public ModelObject AppliedTo { get; }

        public ModelObject AppliedBy { get; }

        public abstract Action Action { get; }

        public abstract UnitsNet.Force Magnitude { get; }

        public Vector3 ComputeVector()
        {
            throw new NotImplementedException();
        }
    }
}
