using System;
using System.Collections.Generic;
using System.Numerics;
using PhysicsEngine.Core.Physics;
using PhysicsEngine.Core.Space;
using UnitsNet;
using Force = PhysicsEngine.Core.Physics.Forces.Force;

namespace PhysicsEngine.Core.Model
{
    public class ModelObject
    {
        public readonly string Id = Guid.NewGuid().ToString();

        public Transform Transform { get; set; } = new();

        public Motion Motion { get; set; } = new();

        private Mass _mass = Mass.FromKilograms(1);
        public Mass Mass
        {
            get => _mass;
            set
            {
                if (value.Value <= 0) throw new ArgumentException("Mass must be positive.");
                _mass = value;
            }
        } 

        private readonly IList<Force> _appliedForces = new List<Force>();

        public IEnumerable<Force> AppliedForces => _appliedForces;

        public void AddForce(Force force)
        {
            _appliedForces.Add(force);
        }

        /// <summary>
        /// Updates the item position to the expected one after the given time delta.
        /// Computes the total forces, the speed variation vector, and the resulting new speed vector.
        /// </summary>
        /// <param name="delta"></param>
        public void UpdatePosition(TimeSpan delta)
        {
            var speedVariationVector = ComputeSpeedVariationVector(delta);

            Motion.ApplySpeedVariationVector(speedVariationVector);
            
            Move(delta);
        }

        /// <summary>
        /// Move the object based on its current <see cref="ModelObject.Motion"/>. The object position is updated after
        /// having applied its motion during the specified period.
        /// </summary>
        /// <param name="period">The period of time during which the object is moving.</param>
        public void Move(TimeSpan period)
        {
            var normalizedDirection =Motion.DirectionNormalized;
            var distanceCovered = Motion.Speed * period;
            var speedVector = Vector3.Multiply(normalizedDirection, (float)distanceCovered.Meters);
            Transform.Position += speedVector;
        }

        /// <summary>
        /// Get the speed variation vector, using the following formula :
        /// speedVariationVector = sum(forces) / mass * timeDelta
        /// For a better precision, the time delta should be as close to zero as possible
        /// </summary>
        /// <param name="delta">Time delta</param>
        /// <returns>Speed variation vector</returns>
        public Vector3 ComputeSpeedVariationVector(TimeSpan delta)
        {
            var totalForcesVector = new Vector3();
            foreach (var force in AppliedForces)
            {
                totalForcesVector += force.ComputeVector();
            }

            var speedVariationVector = totalForcesVector / (float) Mass.Kilograms * (float) delta.TotalSeconds;

            return speedVariationVector;
        }
    }
}
