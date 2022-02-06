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
            return _dbContext.TransactionTags.ToList().FindAll(c => !c.Deleted);
        }

        [HttpGet("{id}")]
        public TransactionTag? Get(Guid id)
        {
            List<TransactionTag> tags = _dbContext.TransactionTags.ToList().FindAll(c => !c.Deleted);

            return tags.Find(t => t.Id == id);
        }

        [HttpPost]
        public IActionResult Post(TransactionTagCreateRequest request)
        {
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

        public class TransactionTagCreateRequest
        {
            public string Name { get; set; }
        }
    }
}
