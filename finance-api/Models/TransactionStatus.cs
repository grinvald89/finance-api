using System.ComponentModel.DataAnnotations;

namespace finance_api.Data
{
    public class TransactionStatus
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}