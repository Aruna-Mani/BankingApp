using BankingApp.Models;

namespace BankingApp.Data.Interfaces
{
    public interface IAccountService
    {
        Task<Account> GetAccountAsync(int accountId);
    }
}
