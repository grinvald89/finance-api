using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-directions")]
    [ApiController]
    public class TransactionDirectionsController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionDirectionsController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<TransactionDirection> Get()
        {
            return _dbContext.TransactionDirections.ToList().FindAll(c => !c.Deleted);
        }

        [HttpGet("{id}")]
        public TransactionDirection? Get(Guid id)
        {
            List<TransactionDirection> directions = _dbContext.TransactionDirections.ToList().FindAll(c => !c.Deleted);

            return directions.Find(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(TransactionDirectionCreateRequest request)
        {
            TransactionDirection direction = new TransactionDirection()
            {
                Deleted = false,
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _dbContext.TransactionDirections.Add(direction);
            _dbContext.SaveChanges();

            return Ok(direction);
        }

        public class TransactionDirectionCreateRequest
        {
            public string Name { get; set; }
        }
    }
}
