using System;
using System.Numerics;
using PhysicsEngine.Core.Engine;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Space;
using UnitsNet;

namespace PhysicsEngine.Wpf.Configuration
{
    public class DummyConfigurator : IEngineConfigurator
    {
        public void Setup(WorldSpace space)
        {
            var mObj = new ModelObject();
            mObj.Transform.Position = new Vector3(25,25,0);
            mObj.Motion.Speed = Speed.FromCentimetersPerSecond(50);
            mObj.Motion.Direction = new Vector3(1,1,0);

            space.AddObject(mObj);
        }
    }
}
