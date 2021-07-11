using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using PhysicsEngine.Core.Physics;
using PhysicsEngine.Core.Space;
using UnitsNet;
using Force = PhysicsEngine.Core.Physics.Forces.Force;

namespace PhysicsEngine.Core.Model
{
    public class ModelObject
    {
        public readonly string Id = Guid.NewGuid().ToString();

        public Transform Transform { get; set; } = new Transform();

        public Motion Motion { get; set; } = new Motion();

        public Mass Mass { get; set; }

        private readonly IList<Force> _forces = new List<Force>();

        public IEnumerable<Force> Forces => _forces;

        /// <summary>
        /// Move the object based on its current <see cref="ModelObject.Motion"/>. The object position is updated after
        /// having applied its motion during the specified period.
        /// </summary>
        /// <param name="period">The period of time during which the object is moving.</param>
        public void Move(TimeSpan period)
        {
            var normalizedDirection = Vector3.Normalize(Motion.Direction);
            var distanceCovered = Motion.Speed * period;
            var speedVector = Vector3.Multiply(normalizedDirection, (float)distanceCovered.Centimeters);
            Transform.Position += speedVector;
        }
    }
}
