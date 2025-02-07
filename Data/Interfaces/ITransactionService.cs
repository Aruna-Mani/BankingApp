using BankingApp.Models;

namespace BankingApp.Data.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int accountId, int topCount = 5);
        Task<FinancialSummary> GetFinancialSummaryAsync(int accountId);
    }
}
