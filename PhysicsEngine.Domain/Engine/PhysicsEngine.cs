using System;
using System.Threading.Tasks;
using PhysicsEngine.Core.Space;

namespace PhysicsEngine.Core.Engine
{
    /// <summary>
    /// The PhysicsEngine orchestrates the evolution of the simulation based on time evolution.
    /// </summary>
    public class PhysicsEngine
    {
        public ISceneRenderer SceneRenderer { get; }
        public IEngineConfigurator Configurator { get; }

        private readonly WorldSpace _worldSpace = new();

        public PhysicsEngine(ISceneRenderer renderer, IEngineConfigurator configurator = null)
        {
            SceneRenderer = renderer ?? throw new ArgumentNullException(nameof(renderer));

            if (configurator != null)
            {
                Configurator = configurator;
            }
        }

        // TARGET ALGORITHM:
        // Loop (for every time delta)
        // Loop (for every object)
        // Compute forces on all objects
        // Compute resulting force
        // Apply resulting force on object's speed vector
        // Loop (for every object)
        // Compute new position of object after time delta
        // Render scene
        public async Task Run()
        {
            Configurator?.Setup(_worldSpace);

            var timeDelta = TimeSpan.FromMilliseconds(30);

            // Loop (for every time delta)
            while (true)
            {
                var delayTask = Task.Delay(timeDelta);

                // Loop (for every object)
                foreach (var modelObject in _worldSpace.GetModelObjects())
                {
                    // Compute new position of object after time delta
                    modelObject.UpdatePosition(timeDelta);
                }

                SceneRenderer.RenderScene(_worldSpace);

                await delayTask;
            }
        }
    }
}
