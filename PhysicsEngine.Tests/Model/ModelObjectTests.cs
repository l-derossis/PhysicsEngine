using System;
using System.Numerics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Physics;
using PhysicsEngine.Core.Physics.Forces;
using UnitsNet;
using Force = PhysicsEngine.Core.Physics.Forces.Force;

namespace PhysicsEngine.Tests.Model
{
    [TestClass]
    public class ModelObjectTests
    {
        private const float FloatingPointTolerance = 1e-5f;

        private readonly ModelObject _object = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow(-2)]
        [DataRow(0)]
        public void MassShouldBePositive(int mass)
        {
            var m = Mass.FromKilograms(mass);
            var _ = new ModelObject
            {
                Mass = m
            };
        }

        [TestMethod]
        public void Move_NoMovement()
        {
            _object.Move(TimeSpan.FromSeconds(1));

            _object.Transform.Position.X.Should().BeApproximately(0, FloatingPointTolerance);
        }

        [TestMethod]
        public void Move_SingleDimensionMovement()
        {
            var motion = new Motion { DirectionNormalized = new Vector3(1, 0, 0), Speed = Speed.FromMetersPerSecond(2) };
            _object.Motion = motion;

            _object.Move(TimeSpan.FromSeconds(1));

            _object.Transform.Position.X.Should().BeApproximately(2, FloatingPointTolerance);
        }

        [TestMethod]
        public void Move_TwoDimensionsMovement()
        {
            var motion = new Motion { DirectionNormalized = new Vector3(4, 3, 0), Speed = Speed.FromMetersPerSecond(1) };
            _object.Motion = motion;

            _object.Move(TimeSpan.FromSeconds(5));

            _object.Transform.Position.X.Should().BeApproximately(4, FloatingPointTolerance);
            _object.Transform.Position.Y.Should().BeApproximately(3, FloatingPointTolerance);
        }

        [TestMethod]
        public void ComputeSpeedVariationVector_SimpleValues()
        {
            var obj1 = new ModelObject { Transform = { Position = new Vector3(2, 2, 0) } };
            var obj2 = new ModelObject { Transform = { Position = new Vector3(2, 1, 0) } };
            var force = new DummyAttractionForce(obj2, obj1);
            obj1.Mass = Mass.FromKilograms(1);
            obj1.AddForce(force);


            var speedVariationVector = obj1.ComputeSpeedVariationVector(TimeSpan.FromSeconds(1));

            speedVariationVector.X.Should().BeApproximately(0, FloatingPointTolerance);
            speedVariationVector.Y.Should().BeApproximately(-2, FloatingPointTolerance);
            speedVariationVector.Z.Should().BeApproximately(0, FloatingPointTolerance);

        }

        [TestMethod]
        public void ComputeSpeedVariationVector_Tridimensional()
        {
            var obj1 = new ModelObject { Transform = { Position = new Vector3(5, 8, 2) } };
            var obj2 = new ModelObject { Transform = { Position = new Vector3(4, 4, 6) } };
            var force = new DummyAttractionForce(obj2, obj1);
            obj1.Mass = Mass.FromKilograms(3);
            obj1.AddForce(force);

            var speedVariationVector = obj1.ComputeSpeedVariationVector(TimeSpan.FromSeconds(2));

            speedVariationVector.X.Should().BeApproximately((float)(-4.0 / (3.0 * Math.Sqrt(33))), FloatingPointTolerance);
            speedVariationVector.Y.Should().BeApproximately((float)(-16.0 / (3.0 * Math.Sqrt(33))), FloatingPointTolerance);
            speedVariationVector.Z.Should().BeApproximately((float)(16.0 / (3.0 * Math.Sqrt(33))), FloatingPointTolerance);

        }

        [TestMethod]
        public void ComputeSpeedVariationVector_OppositeForces()
        {
            var obj1 = new ModelObject { Transform = { Position = new Vector3(5, 8, 2) }, Mass = Mass.FromKilograms(3) };
            var obj2 = new ModelObject { Transform = { Position = new Vector3(4, 4, 6) } };
            var attraction = new DummyAttractionForce(obj2, obj1);
            var repulsion = new DummyRepulsionForce(obj2, obj1);

            obj1.AddForce(attraction);
            obj1.AddForce(repulsion);

            var speedVariationVector = obj1.ComputeSpeedVariationVector(TimeSpan.FromSeconds(2));

            speedVariationVector.X.Should().BeApproximately(0, FloatingPointTolerance);
            speedVariationVector.Y.Should().BeApproximately(0, FloatingPointTolerance);
            speedVariationVector.Z.Should().BeApproximately(0, FloatingPointTolerance);
        }

        [TestMethod]
        public void UpdatePosition_NoMovement()
        {
            _object.UpdatePosition(TimeSpan.FromSeconds(1));

            _object.Transform.Position.X.Should().BeApproximately(0, FloatingPointTolerance);
        }

        [TestMethod]
        public void UpdatePosition_NoForces()
        {
            var motion = new Motion { DirectionNormalized = new Vector3(1, 0, 0), Speed = Speed.FromMetersPerSecond(10) };
            _object.Motion = motion;

            _object.UpdatePosition(TimeSpan.FromSeconds(1));

            _object.Transform.Position.X.Should().BeApproximately(10, FloatingPointTolerance);
        }
    }

    class DummyAttractionForce : Force
    {
        public DummyAttractionForce(ModelObject appliedBy, ModelObject appliedTo) : base(appliedBy, appliedTo) { }

        public override ForceAction ForceAction { get; } = ForceAction.Attraction;
        public override UnitsNet.Force Magnitude => UnitsNet.Force.FromNewtons(2);
    }

    class DummyRepulsionForce : Force
    {
        public DummyRepulsionForce(ModelObject appliedBy, ModelObject appliedTo) : base(appliedBy, appliedTo) { }

        public override ForceAction ForceAction { get; } = ForceAction.Repulsion;
        public override UnitsNet.Force Magnitude => UnitsNet.Force.FromNewtons(2);
    }
}
