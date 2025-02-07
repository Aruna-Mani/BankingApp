using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class FundTransfer
    {
        public int Id { get; set; }  // Primary Key

        [Required(ErrorMessage = "Sender Account is required.")]
        public string SenderAccount { get; set; }

        [Required(ErrorMessage = "Receiver Account is required.")]
        public string ReceiverAccount { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public DateTime TransferDate { get; set; } = DateTime.Now;
    }

}
