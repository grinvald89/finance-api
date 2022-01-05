using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-sub-category-first-options")]
    [ApiController]
    public class TransactionSubCategoryFirstOptionsController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionSubCategoryFirstOptionsController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<TransactionSubCategoryFirstOption> Get()
        {
            string? querySubCategoryId = HttpContext.Request.Query["subCategoryId"];
            Guid subCategoryId;

            if (querySubCategoryId == null)
            {
                return _dbContext.TransactionSubCategoryFirstOptions.ToList().FindAll(c => !c.Deleted);
            }

            subCategoryId = Guid.Parse(querySubCategoryId);

            return _dbContext.TransactionSubCategoryFirstOptions.ToList()
                .FindAll(c => !c.Deleted && Guid.Equals(c.SubCategoryId, subCategoryId));
        }

        [HttpGet("{id}")]
        public TransactionSubCategoryFirstOption? Get(Guid id)
        {
            List<TransactionSubCategoryFirstOption> options = _dbContext.TransactionSubCategoryFirstOptions.ToList().FindAll(c => !c.Deleted);

            return options.Find(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(SubCategoryFirstOptionCreateRequest request)
        {
            TransactionSubCategoryFirstOption firstOption = new TransactionSubCategoryFirstOption()
            {
                Deleted = false,
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            if (request.SubCategoryId != null)
            {
                firstOption.SubCategoryId = Guid.Parse(request.SubCategoryId);
            }

            _dbContext.TransactionSubCategoryFirstOptions.Add(firstOption);
            _dbContext.SaveChanges();

            return Ok(firstOption);
        }

        public class SubCategoryFirstOptionCreateRequest
        {
            public string? SubCategoryId { get; set; }
            public string Name { get; set; }
        }
    }
}
