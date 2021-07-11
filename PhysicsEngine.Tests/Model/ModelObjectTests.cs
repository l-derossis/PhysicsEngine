using System;
using System.Numerics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Physics;
using UnitsNet;

namespace PhysicsEngine.Tests.Model
{
    [TestClass]
    public class ModelObjectTests
    {
        private const float FloatingPointTolerance = 1e-5f;

        private readonly ModelObject _object = new ModelObject();

        [TestMethod]
        public void Move_SingleDimensionMovement()
        {
            var motion = new Motion {Direction = new Vector3(1, 0, 0), Speed = Speed.FromMetersPerSecond(2)};
            _object.Motion = motion;

            _object.Move(TimeSpan.FromSeconds(1));

            _object.Transform.Position.X.Should().BeApproximately(200, FloatingPointTolerance);
        }

        [TestMethod]
        public void Move_TwoDimensionsMovement()
        {
            var motion = new Motion { Direction = new Vector3(4, 3, 0), Speed = Speed.FromCentimetersPerSecond(1) };
            _object.Motion = motion;

            _object.Move(TimeSpan.FromSeconds(5));

            _object.Transform.Position.X.Should().BeApproximately(4, FloatingPointTolerance);
            _object.Transform.Position.Y.Should().BeApproximately(3, FloatingPointTolerance);
        }
    }
}
