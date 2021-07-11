using System.Numerics;

namespace PhysicsEngine.Core.Space
{
    public class Transform
    {
        public Vector3 Position;

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
