using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-total-amount")]
    [ApiController]
    public class TransactionTotalAmountController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionTotalAmountController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public TransactionTotalAmount get()
        {
            List<Transaction> transactions = _dbContext.Transactions
                .Include(t => t.Direction)
                .Include(t => t.Category)
                .Include(t => t.Payer)
                .Include(t => t.Payer.FullName)
                .Include(t => t.Status)
                .Include(t => t.Tags)
                .Include(t => t.Type)
                .ToList()
                .FindAll(t => !t.Deleted);

            List<TransactionTag> tags = _dbContext.TransactionTags
                .ToList()
                .FindAll(t => !t.Deleted);
            transactions.ForEach(transaction =>
            {
                transaction.Tags = new List<TransactionTag>();
            });

            transactions.ForEach(transaction =>
            {
                if (transaction.TagIds != null)
                {
                    List<string> tagIds = transaction.TagIds.Split(";").ToList();
                    if (tagIds.Count > 0)
                    {
                        tagIds.ForEach(tagId =>
                        {
                            if (tagId.Length > 0)
                            {
                                TransactionTag tag = tags.Find(tagItem => Guid.Equals(tagItem.Id, Guid.Parse(tagId)));
                                if (tag != null)
                                {
                                    transaction.Tags.Add(tag);
                                }
                            }
                        });
                    }

                }
            });

            string filterQuery = HttpContext.Request.Query["filter"];

            TransactionFilter filter = JsonSerializer.Deserialize<TransactionFilter>(filterQuery);

            // Todo: добавить проверку на валидность пагинатора

            if (filter != null)
            {
                transactions = transactions
                    .FindAll(transaction =>
                    {
                        bool isDateFromFilter = true;

                        if (filter.period != null)
                        {
                            isDateFromFilter = transaction.Date >= filter.period.startDate && transaction.Date <= filter.period.endDate;
                        }

                        bool isCategoryFromFilter = filter.categoryIds.Contains(transaction.Category.Id);
                        bool isDirectionFromFilter = filter.directionIds.Contains(transaction.Direction.Id);
                        bool isPayerFromFilter = filter.payerIds.Contains(transaction.Payer.Id);
                        bool isStatusFromFilter = filter.statusIds.Contains(transaction.Status.Id);
                        bool isTypeFromFilter = filter.typeIds.Contains(transaction.Type.Id);

                        bool isTagsFromFilter = filter.tagIds.Length == 0;
                        transaction.Tags.ForEach(transactionTag =>
                            {
                                if (filter.tagIds.Contains(transactionTag.Id)) {
                                    isTagsFromFilter = true;
                                }
                            });

                        return  isDateFromFilter &&
                                isCategoryFromFilter &&
                                isDirectionFromFilter &&
                                isPayerFromFilter &&
                                isStatusFromFilter &&
                                isTagsFromFilter &&
                                isTypeFromFilter;
                    });
            }

            return GetTotalAmount(transactions);
        }

        private TransactionTotalAmount GetTotalAmount(List<Transaction> transactions)
        {
            Guid refillTypeId = Guid.Parse("c8a5767f-b8b8-4b4b-a633-88656925d700");
            Guid expenseTypeId = Guid.Parse("d0d09615-9b0c-4b72-ab61-1810eada9ecd");

            float refill = 0;
            float expense = 0;

            transactions.ForEach(transaction =>
            {
                if (Guid.Equals(transaction.Type.Id, refillTypeId))
                {
                    refill += transaction.Summ;
                }

                if (Guid.Equals(transaction.Type.Id, expenseTypeId))
                {
                    expense += transaction.Summ;
                }
            });

            TransactionTotalAmount totalAmount = new TransactionTotalAmount()
            {
                Refill = refill,
                Expense = expense,
                Balance = refill - expense
            };

            return totalAmount;
        }
    }

    public class TransactionTotalAmount
    {
        public float Refill { get; set; }
        public float Expense { get; set; }
        public float Balance { get; set; }
    }
}
