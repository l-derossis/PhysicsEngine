namespace PhysicsEngine.Core.Space.Transformations
{
    public class Translation : ITransformation
    {
        public Translation(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public GeometricalState Transform(GeometricalState initialState)
        {
            var newPosition = new Position(
                initialState.Position.X + X,
                initialState.Position.Y + Y,
                initialState.Position.Z + Z
                
            );

            return new GeometricalState
            {
                Position = newPosition
            };
        }
    }
}
