namespace finance_api.Models
{
    public class TransactionSubCategoryFirstOption
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid SubCategoryId { get; set; }
        public Guid SubCategoryFirstOptionId { get; set; }
        public bool Deleted { get; set; }
    }
}