namespace finance_api.Models
{
    public class TransactionCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DirectionId { get; set; }
        public bool Deleted { get; set; }
    }
}