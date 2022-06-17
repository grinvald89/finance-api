using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-count")]
    [ApiController]
    public class TransactionCountController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionCountController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public int get()
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

            return transactions.Count;
        }
    }
}
