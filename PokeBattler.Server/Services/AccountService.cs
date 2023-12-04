using PokeBattler.Common.Models;
using System;

namespace PokeBattler.Server.Services;

public interface IAccountService
{
    Account Create(Account account);
}

public class AccountService : IAccountService
{
    // TODO: Move Trainer class to Common and return that instead of Account
    public Account Create(Account account)
    {
        var id = Guid.NewGuid();
        account.Id = id;
        return account;
    }
}
