using System;
using System.Numerics;
using PhysicsEngine.Core.Engine;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Physics.Forces;
using PhysicsEngine.Core.Space;
using UnitsNet;

namespace PhysicsEngine.Wpf.Configuration
{
    public class PendulumConfigurator : IEngineConfigurator
    {
        public void Setup(WorldSpace space)
        {
            var mObj1 = new ModelObject();
            mObj1.Transform.Position = new Vector3(200, 100, 0);
            mObj1.Mass = Mass.FromKilograms(5);

            var mObj2 = new ModelObject();
            mObj2.Transform.Position = new Vector3(300, 100, 0);

            var earth = new ModelObject();
            earth.Transform.Position = new Vector3(100, 50000, 0);

            var pendulum = new PendulumForce(mObj2, mObj1);
            var gravity = new EarthGravity(earth, mObj1);

            mObj1.AddForce(pendulum);
            mObj1.AddForce(gravity);

            space.AddObject(mObj1);
            space.AddObject(mObj2);
        }
    }
}
