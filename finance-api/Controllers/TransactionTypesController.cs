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
        public TransactionType[] Get()
        {
            return _dbContext.TransactionTypes.ToArray();
        }
    }
}
