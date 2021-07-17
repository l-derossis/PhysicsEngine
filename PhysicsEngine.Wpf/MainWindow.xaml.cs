using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Space;
using PhysicsEngine.Core.Space.Transformations;
using PhysicsEngine.Wpf.Configuration;
using PhysicsEngine.Wpf.Renderers;

namespace PhysicsEngine.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var engine = new Core.Engine.PhysicsEngine(new CanvasSceneRenderer(MainCanvas), new PendulumConfigurator());

            Task.Run(async () =>
            {
                await engine.Run();
            });
        }
    }
}
