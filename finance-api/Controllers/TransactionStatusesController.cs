using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-statuses")]
    [ApiController]
    public class TransactionStatusesController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionStatusesController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<TransactionStatus> Get()
        {
            return _dbContext.TransactionStatuses.ToList();
        }

        [HttpGet("{id}")]
        public TransactionStatus? Get(Guid id)
        {
            List<TransactionStatus> statuses = _dbContext.TransactionStatuses.ToList();

            return statuses.Find(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(TransactionStatusCreateRequest request)
        {
            TransactionStatus status = new TransactionStatus()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _dbContext.TransactionStatuses.Add(status);
            _dbContext.SaveChanges();

            return Ok(status);
        }

        public class TransactionStatusCreateRequest
        {
            public string Name { get; set; }
        }
    }
}
