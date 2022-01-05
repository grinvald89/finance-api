using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-types")]
    [ApiController]
    public class TransactionTypesController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionTypesController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<TransactionType> Get()
        {
            return _dbContext.TransactionTypes.ToList();
        }

        [HttpGet("{id}")]
        public TransactionType? Get(Guid id)
        {
            List<TransactionType> types = _dbContext.TransactionTypes.ToList();

            return types.Find(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(TransactionTypeCreateRequest request)
        {
            TransactionType type = new TransactionType()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _dbContext.TransactionTypes.Add(type);
            _dbContext.SaveChanges();

            return Ok(type);
        }

        public class TransactionTypeCreateRequest
        {
            public string Name { get; set; }
        }
    }
}
