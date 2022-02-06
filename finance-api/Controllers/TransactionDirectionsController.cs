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
            // Todo: проверка на id и существование направления

            List<TransactionDirection> directions =
                _dbContext.TransactionDirections
                    .ToList()
                    .FindAll(c => !c.Deleted);

            return directions.Find(t => t.Id == id);
        }

        [HttpPut]
        public IActionResult Put(TransactionDirectionRequest request)
        {
            // Todo: проверка на существование направления

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

        [HttpPost]
        public IActionResult Post(TransactionDirectionRequest request)
        {
            // Todo: проверка на id и существование направления

            TransactionDirection direction =
                _dbContext.TransactionDirections
                    .ToList()
                    .FindAll(t => !t.Deleted)
                    .Find(t => Guid.Equals(t.Id, request.Id));

            direction.Name = request.Name;

            _dbContext.SaveChanges();

            return Ok(direction);
        }

        [HttpDelete]
        public IActionResult Delete(TransactionDirectionIdRequest request)
        {
            // Todo: проверка на id и существование направления

            TransactionDirection direction =
                _dbContext.TransactionDirections
                    .ToList()
                    .Find(t => Guid.Equals(t.Id, request.Id));

            direction.Deleted = !direction.Deleted;

            _dbContext.SaveChanges();

            return Ok(direction);
        }

        public class TransactionDirectionRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class TransactionDirectionIdRequest
        {
            public Guid Id { get; set; }
        }
    }
}
