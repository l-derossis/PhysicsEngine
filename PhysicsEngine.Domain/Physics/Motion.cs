using System;
using System.Numerics;
using UnitsNet;

namespace PhysicsEngine.Core.Physics
{
    public class Motion
    {
        public Speed Speed { get; set; } = Speed.Zero;

        private Vector3 _directionNormalized;
        /// <summary>
        /// Direction of the force as a normalized vector.
        /// The vector is automatically normalized when set except when it is a zero vector.
        /// </summary>
        public Vector3 DirectionNormalized
        {
            get => _directionNormalized;
            set => _directionNormalized = value == Vector3.Zero ? Vector3.Zero : Vector3.Normalize(value);
        }

        /// <summary>
        /// Returns a vector with the correct direction and which length is equal to the speed in m/s
        /// </summary>
        public Vector3 Vector => DirectionNormalized * (float) Speed.MetersPerSecond;

        public void ApplySpeedVariationVector(Vector3 speedVariationVector)
        {
            var updatedSpeedVector = Vector + speedVariationVector;
            Speed = Speed.FromMetersPerSecond(updatedSpeedVector.Length());
            DirectionNormalized = updatedSpeedVector;
        }
    }
}
