using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-sub-categories")]
    [ApiController]
    public class TransactionSubCategoriesController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionSubCategoriesController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<TransactionSubCategory> Get()
        {
            string? queryCategoryId = HttpContext.Request.Query["CategoryId"];
            string? queryCategoryOptionId = HttpContext.Request.Query["CategoryOptionId"];
            Guid categoryId;
            Guid categoryOptionId;

            if (queryCategoryId == null)
            {
                return _dbContext.TransactionSubCategories.ToList().FindAll(c => !c.Deleted);
            }

            if (queryCategoryOptionId == null)
            {
                categoryId = Guid.Parse(queryCategoryId);

                return _dbContext.TransactionSubCategories.ToList()
                    .FindAll(c => !c.Deleted && Guid.Equals(c.CategoryId, categoryId));
            }

            categoryId = Guid.Parse(queryCategoryId);
            categoryOptionId = Guid.Parse(queryCategoryOptionId);

            return _dbContext.TransactionSubCategories.ToList()
                .FindAll(c => !c.Deleted && Guid.Equals(c.CategoryId, categoryId) && Guid.Equals(c.CategoryOptionId, categoryOptionId));
        }

        [HttpGet("{id}")]
        public TransactionSubCategory? Get(Guid id)
        {
            List<TransactionSubCategory> options = _dbContext.TransactionSubCategories.ToList().FindAll(c => !c.Deleted);

            return options.Find(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(SubCategoryCreateRequest request)
        {
            TransactionSubCategory subCategory = new TransactionSubCategory()
            {
                Deleted = false,
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            if (request.CategoryId != null)
            {
                subCategory.CategoryId = Guid.Parse(request.CategoryId);
            }

            if (request.CategoryOptionId != null)
            {
                subCategory.CategoryOptionId = Guid.Parse(request.CategoryOptionId);
            }

            _dbContext.TransactionSubCategories.Add(subCategory);
            _dbContext.SaveChanges();

            return Ok(subCategory);
        }

        public class SubCategoryCreateRequest
        {
            public string? CategoryId { get; set; }
            public string? CategoryOptionId{ get; set; }
            public string Name { get; set; }
        }
    }
}
