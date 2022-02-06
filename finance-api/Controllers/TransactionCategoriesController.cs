using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-categories")]
    [ApiController]
    public class TransactionCategoriesController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionCategoriesController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<TransactionCategory> Get()
        {
            string? queryDirectionId = HttpContext.Request.Query["directionId"];
            Guid directionId;

            if (queryDirectionId == null)
            {
                return _dbContext.TransactionCategories.ToList().FindAll(c => !c.Deleted);
            }

            directionId = Guid.Parse(queryDirectionId);

            return _dbContext.TransactionCategories.ToList()
                .FindAll(c => !c.Deleted && Guid.Equals(c.DirectionId, directionId));
        }

        [HttpGet("{id}")]
        public TransactionCategory? Get(Guid id)
        {
            List<TransactionCategory> categories = _dbContext.TransactionCategories.ToList().FindAll(c => !c.Deleted);

            return categories.Find(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(TransactionCategoryCreateRequest request)
        {
            TransactionCategory category = new TransactionCategory()
            {
                Deleted = false,
                DirectionId = request.DirectionId,
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _dbContext.TransactionCategories.Add(category);
            _dbContext.SaveChanges();

            return Ok(category);
        }

        public class TransactionCategoryCreateRequest
        {
            public Guid DirectionId { get; set; }
            public string Name { get; set; }
        }
    }
}
