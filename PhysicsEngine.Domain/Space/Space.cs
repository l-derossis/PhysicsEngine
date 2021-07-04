using System.Collections.Generic;
using System.Diagnostics;
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

    public class Space
    {
        private readonly IDictionary<ModelObject, GeometricalState> _modelObjects = new Dictionary<ModelObject, GeometricalState>();

        public void AddObject(ModelObject mObject)
        {
            _modelObjects.Add(mObject, new GeometricalState());
        }

        public (ModelObject modelObject, GeometricalState state) GetObject(string id)
        {
            var pair = _modelObjects.FirstOrDefault(m => m.Key.Id == id);

            return !pair.Equals(default(KeyValuePair<ModelObject, GeometricalState>)) ? (pair.Key, pair.Value) : (null, null);
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
