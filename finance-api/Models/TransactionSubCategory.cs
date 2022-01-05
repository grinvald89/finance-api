namespace finance_api.Models
{
    public class TransactionSubCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CategoryOptionId { get; set; }
        public bool Deleted { get; set; }
    }
}