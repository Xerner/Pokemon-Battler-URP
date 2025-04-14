using AutoChess.Contracts.Options;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoChess.Client.SignalR
{
    public class HubConnectionManager
    {
        private readonly ILogger<AutoChessServer> Logger;
        private readonly IOptions<ServerOptions> ServerOptions;

        public HubConnection Connection { get; private set; } = null;

        public HubConnectionManager(ILogger<AutoChessServer> logger, IOptions<ServerOptions> serverOptions)
        {
            this.Logger = logger;
            this.ServerOptions = serverOptions;
        }

        public HubConnection BuildConnection(Func<Exception, Task> onConnectionClosed)
        {
            var hubUrl = ServerOptions.Value.Urls[0] + "/" + ServerOptions.Value.HubName;
            Connection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();
            Connection.Closed += onConnectionClosed;
            return Connection;
        }

        public async Task OnErrorWaitABitAndRestartConnection(Exception error)
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            Logger.LogInformation("Attempting to reconnect");
            await StartAsync();
        }

        public async Task StartAsync()
        {
            if (Connection is null)
            {
                Logger.LogInformation("Connection is null");
                return;
            }
            await Connection.StartAsync();
            Logger.LogInformation("Reconnected");
        }
    }
}
