using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-sub-category-second-options")]
    [ApiController]
    public class TransactionSubCategorySecondOptionsController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionSubCategorySecondOptionsController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<TransactionSubCategorySecondOption> Get()
        {
            string? querySubCategoryId = HttpContext.Request.Query["subCategoryId"];
            string? querySubCategoryFirstOptionId = HttpContext.Request.Query["subCategoryFirstOptionId"];
            Guid subCategoryId;
            Guid subCategoryFirstOptionId;

            if (querySubCategoryId == null)
            {
                return _dbContext.TransactionSubCategorySecondOptions.ToList().FindAll(c => !c.Deleted);
            }

            if (querySubCategoryFirstOptionId == null)
            {
                subCategoryId = Guid.Parse(querySubCategoryId);

                return _dbContext.TransactionSubCategorySecondOptions.ToList()
                    .FindAll(c => !c.Deleted && Guid.Equals(c.SubCategoryId, subCategoryId));
            }

            subCategoryId = Guid.Parse(querySubCategoryId);
            subCategoryFirstOptionId = Guid.Parse(querySubCategoryFirstOptionId);

            return _dbContext.TransactionSubCategorySecondOptions.ToList()
                .FindAll(c => !c.Deleted && Guid.Equals(c.SubCategoryId, subCategoryId) && Guid.Equals(c.SubCategoryFirstOptionId, subCategoryFirstOptionId));
        }

        [HttpGet("{id}")]
        public TransactionSubCategorySecondOption? Get(Guid id)
        {
            List<TransactionSubCategorySecondOption> options = _dbContext.TransactionSubCategorySecondOptions.ToList().FindAll(c => !c.Deleted);

            return options.Find(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(SubCategorySecondOptionCreateRequest request)
        {
            TransactionSubCategorySecondOption option = new TransactionSubCategorySecondOption()
            {
                Deleted = false,
                Id = Guid.NewGuid(),
                Name = request.Name,
                SubCategoryId = request.SubCategoryId,
                SubCategoryFirstOptionId = request.SubCategoryFirstOptionId
            };

            _dbContext.TransactionSubCategorySecondOptions.Add(option);
            _dbContext.SaveChanges();

            return Ok(option);
        }

        public class SubCategorySecondOptionCreateRequest
        {
            public Guid SubCategoryId { get; set; }
            public Guid SubCategoryFirstOptionId { get; set; }
            public string Name { get; set; }
        }
    }
}
