using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static PokeBattler.Client.Services.ServerService;

namespace PokeBattler.Client.Services
{
    public interface IServerService { }

    public class ServerService : IServerService
    {
        //readonly IServerConnection connection;
        object service;
        MethodInfo method;

        public ServerService(ITrainersService trainerService)
        {
            //this.connection = connection;
            //SubscribeToHubEvents(trainerService);
        }

        // FIXME: or remove this 
        //void SubscribeToHubEvents<T>(T service)
        //{
        //    var type = typeof(T);
        //    var methods = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(ServerSubscriptionAttribute), false).Length > 0);
        //    foreach (var method in methods)
        //    {
        //        if (method.ReturnType != typeof(Task))
        //        {
        //            throw new InvalidOperationException($"{type.Name}.{method.Name} must return a {typeof(Task).Name} to subscribe to Server remote procedure calls");
        //        }
        //        var parameterTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
        //        var attribute = method.GetCustomAttribute<ServerSubscriptionAttribute>();
        //        //connection.On(attribute.MethodName, (object[] args) => method.Invoke(service, args));
        //    }
        //}

        //public delegate void ParamsAction(params object[] oArgs);

        //ParamsAction Wrapper(params object[] args)
        //{
        //    return (object[] args) => method.Invoke(service, args);
        //}
    }
}
