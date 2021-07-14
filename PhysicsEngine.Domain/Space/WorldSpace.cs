using System.Collections.Generic;
using System.Linq;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Space.Transformations;

namespace PhysicsEngine.Core.Space
{
    // Y        Z
    // |       /
    // |     /
    // |   /
    // | /
    // |__________ X

    /// <summary>
    /// Space reference for the world.
    /// Axises are in meters : one unit on each axis is equal to one meter.
    /// TODO : Set the length unit for axis scale
    /// </summary>
    public class WorldSpace
    {
        private readonly IDictionary<ModelObject, Transform> _modelObjects = new Dictionary<ModelObject, Transform>();

        public void AddObject(ModelObject mObject)
        {
            _modelObjects.Add(mObject, new Transform());
        }

        public IEnumerable<ModelObject> GetModelObjects()
        {
            return _modelObjects.Select(pair => pair.Key);
        }
    }
}
