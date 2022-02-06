using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public List<Transaction> Get()
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
                .FindAll(t => t.Deleted == false); ;

            return transactions;
        }

        [HttpGet("{id}")]
        public Transaction? Get(Guid id)
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
                .FindAll(t => t.Deleted == false);

            return transactions.Find(t => t.Id == id);
        }

        [HttpPut]
        public IActionResult Put(TransactionRequest request)
        {
            Transaction transaction = new Transaction()
            {
                Category = GetCategoryById(request.CategoryId),
                Comment = request.Comment,
                Direction = GetDirectionById(request.DirectionId),
                Id = Guid.NewGuid(),
                Payer = GetPayerById(request.PayerId),
                Status = GetStatusById(request.StatusId),
                Tags = GetTagsByIds(request.TagIds),
                TransactionDate = request.TransactionDate,
                Type = GetTypeById(request.TypeId)
            };

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

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
                    .FindAll(t => t.Deleted == false)
                    .Find(t => Guid.Equals(t.Id, request.Id));

            transaction.Category = GetCategoryById(request.CategoryId);
            transaction.Comment = request.Comment;
            transaction.Direction = GetDirectionById(request.DirectionId);
            transaction.Payer = GetPayerById(request.PayerId);
            transaction.Status = GetStatusById(request.StatusId);
            transaction.Tags = GetTagsByIds(request.TagIds);
            transaction.TransactionDate = request.TransactionDate;
            transaction.Type = GetTypeById(request.TypeId);

            _dbContext.SaveChanges();

            return Ok(transaction);
        }

        [HttpDelete]
        public IActionResult Delete(TransactionId request)
        {
            // Todo: проверка на id и существование транзакции

            Transaction transaction =
                _dbContext.Transactions
                    .Include(t => t.Payer.FullName)
                    .ToList()
                    .FindAll(t => t.Deleted == false)
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
                .FindAll(t => t.Deleted == false);

            return directions.Find(c => c.Id == id);
        }

        private TransactionCategory? GetCategoryById(Guid id)
        {
            List<TransactionCategory> categories =
                _dbContext.TransactionCategories
                    .ToList()
                    .FindAll(t => t.Deleted == false);

            return categories.Find(c => c.Id == id);
        }

        private User? GetPayerById(Guid id)
        {
            List<User> users =
                _dbContext.Users
                    .Include(t => t.FullName)
                    .ToList()
                    .FindAll(t => t.Deleted == false);

            return users.Find(c => c.Id == id);
        }

        private TransactionStatus? GetStatusById(Guid id)
        {
            List<TransactionStatus> statuses =
                _dbContext.TransactionStatuses
                    .ToList()
                    .FindAll(t => t.Deleted == false);

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
                    .FindAll(t => t.Deleted == false);

            List<TransactionTag> result = tags.FindAll(c => ids.Contains(c.Id));

            return result;
        }

        private TransactionType? GetTypeById(Guid id)
        {
            List<TransactionType> types =
                _dbContext.TransactionTypes
                    .ToList()
                    .FindAll(t => t.Deleted == false);

            return types.Find(c => c.Id == id);
        }
    }

    public class TransactionRequest
    {
        public Guid CategoryId { get; set; }
        public string Comment { get; set; }
        public Guid DirectionId { get; set; }
        public Guid Id { get; set; }
        public Guid PayerId { get; set; }
        public Guid StatusId { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid[] TagIds { get; set; }
        public Guid TypeId { get; set; }
    }

    public class TransactionId
    {
        public Guid Id { get; set; }
    }
}
