//using Microsoft.AspNetCore.SignalR.Client;
//using System;
//using System.Linq;
//using System.Reflection;
//using System.Threading.Tasks;

//namespace PokeBattler.Client.Controllers.HubConnections;
//{
//    public interface IApiConnection
//    {
//        Task Send(string methodName);
//        Task Send(string methodName, );
//    }

//    public class SignalRController
//    {
//        public const string ApiPath = "https://localhost:5000/Hubs";
//        public const string HubName = "Hub";
//        public const string HubApiPath = ApiPath + "/" + HubName;

//        public HubConnection HubConnection { get; private set; }

//        public SignalRController(ITrainersService trainerService)
//        {
//            HubConnection = new HubConnectionBuilder()
//                .WithUrl(HubApiPath)
//                .WithAutomaticReconnect()
//                .Build();


//        }

//        void SubscribeToHubEvents<T>()
//        {
//            var type = typeof(T);
//            var methods = type.GetMethods()
//                            .Where(m => m.GetCustomAttributes(typeof(ServerSubscriptionAttribute), false).Length > 0)
//                            .ToArray();
//            foreach (MethodInfo method in methods)
//            {
//                Console.WriteLine(method.Name);
//            }
//        }
//    }
//}
