using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class BankAccount
    {
        public int Id { get; set; }  // Primary Key

        [Required(ErrorMessage = "Account Number is required.")]
        [StringLength(20, ErrorMessage = "Account Number cannot exceed 20 characters.")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "Account Holder Name is required.")]
        [StringLength(100, ErrorMessage = "Account Holder Name cannot exceed 100 characters.")]
        public string AccountHolder { get; set; }

        [Required(ErrorMessage = "Balance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative.")]
        public decimal Balance { get; set; }
    }
}
