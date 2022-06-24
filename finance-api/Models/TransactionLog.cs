using finance_api.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace finance_api.Models
{
    public class TransactionLog
    {
        public Guid Id { get; set; }
        
        // Дата
        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public string TransactionJson{ get; set; }

        public string Type{ get; set; }

        public bool Deleted { get; set; }
    }
}