using finance_api.Data;

namespace finance_api.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        
        // Дата транзакции
        public DateTime TransactionDate { get; set; }

        // Плательщик
        public User Payer { get; set; }

        // Статус транзакции (исполнено/запланировано)
        public TransactionStatus Status { get; set; }

        // Тип транзакции (приход/расход)
        public TransactionType Type { get; set; }

        // Категория транзакции (личная/семейная/бизнес)
        public TransactionCategory Category { get; set; }

        // Опция для категории
        public TransactionCategoryOption CategoryOption { get; set; }

        // Подкатегория транзакции
        public TransactionSubCategory SubCategory { get; set; }

        // Опция для подкатегории первого уровня
        public TransactionSubCategoryFirstOption SubCategoryFirstOption { get; set; }

        // Опция для подкатегории второго уровня
        public TransactionSubCategorySecondOption SubCategorySecondOption { get; set; }
        public bool Deleted { get; set; }
    }
}