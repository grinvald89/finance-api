using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-category-options")]
    [ApiController]
    public class TransactionCategoryOptionsController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionCategoryOptionsController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<TransactionCategoryOption> Get()
        {
            return _dbContext.TransactionCategoryOptions.ToList().FindAll(c => !c.Deleted);
        }

        [HttpGet("{id}")]
        public TransactionCategoryOption? Get(Guid id)
        {
            List<TransactionCategoryOption> options = _dbContext.TransactionCategoryOptions.ToList().FindAll(c => !c.Deleted);

            return options.Find(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(TransactionCategoryOptionCreateRequest request)
        {
            TransactionCategoryOption option = new TransactionCategoryOption()
            {
                Deleted = false,
                Id = Guid.NewGuid(),
                Name = request.Name,
                CategoryId = request.CategoryId
            };

            _dbContext.TransactionCategoryOptions.Add(option);
            _dbContext.SaveChanges();

            return Ok(option);
        }

        public class TransactionCategoryOptionCreateRequest
        {
            public Guid CategoryId { get; set; }
            public string Name { get; set; }
        }
    }

    [Route("api/transaction-category-options/by-category")]
    [ApiController]
    public class TransactionCategoryOptionsByCategoryController : ControllerBase
    {
        private readonly FinanceDbContext dbContext;

        public TransactionCategoryOptionsByCategoryController(FinanceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{categoryId}")]
        public List<TransactionCategoryOption>? Get(Guid categoryId)
        {
            List<TransactionCategoryOption> options = this.dbContext.TransactionCategoryOptions.ToList()
                .FindAll(c => !c.Deleted && c.CategoryId == categoryId);

            return options;
        }
    }
}
