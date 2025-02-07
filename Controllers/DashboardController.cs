using BankingApp.Data.Interfaces;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        public DashboardController(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }
        //[Authorize]
        public async Task<IActionResult> Index(int? accountId)
        {
            if (!accountId.HasValue)
            {
                // Default to the first account or the logged-in user's account
                accountId = 1; // or logic to fetch the default account ID
            }
            // Get account details
            var account = await _accountService.GetAccountAsync(accountId.Value);

            // Get recent transactions for the account
            var transactions = await _transactionService.GetRecentTransactionsAsync(accountId.Value);

            // Get financial summary for the account
            var financialSummary = await _transactionService.GetFinancialSummaryAsync(accountId.Value);

            // Create ViewModel
            var viewModel = new DashboardViewModel
            {
                Account = account,
                Transactions = transactions,
                FinancialSummary = financialSummary
            };

            // Return the view with the populated ViewModel
            return View(viewModel);
        }
    }
}
