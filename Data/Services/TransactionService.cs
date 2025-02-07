using BankingApp.Data.Interfaces;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Data.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(int accountId, int topCount = 5)
        {
            return await _context.Transactions
                                 .Where(t => t.AccountId == accountId)
                                 .OrderByDescending(t => t.Date)
                                 .Take(topCount)
                                 .ToListAsync();
        }

        public async Task<FinancialSummary> GetFinancialSummaryAsync(int accountId)
        {
            var transactions = await _context.Transactions
                                              .Where(t => t.AccountId == accountId)
                                              .ToListAsync();

            var summary = new FinancialSummary
            {
                TotalDeposits = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount),
                TotalWithdrawals = transactions.Where(t => t.Amount < 0).Sum(t => t.Amount),
                Balance = transactions.Sum(t => t.Amount),
                TransactionCount = transactions.Count
            };

            return summary;
        }
    }
}
