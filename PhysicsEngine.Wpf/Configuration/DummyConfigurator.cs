using System;
using System.Numerics;
using PhysicsEngine.Core.Engine;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Physics.Forces;
using PhysicsEngine.Core.Space;
using UnitsNet;

namespace PhysicsEngine.Wpf.Configuration
{
    public class DummyConfigurator : IEngineConfigurator
    {
        public void Setup(WorldSpace space)
        {
            var mObj1 = new ModelObject();
            mObj1.Transform.Position = new Vector3(100,200,0);
            mObj1.Mass = Mass.FromKilograms(1);
            mObj1.Motion.Speed = Speed.FromMetersPerSecond(20);
            mObj1.Motion.DirectionNormalized = new Vector3(-1, 1, 0);

            var mObj2 = new ModelObject();
            mObj2.Transform.Position = new Vector3(350, 200, 0);


            var gravity1 = new EarthGravity(mObj1, mObj1);
            mObj1.AddForce(gravity1);


            space.AddObject(mObj1);
            space.AddObject(mObj2);
        }
    }
}
