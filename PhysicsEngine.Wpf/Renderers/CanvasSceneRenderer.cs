using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PhysicsEngine.Core.Engine;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Space;

namespace PhysicsEngine.Wpf.Renderers
{
    public class CanvasSceneRenderer : ISceneRenderer
    {
        private readonly Dictionary<ModelObject, UIElement> _objects = new();

        private readonly Canvas _canvas;

        public CanvasSceneRenderer(Canvas canvas)
        {
            _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
        }

        public void RenderScene(WorldSpace space)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                foreach (var modelObject in space.GetModelObjects())
                {
                    if (!_objects.ContainsKey(modelObject))
                    {
                        _objects[modelObject] = CreateModelObjectRepresentation();
                    }

                    Canvas.SetLeft(_objects[modelObject], modelObject.Transform.Position.X);
                    Canvas.SetTop(_objects[modelObject], modelObject.Transform.Position.Y);
                }
            });
        }

        private UIElement CreateModelObjectRepresentation()
        {
            var ellipse = new Ellipse
            {
                Fill = new SolidColorBrush(Colors.Red),
                Width = 20,
                Height = 20
            };

            _canvas.Children.Add(ellipse);

            return ellipse;
        }
    }
}
