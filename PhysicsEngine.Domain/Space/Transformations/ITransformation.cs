namespace PhysicsEngine.Core.Space.Transformations
{
    public interface ITransformation
    {
        Transform Transform(Transform initialState);
    }
}
