using AutoChess.Contracts.Models;
using AutoChess.Library.Interfaces;

namespace AutoChess.Library.Services;

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
