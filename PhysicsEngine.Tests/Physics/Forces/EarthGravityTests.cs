using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Physics.Forces;
using PhysicsEngine.Core.Space;
using UnitsNet;
using UnitsNet.Units;

namespace PhysicsEngine.Tests.Physics.Forces
{
    [TestClass]
    public class EarthGravityTests
    {
        private const double FloatingPointTolerance = 1e-5;

        private readonly ModelObject _earth = new ModelObject();
        private readonly ModelObject _object = new ModelObject { Mass = new Mass(10, MassUnit.Kilogram) };

        [TestMethod]
        public void Magnitude()
        {
            var gravity = new EarthGravity(_earth, _object);

            gravity.Magnitude.Newtons.Should().BeApproximately(98.1d, FloatingPointTolerance);
        }
    }
}
