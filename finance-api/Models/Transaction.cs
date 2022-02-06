using finance_api.Data;

namespace finance_api.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        
        // Дата
        public DateTime TransactionDate { get; set; }

        // Плательщик
        public User Payer { get; set; }

        // Статус (исполнено/запланировано)
        public TransactionStatus Status { get; set; }

        // Тип (приход/расход)
        public TransactionType Type { get; set; }

        // Направление (личное/семейное/бизнес)
        public TransactionDirection Direction { get; set; }

        // Категория
        public TransactionCategory Category { get; set; }

        // Теги
        public List<TransactionTag> Tags { get; set; }

        // Опция для подкатегории первого уровня
        public string Comment { get; set; }

        public bool Deleted { get; set; }
    }
}