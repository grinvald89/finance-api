namespace finance_api.Models
{
    public class TransactionTag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}