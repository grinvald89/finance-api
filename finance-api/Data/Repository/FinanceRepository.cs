using Microsoft.EntityFrameworkCore;
using finance_api.Models;

namespace finance_api.Data
{
    public class FinanceRepository
    {
        private readonly FinanceDbContext dbContext;

        public FinanceRepository(FinanceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<User> Users =>
                dbContext.Users
                    .Include(c => c.Authorization)
                    .Include(c => c.FullName)
                    .Include(c => c.Roles);

        public IEnumerable<UserFullName> UserFullNames => dbContext.UserFullNames;
        public IEnumerable<UserRole> UserRoles => dbContext.UserRoles;
        public IEnumerable<UserAuthorization> UserAuthorizations => dbContext.UserAuthorizations;


        public IEnumerable<Transaction> Transactions =>
                dbContext.Transactions
                    .Include(t => t.Category)
                    .Include(t => t.Direction)
                    .Include(t => t.Status)
                    .Include(t => t.Tags)
                    .Include(t => t.Type);

        public IEnumerable<TransactionCategory> TransactionCategories { get; set; }
        public IEnumerable<TransactionDirection> TransactionDirections { get; set; }
        public IEnumerable<TransactionStatus> TransactionStatuses { get; set; }
        public IEnumerable<TransactionTag> TransactionTags { get; set; }
        public IEnumerable<TransactionType> TransactionTypes { get; set; }
    }
}
