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
            return _dbContext.TransactionTypes
                .ToList()
                .FindAll(t => !t.Deleted);
        }

        [HttpGet("{id}")]
        public TransactionType? Get(Guid id)
        {
            // Todo: проверка на id и существование типа

            List<TransactionType> types =
                _dbContext.TransactionTypes
                    .ToList()
                    .FindAll(t => !t.Deleted);

            return types.Find(t => t.Id == id);
        }

        [HttpPut]
        public IActionResult Put(TransactionTypeRequest request)
        {
            bool isTypeExist = _dbContext.TransactionTypes
                .ToList()
                .FindAll(t => !t.Deleted)
                .Find(t => t.Name == request.Name) != null;

            if (isTypeExist)
            {
                return StatusCode(501, "Тип '" + request.Name + "' уже существует!");
            }

            TransactionType type = new TransactionType()
            {
                Deleted = false,
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _dbContext.TransactionTypes.Add(type);
            _dbContext.SaveChanges();

            return Ok(type);
        }

        [HttpPost]
        public IActionResult Post(TransactionTypeRequest request)
        {
            // Todo: проверка на id и существование типа

            TransactionType type =
                _dbContext.TransactionTypes
                    .ToList()
                    .FindAll(t => !t.Deleted)
                    .Find(t => Guid.Equals(t.Id, request.Id));

            type.Name = request.Name;

            _dbContext.SaveChanges();

            return Ok(type);
        }

        [HttpDelete]
        public IActionResult Delete(TransactionTypeIdRequest request)
        {
            // Todo: проверка на id и существование типа

            TransactionType type =
                _dbContext.TransactionTypes
                    .ToList()
                    .Find(t => Guid.Equals(t.Id, request.Id));

            type.Deleted = !type.Deleted;

            _dbContext.SaveChanges();

            return Ok(type);
        }

        public class TransactionTypeRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
        public class TransactionTypeIdRequest
        {
            public Guid Id { get; set; }
        }
    }
}
