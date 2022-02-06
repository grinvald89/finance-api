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
            List<TransactionStatus> statuses =
                _dbContext.TransactionStatuses
                    .ToList()
                    .FindAll(s => !s.Deleted);

            return statuses;
        }

        [HttpGet("{id}")]
        public TransactionStatus? Get(Guid id)
        {
            // Todo: проверка на id и существование статуса

            List<TransactionStatus> statuses =
                _dbContext.TransactionStatuses
                    .ToList()
                    .FindAll(s => !s.Deleted);

            return statuses.Find(t => t.Id == id);
        }

        [HttpPut]
        public IActionResult Put(TransactionStatusRequest request)
        {
            bool isStatusExist =
                _dbContext.TransactionStatuses
                .ToList()
                .FindAll(s => !s.Deleted)
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

        [HttpPost]
        public IActionResult Post(TransactionStatusRequest request)
        {
            // Todo: проверка на id и существование статуса

            TransactionStatus status =
                _dbContext.TransactionStatuses
                    .ToList()
                    .FindAll(s => !s.Deleted)
                    .Find(s => Guid.Equals(s.Id, request.Id));

            status.Name = request.Name;

            _dbContext.SaveChanges();

            return Ok(status);
        }

        [HttpDelete]
        public IActionResult Delete(TransactionStatusIdRequest request)
        {
            // Todo: проверка на id и существование статуса

            TransactionStatus status =
                _dbContext.TransactionStatuses
                    .ToList()
                    .Find(s => Guid.Equals(s.Id, request.Id));

            status.Deleted = true;

            _dbContext.SaveChanges();

            return Ok(status);
        }

        public class TransactionStatusRequest
        {
            public Guid Id{ get; set; }
            public string Name { get; set; }
        }

        public class TransactionStatusIdRequest
        {
            public Guid Id { get; set; }
        }
    }
}
