using System;
using PokeBattler.Common.Models;

namespace PokeBattler.Client.Services
{
    public interface IClientService
    {
        public Guid ClientID { get; set; }
        public Account Account { get; set; }
    }

    public class ClientService : IClientService
    {
        /// <summary>The local clients Id</summary>
        public Guid ClientID { get; set; }
        public Account Account { get; set; }

        public ClientService(Account account)
        {
            ClientID = Guid.NewGuid();
            Account = account;
        }
    }
}
