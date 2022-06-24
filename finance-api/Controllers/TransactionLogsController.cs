using finance_api.Data;
using finance_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace finance_api.Controllers
{
    [Route("api/transaction-logs")]
    [ApiController]
    public class TransactionLogsController : ControllerBase
    {
        private readonly FinanceDbContext _dbContext;

        public TransactionLogsController(FinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public List<TransactionLog> get()
        {
            List<TransactionLog> logs = _dbContext.TransactionLogs
                .ToList()
                .FindAll(t => !t.Deleted);

            return logs;
        }
    }
}
