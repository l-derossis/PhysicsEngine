using System;
using System.Numerics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Physics.Forces;

namespace PhysicsEngine.Tests.Physics.Forces
{
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
        public override UnitsNet.Force Magnitude => UnitsNet.Force.FromNewtons(3);
    }

    [TestClass]
    public class ForceTests
    {
        const float FloatingPointTolerance = 1e-3f;

        [TestMethod]
        public void ComputeVector_Attraction()
        {
            var obj1 = new ModelObject { Transform = { Position = new Vector3(1, 2, 3) } };
            var obj2 = new ModelObject { Transform = { Position = new Vector3(10, 5, 8) } };

            var force = new DummyAttractionForce(obj1, obj2);

            var vector = force.ComputeVector();

            vector.X.Should().BeApproximately((float)(-18 / Math.Sqrt(115)), FloatingPointTolerance);
            vector.Y.Should().BeApproximately((float)(-6 / Math.Sqrt(115)), FloatingPointTolerance);
            vector.Z.Should().BeApproximately((float)(-10 / Math.Sqrt(115)), FloatingPointTolerance);
        }

        [TestMethod]
        public void ComputeVector_Repulsion()
        {
            var obj1 = new ModelObject { Transform = { Position = new Vector3(6, 4, 1) } };
            var obj2 = new ModelObject { Transform = { Position = new Vector3(15, 3, 7) } };
            var force = new DummyRepulsionForce(obj1, obj2);

            var vector = force.ComputeVector();

            vector.X.Should().BeApproximately((float)(27 / Math.Sqrt(118)), FloatingPointTolerance);
            vector.Y.Should().BeApproximately((float)(-3 / Math.Sqrt(118)), FloatingPointTolerance);
            vector.Z.Should().BeApproximately((float)(18 / Math.Sqrt(118)), FloatingPointTolerance);
        }

        [TestMethod]
        public void ComputeVector_SameObject()
        {
            var obj1 = new ModelObject { Transform = { Position = new Vector3(6, 4, 1) } };
            var force = new DummyRepulsionForce(obj1, obj1);

            var vector = force.ComputeVector();

            vector.X.Should().BeApproximately(0, FloatingPointTolerance);
            vector.Y.Should().BeApproximately(0, FloatingPointTolerance);
            vector.Z.Should().BeApproximately(0, FloatingPointTolerance);
        }
    }
}
