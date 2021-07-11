using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserDataTasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Space;
using PhysicsEngine.Core.Space.Transformations;

namespace PhysicsEngine.WinUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Space _space = new Space();
        private Dictionary<ModelObject, Ellipse> _objects = new Dictionary<ModelObject, Ellipse>();

        public MainPage()
        {
            InitializeComponent();
            InitSpace();
            _ = RunPeriodicSceneRefresh();
        }

        private void InitSpace()
        {
            var mObj = CreateModelObject();
            _space.AddObject(mObj);
            _ = RunPeriodicMove(mObj);
        }

        private async Task RunPeriodicSceneRefresh()
        {
            while (true)
            {
                await Task.Delay(10);
                RefreshScene();
            }
        }

        private void RefreshScene()
        {
            var models = _space.GetModelObjects();

            foreach (var model in models)
            {
                var (_, geometricalState) = _space.GetObject(model.Id);
                var ellipse = _objects[model];
                ellipse.Translation = new Vector3((float )geometricalState.Position.X, (float)geometricalState.Position.Y, 0);
            }

            MainCanvas.UpdateLayout();
        }

        private ModelObject CreateModelObject()
        {
            var mObj = new ModelObject();

            var ellipse = new Ellipse
            {
                Fill = new SolidColorBrush(Windows.UI.Colors.SteelBlue),
                Width = 20,
                Height = 20
            };

            _objects[mObj] = ellipse;
            MainCanvas.Children.Add(ellipse);

            return mObj;
        }

        private async Task RunPeriodicMove(ModelObject mObj)
        {
            while (true)
            {
                await Task.Delay(5);
                TranslateObject(mObj.Id, 1, 1, 1);
            }
        }

        private void TranslateObject(string objectId, int x, int y, int z)
        {
            var translation = new Translation(x, y, z);
            var mObj = _space.GetObject(objectId);
            _space.TransformObject(mObj.modelObject, translation);
        }
    }
}
