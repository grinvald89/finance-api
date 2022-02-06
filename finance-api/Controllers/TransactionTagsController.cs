using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-tags")]
    [ApiController]
    public class TransactionTagsController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionTagsController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<TransactionTag> Get()
        {
            return _dbContext.TransactionTags
                .ToList()
                .FindAll(c => !c.Deleted);
        }

        [HttpGet("{id}")]
        public TransactionTag? Get(Guid id)
        {
            // Todo: проверка на id и существование тега

            List<TransactionTag> tags =
                _dbContext.TransactionTags
                    .ToList()
                    .FindAll(c => !c.Deleted);

            return tags.Find(t => t.Id == id);
        }

        [HttpPut]
        public IActionResult Put(TransactionTagRequest request)
        {
            // Todo: проверка на существование тега

            TransactionTag tag = new TransactionTag()
            {
                Deleted = false,
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            _dbContext.TransactionTags.Add(tag);
            _dbContext.SaveChanges();

            return Ok(tag);
        }

        [HttpPost]
        public IActionResult Post(TransactionTagRequest request)
        {
            // Todo: проверка на id и существование тега

            TransactionTag tag =
                _dbContext.TransactionTags
                    .ToList()
                    .FindAll(t => !t.Deleted)
                    .Find(t => Guid.Equals(t.Id, request.Id));

            tag.Name = request.Name;

            _dbContext.SaveChanges();

            return Ok(tag);
        }

        [HttpDelete]
        public IActionResult Delete(TransactionTagIdRequest request)
        {
            // Todo: проверка на id и существование тега

            TransactionTag tag =
                _dbContext.TransactionTags
                    .ToList()
                    .Find(t => Guid.Equals(t.Id, request.Id));

            tag.Deleted = true;

            _dbContext.SaveChanges();

            return Ok(tag);
        }

        public class TransactionTagRequest
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class TransactionTagIdRequest
        {
            public Guid Id { get; set; }
        }
    }
}