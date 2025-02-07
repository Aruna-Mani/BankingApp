using BankingApp.Data.Interfaces;
using BankingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Data.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetAccountAsync(int accountId)
        {
            return await _context.Accounts
                                 .FirstOrDefaultAsync(a => a.Id == accountId);
        }
    }
}
