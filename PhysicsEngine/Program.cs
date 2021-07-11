using System;
using System.Threading.Tasks;
using PhysicsEngine.Core.Model;
using PhysicsEngine.Core.Space;
using PhysicsEngine.Core.Space.Transformations;

namespace PhysicsEngine.Cli
{
    class Program
    {
        private static readonly WorldSpace WorldSpace = new WorldSpace();

        static void Main(string[] args)
        {
            var argument = "";
            while (argument != "exit")
            {
                argument = Console.ReadLine();

                if (string.IsNullOrEmpty(argument))
                {
                    continue;
                }

                var arguments = argument.Split(" ");

                if (arguments[0] == "add")
                {
                    var mObj = new ModelObject();
                    WorldSpace.AddObject(mObj);
                    //_ = RunPeriodicMove(mObj); // For debug purposes only
                }
                else if (arguments[0] == "ls")
                {
                    if (arguments.Length == 1)
                    {
                        var objects = WorldSpace.GetModelObjects();
                        foreach (var modelObject in objects)
                        {
                            Console.WriteLine(modelObject.Id);
                        }
                    }
                    else
                    {
                        var id = arguments[1];
                        var obj = WorldSpace.GetObject(id);
                        Console.WriteLine($"{obj.modelObject.Id}: {obj.state}");
                    }
                }
                else if (arguments[0] == "trans")
                {
                    var id = arguments[1];
                    var x = int.Parse(arguments[2]);
                    var y = int.Parse(arguments[3]);
                    var z = int.Parse(arguments[4]);

                    TranslateObject(id, x, y, z);
                }
            }
        }

        private static async Task RunPeriodicMove(ModelObject mObj)
        {
            while (true)
            {
                await Task.Delay(500);
                TranslateObject(mObj.Id, 1, 2, 3);
            }
        }

        private static void TranslateObject(string objectId, int x, int y, int z)
        {
            var translation = new Translation(x, y, z);
            var mObj = WorldSpace.GetObject(objectId);
            WorldSpace.TransformObject(mObj.modelObject, translation);
        }
    }
}
