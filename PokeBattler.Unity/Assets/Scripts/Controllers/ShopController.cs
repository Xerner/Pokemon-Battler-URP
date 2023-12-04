using Microsoft.AspNetCore.SignalR.Client;
using PokeBattler.Client.Services;
using PokeBattler.Common.Models;
using System;

namespace PokeBattler.Client.Controllers
{
    public class ShopController
    {
        readonly HubConnection connection;
        readonly IClientService clientService;

        public ShopController(HubConnection connection, IClientService clientService)
        {
            this.connection = connection;
            this.clientService = clientService;
        }

        public void RequestToBuyPokemon(Pokemon pokemon)
        {
            connection.InvokeAsync<(Guid, Pokemon)>("BuyPokemon", (clientService.Account.Id, pokemon));
        }
    }
}
