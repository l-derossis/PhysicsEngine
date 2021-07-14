using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
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

        public abstract ForceAction ForceAction { get; }

        public abstract UnitsNet.Force Magnitude { get; }

        public Vector3 ComputeVector()
        {
            var vector = ForceAction == ForceAction.Attraction
                ? AppliedBy.Transform.Position - AppliedTo.Transform.Position
                : AppliedTo.Transform.Position - AppliedBy.Transform.Position;

            vector = vector == Vector3.Zero ? Vector3.Zero : Vector3.Normalize(vector);

            return vector * (float)Magnitude.Newtons;
        }
    }
}
