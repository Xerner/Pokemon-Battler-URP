using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Services;
using System.Threading.Tasks;
using UnityEngine;

namespace PokeBattler.Client.Controllers.HubConnections
{
    public interface ITrainerHubConnection : IHubConnection
    {
        Task UpdateTrainerReady(ulong trainerID, bool ready);
    }

    public class TrainerHubConnection : ITrainerHubConnection
    {
        public HubConnection HubConnection { get; private set; }

        public TrainerHubConnection(ISignalRService signalr)
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl(signalr.GetHubApiPath("Trainers"))
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task UpdateTrainerReady(ulong trainerID, bool ready)
        {
            await HubConnection.InvokeAsync("UpdateTrainerReady", trainerID, ready);
            Debug.Log($"Trainer {trainerID} set ready to {ready}");
        }
    }
}
