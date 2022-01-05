using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-types")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionsController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public Transaction[] Get()
        {
            return _dbContext.Transactions.ToArray();
        }

        [HttpPost]
        public IActionResult Post(ITransactionCreateRequest request)
        {
            Transaction transaction = new Transaction()
            {
                Category = GetCategoryById(request.CategoryId),
                CategoryOption = GetCategoryOptionById(request.CategoryOptionId),
                Id = Guid.NewGuid(),
                Payer = GetPayerById(request.PayerId),
                Status = GetStatusById(request.StatusId),
                SubCategory = GetSubCategoryById(request.SubCategoryId),
                SubCategoryFirstOption = GetSubCategoryFirstOptionById(request.SubCategoryFirstOptionId),
                SubCategorySecondOption = GetSubCategorySecondOptionById(request.SubCategorySecondOptionId),
                TransactionDate = request.TransactionDate,
                Type = GetTypeById(request.TypeId)
            };

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return Ok(transaction);
        }

        private TransactionCategory? GetCategoryById(Guid id)
        {
            List<TransactionCategory> categories = _dbContext.TransactionCategories.ToList();

            return categories.Find(c => c.Id == id);
        }
        private TransactionCategoryOption? GetCategoryOptionById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            List<TransactionCategoryOption> categoryOptions = _dbContext.TransactionCategoryOptions.ToList();

            return categoryOptions.Find(c => c.Id == id);
        }
        private User? GetPayerById(Guid id)
        {
            List<User> users = _dbContext.Users.ToList();

            return users.Find(c => c.Id == id);
        }
        private TransactionStatus? GetStatusById(Guid id)
        {
            List<TransactionStatus> statuses = _dbContext.TransactionStatuses.ToList();

            return statuses.Find(c => c.Id == id);
        }
        private TransactionSubCategory? GetSubCategoryById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            List<TransactionSubCategory> subCategories = _dbContext.TransactionSubCategories.ToList();

            return subCategories.Find(c => c.Id == id);
        }
        private TransactionSubCategoryFirstOption? GetSubCategoryFirstOptionById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            List<TransactionSubCategoryFirstOption> subCategoryFirstOptions = _dbContext.TransactionSubCategoryFirstOptions.ToList();

            return subCategoryFirstOptions.Find(c => c.Id == id);
        }
        private TransactionSubCategorySecondOption? GetSubCategorySecondOptionById(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            List<TransactionSubCategorySecondOption> subCategorySecondOptions = _dbContext.TransactionSubCategorySecondOptions.ToList();

            return subCategorySecondOptions.Find(c => c.Id == id);
        }
        private TransactionType? GetTypeById(Guid id)
        {
            List<TransactionType> types = _dbContext.TransactionTypes.ToList();

            return types.Find(c => c.Id == id);
        }
    }

    public interface ITransactionCreateRequest
    {
        public Guid CategoryId { get; set; }
        public Guid? CategoryOptionId { get; set; }
        public Guid PayerId { get; set; }
        public Guid StatusId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? SubCategoryFirstOptionId { get; set; }
        public Guid? SubCategorySecondOptionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public Guid TypeId { get; set; }

    }
}
