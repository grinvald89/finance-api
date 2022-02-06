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
            // Todo: проверка на id и существование направления (direction)

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
            // Todo: проверка на id и существование категории

            List<TransactionCategory> categories =
                _dbContext.TransactionCategories
                    .ToList()
                    .FindAll(c => !c.Deleted);

            return categories.Find(t => t.Id == id);
        }

        [HttpPut]
        public IActionResult Put(TransactionCategoryRequest request)
        {
            // Todo: проверка на существование категории

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

        [HttpPost]
        public IActionResult Post(TransactionCategoryRequest request)
        {
            // Todo: проверка на id и существование категории

            TransactionCategory category =
                _dbContext.TransactionCategories
                    .ToList()
                    .FindAll(c => !c.Deleted)
                    .Find(c => Guid.Equals(c.Id, request.Id));

            category.Name = request.Name;
            category.DirectionId = request.DirectionId;

            _dbContext.SaveChanges();

            return Ok(category);
        }

        [HttpDelete]
        public IActionResult Delete(TransactionCategoryIdRequest request)
        {
            // Todo: проверка на id и существование категории

            TransactionCategory category =
                _dbContext.TransactionCategories
                    .ToList()
                    .Find(c => Guid.Equals(c.Id, request.Id));

            category.Deleted = !category.Deleted;

            _dbContext.SaveChanges();

            return Ok(category);
        }

        public class TransactionCategoryRequest
        {
            public Guid DirectionId { get; set; }
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class TransactionCategoryIdRequest
        {
            public Guid Id { get; set; }
        }
    }
}
