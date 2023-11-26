using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Client.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeBattler.Services
{
    public interface ISignalRService
    {
        Uri GetHubApiPath(string HubName);
    }

    public class SignalRService : ISignalRService
    {
        private static readonly string ApiPath = "https://localhost:5387/Hubs";

        public Uri GetHubApiPath(string HubName)
        {
            return new Uri($"{ApiPath}/{HubName}");
        }
    }
}
