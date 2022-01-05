namespace finance_api.Models
{
    public class TransactionCategoryOption
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
    }
}
