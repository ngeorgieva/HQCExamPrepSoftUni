namespace BangaloreUniversityLearningSystem.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Controllers;
    using Data;
    using Interfaces;
    using Models;

    public class BangaloreUniversityLearningSysytemEngine : IEngine
    {
        public void Run()
        {
            var dataBase = new BangaloreUniversityData();
            User user = null;
            while (true)
            {
                string routeUrl = Console.ReadLine();
                if (routeUrl == null)
                {
                    break;
                }

                var route = new Route(routeUrl);

                var controllerType = Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .FirstOrDefault(type => type.Name == route.ControllerName);

                var controller = Activator.CreateInstance(controllerType, dataBase, user) as Controller;

                var action = controllerType.GetMethod(route.ActionName);
                object[] parameters = MapParameters(route, action);
                string viewResult = string.Empty;

                try
                {
                    var view = action.Invoke(controller, parameters) as IView;
                    viewResult = view.Display();
                    //Console.WriteLine(view.Display());
                    user = controller.User;
                }
                catch (Exception ex)
                {
                    viewResult = ex.InnerException.Message;
                }

                Console.WriteLine(viewResult);
            }
        }

        private static object[] MapParameters(Route route, MethodInfo action)
        {
            return action
                .GetParameters()
                .Select<ParameterInfo, object>(
                    p =>
                        {
                            if (p.ParameterType == typeof(int))
                            {
                                return int.Parse(route.Parameters[p.Name]);
                            }
                            else
                            {
                                return route.Parameters[p.Name];
                            }
                        }).ToArray();
        }
    }
}
