using System.ComponentModel.DataAnnotations;

namespace finance_api.Models
{
    public class TransactionType
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}