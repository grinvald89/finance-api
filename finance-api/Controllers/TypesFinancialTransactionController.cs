using finance_api.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/types-financial-transaction")]
    [ApiController]
    public class TypesFinancialTransactionController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TypesFinancialTransactionController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public TypeFinancialTransaction[] Get()
        {
            return _dbContext.TypesFinancialTransaction.ToArray();
        }
    }
}
