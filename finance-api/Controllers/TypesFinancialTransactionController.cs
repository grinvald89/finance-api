using finance_api.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/types-financial-transaction")]
    [ApiController]
    public class TypesFinancialTransactionController : ControllerBase
    {
        private readonly TypeFinancialTransactionDbContext _dbContext;

        public TypesFinancialTransactionController(TypeFinancialTransactionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/types-financial-transaction
        [HttpGet]
        public TypeFinancialTransaction[] Get()
        {
            return _dbContext.TypesFinancialTransaction.ToArray();
        }
    }
}
