namespace PhysicsEngine.Core.Space.Transformations
{
    public interface ITransformation
    {
        GeometricalState Transform(GeometricalState initialState);
    }
}
