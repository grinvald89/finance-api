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
            List<TransactionStatus> statuses = _dbContext.TransactionStatuses.ToList().FindAll(s => !s.Deleted);

            return statuses;
        }

        [HttpGet("{id}")]
        public TransactionStatus? Get(Guid id)
        {
            List<TransactionStatus> statuses = _dbContext.TransactionStatuses.ToList().FindAll(s => !s.Deleted);

            return statuses.Find(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(TransactionStatusCreateRequest request)
        {
            bool isStatusExist = _dbContext.TransactionStatuses.ToList()
                .Find(s => s.Name == request.Name) != null;

            if (isStatusExist)
            {
                return StatusCode(501, "Статус '" + request.Name + "' уже существует!");
            }

            TransactionStatus status = new TransactionStatus()
            {
                Deleted = false,
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
