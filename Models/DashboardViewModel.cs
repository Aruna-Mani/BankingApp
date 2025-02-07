namespace BankingApp.Models
{
    public class DashboardViewModel
    {
        public Account Account { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public FinancialSummary FinancialSummary { get; set; }
    }
}
