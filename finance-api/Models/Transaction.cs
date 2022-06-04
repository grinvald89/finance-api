using finance_api.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace finance_api.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        
        // Дата
        public DateTime Date { get; set; }

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

        // Ids тегов
        // Костыль, уберется после правильной установки связей многие-ко-многим
        public string TagIds { get; set; }

        // Комментарий
        public string Comment { get; set; }

        // Сумма
        public float Summ { get; set; }

        public bool Deleted { get; set; }
    }
}