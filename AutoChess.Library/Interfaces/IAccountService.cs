using AutoChess.Contracts.Models;

namespace AutoChess.Library.Interfaces;

public interface IAccountService
{
    Account Create(Account account);
}
