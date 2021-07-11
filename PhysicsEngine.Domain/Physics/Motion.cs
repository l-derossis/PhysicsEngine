using System;
using System.Numerics;
using UnitsNet;

namespace PhysicsEngine.Core.Physics
{
    public class Motion
    {
        public Speed Speed { get; set; } = Speed.Zero;
        
        public Vector3 Direction { get; set; }
    }
}
