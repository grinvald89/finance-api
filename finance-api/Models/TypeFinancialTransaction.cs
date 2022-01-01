using System.ComponentModel.DataAnnotations;

namespace finance_api.Data
{
    public class TypeFinancialTransaction
    {
        public TypeFinancialTransaction(string Name)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
