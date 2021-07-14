using System;
using PhysicsEngine.Core.Engine;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Space;

namespace PhysicsEngine.Wpf.Configuration
{
    /// <summary>
    /// Creates a scene where an object attracts another one with its gravitational attraction
    /// </summary>
    public class GravitySampleConfigurator : IEngineConfigurator
    {
        public void Setup(WorldSpace space)
        {
            var attractive = new ModelObject();
        }
    }
}
