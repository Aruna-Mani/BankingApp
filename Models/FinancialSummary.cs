namespace BankingApp.Models
{
    public class FinancialSummary
    {
        public decimal TotalDeposits { get; set; }
        public decimal TotalWithdrawals { get; set; }
        public decimal Balance { get; set; }
        public int TransactionCount { get; set; }
    }
}
