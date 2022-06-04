using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionsController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<Transaction> get()
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
                if (Guid.Equals(transaction.Id, Guid.Parse("038c0806-8d2e-433e-b77d-bbe04ff42a07")))
                {
                    var a = 1;
                }

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

            return transactions;
        }

        [HttpGet("{id}")]
        public Transaction? Get(Guid id)
        {
            // Todo: проверка на id и существование транзакции

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

            return transactions.Find(t => t.Id == id);
        }

        [HttpPut]
        public IActionResult Put(TransactionRequest request)
        {
            List<TransactionTag> transactionTags =
                _dbContext.TransactionTags.ToList()
                    .FindAll(t => !t.Deleted);

            Transaction transaction = new Transaction()
            {
                Category = GetCategoryById(request.CategoryId),
                Comment = request.Comment,
                Date = request.TransactionDate,
                Direction = GetDirectionById(request.DirectionId),
                Id = Guid.NewGuid(),
                Payer = GetPayerById(request.PayerId),
                Status = GetStatusById(request.StatusId),
                Summ = request.Summ,
                //Tags = GetTagsByIds(request.TagIds),
                Type = GetTypeById(request.TypeId)
            };

            string tagIds = "";
            if (request.TagIds != null)
            {
                for (int i = 0; i < request.TagIds.Length; i++)
                {
                    tagIds = tagIds + request.TagIds[i].ToString();

                    if (i + 1 < request.TagIds.Length)
                    {
                        tagIds = tagIds + ";";
                    }
                }
            }
            transaction.TagIds = tagIds;

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            if (request.TagIds != null)
            {
                request.TagIds.ToList().ForEach(tagId =>
                {
                    TransactionTag transactionTag = transactionTags.Find(tag => Guid.Equals(tag.Id, tagId));
                    if (transactionTag != null)
                    {
                        if (transaction.Tags == null)
                        {
                            transaction.Tags = new List<TransactionTag>();
                        }
                        transaction.Tags.Add(transactionTag);
                    }
                });
            }

            return Ok(transaction);
        }

        [HttpPost]
        public IActionResult Post(TransactionRequest request)
        {
            // Todo: проверка на id и существование транзакции

            Transaction transaction =
                _dbContext.Transactions
                    .Include(t => t.Payer.FullName)
                    .ToList()
                    .FindAll(t => !t.Deleted)
                    .Find(t => Guid.Equals(t.Id, request.Id));

            List<TransactionTag> transactionTags =
                _dbContext.TransactionTags.ToList()
                    .FindAll(t => !t.Deleted);

            transaction.Category = GetCategoryById(request.CategoryId);
            transaction.Comment = request.Comment;
            transaction.Date = request.TransactionDate;
            transaction.Direction = GetDirectionById(request.DirectionId);
            transaction.Payer = GetPayerById(request.PayerId);
            transaction.Status = GetStatusById(request.StatusId);
            transaction.Summ = request.Summ;

            string tagIds = "";
            if (request.TagIds != null)
            {
                for (int i = 0; i < request.TagIds.Length; i++)
                {
                    tagIds = tagIds + request.TagIds[i].ToString();

                    if (i + 1 < request.TagIds.Length)
                    {
                        tagIds = tagIds + ";";
                    }
                }
            }

            transaction.TagIds = tagIds;
            transaction.Type = GetTypeById(request.TypeId);

            _dbContext.SaveChanges();

            if (request.TagIds != null)
            {
                request.TagIds.ToList().ForEach(tagId =>
                {
                    TransactionTag transactionTag = transactionTags.Find(tag => Guid.Equals(tag.Id, tagId));
                    if (transactionTag != null)
                    {
                        if (transaction.Tags == null)
                        {
                            transaction.Tags = new List<TransactionTag>();
                        }
                        transaction.Tags.Add(transactionTag);
                    }
                });
            }

            return Ok(transaction);
        }

        [HttpDelete]
        public IActionResult Delete(TransactionIdRequest request)
        {
            // Todo: проверка на id и существование транзакции

            Transaction transaction =
                _dbContext.Transactions
                    .Include(t => t.Payer.FullName)
                    .ToList()
                    .Find(t => Guid.Equals(t.Id, request.Id));

            transaction.Deleted = true;
            _dbContext.SaveChanges();

            return Ok(transaction);
        }

        private TransactionDirection? GetDirectionById(Guid id)
        {
            List<TransactionDirection> directions =
                _dbContext.TransactionDirections
                .ToList()
                .FindAll(t => !t.Deleted);

            return directions.Find(c => c.Id == id);
        }

        private TransactionCategory? GetCategoryById(Guid id)
        {
            List<TransactionCategory> categories =
                _dbContext.TransactionCategories
                    .ToList()
                    .FindAll(t => !t.Deleted);

            return categories.Find(c => c.Id == id);
        }

        private User? GetPayerById(Guid id)
        {
            List<User> users =
                _dbContext.Users
                    .Include(t => t.FullName)
                    .ToList()
                    .FindAll(t => !t.Deleted);

            return users.Find(c => c.Id == id);
        }

        private TransactionStatus? GetStatusById(Guid id)
        {
            List<TransactionStatus> statuses =
                _dbContext.TransactionStatuses
                    .ToList()
                    .FindAll(t => !t.Deleted);

            return statuses.Find(c => c.Id == id);
        }

        private List<TransactionTag>? GetTagsByIds(Guid[]? ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return null;
            }

            List<TransactionTag> tags =
                _dbContext.TransactionTags
                    .ToList()
                    .FindAll(t => !t.Deleted);

            List<TransactionTag> result = tags.FindAll(c => ids.Contains(c.Id));

            return result;
        }

        private TransactionType? GetTypeById(Guid id)
        {
            List<TransactionType> types =
                _dbContext.TransactionTypes
                    .ToList()
                    .FindAll(t => !t.Deleted);

            return types.Find(c => c.Id == id);
        }
    }

    public class TransactionFilter
    {
        public Guid[] categoryIds { get; set; }
        public Guid[] directionIds { get; set; }
        public Guid[] payerIds { get; set; }
        public TransactionFilterPeriod period { get; set; }
        public Guid[] statusIds { get; set; }
        public Guid[] tagIds { get; set; }
        public Guid[] typeIds { get; set; }
    }

    public class TransactionFilterPeriod
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set;}
    }

    public class TransactionRequest
    {
        public Guid CategoryId { get; set; }
        public string Comment { get; set; }
        public Guid DirectionId { get; set; }
        public Guid Id { get; set; }
        public Guid PayerId { get; set; }
        public Guid StatusId { get; set; }
        public float Summ { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid[] TagIds { get; set; }
        public Guid TypeId { get; set; }
    }

    public class TransactionIdRequest
    {
        public Guid Id { get; set; }
    }
}
