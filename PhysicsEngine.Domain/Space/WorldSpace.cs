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
    /// Axises are in centimeter : one unit on each axis is equal to one centimeter.
    /// TODO : Set the length unit for axis scale
    /// </summary>
    public class WorldSpace
    {
        private readonly IDictionary<ModelObject, Transform> _modelObjects = new Dictionary<ModelObject, Transform>();

        public void AddObject(ModelObject mObject)
        {
            _modelObjects.Add(mObject, new Transform());
        }

        public (ModelObject modelObject, Transform state) GetObject(string id)
        {
            var pair = _modelObjects.FirstOrDefault(m => m.Key.Id == id);

            return !pair.Equals(default(KeyValuePair<ModelObject, Transform>)) ? (pair.Key, pair.Value) : (null, null);
        }

        public IEnumerable<ModelObject> GetModelObjects()
        {
            return _modelObjects.Select(pair => pair.Key);
        }

        public void TransformObject(ModelObject modelObject, ITransformation transformation)
        {
            var state = _modelObjects[modelObject];
            var updatedState = transformation.Transform(state);
            _modelObjects[modelObject] = updatedState;
        }
    }
}
