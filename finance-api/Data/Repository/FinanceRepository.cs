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
                    .Include(c => c.FullName)
                    .Include(c => c.BusinessAccounts)
                    .Include(c => c.PersonalAccount)
                    .Include(c => c.FamilyAccount)
                    .Include(c => c.Roles)
                    .Include(c => c.Authorization);

        public IEnumerable<UserFullName> UserFullNames => dbContext.UserFullNames;
        public IEnumerable<BusinessAccount> BusinessAccounts => dbContext.BusinessAccounts;
        public IEnumerable<PersonalAccount> PersonalAccounts => dbContext.PersonalAccounts;
        public IEnumerable<FamilyAccount> FamilyAccounts => dbContext.FamilyAccounts;
        public IEnumerable<UserRole> UserRoles => dbContext.UserRoles;
        public IEnumerable<UserAuthorization> UserAuthorizations => dbContext.UserAuthorizations;


        public IEnumerable<Transaction> Transactions =>
                dbContext.Transactions
                    .Include(t => t.Category)
                    .Include(t => t.CategoryOption)
                    .Include(t => t.Status)
                    .Include(t => t.SubCategory)
                    .Include(t => t.SubCategoryFirstOption)
                    .Include(t => t.SubCategorySecondOption)
                    .Include(t => t.Type);

        public IEnumerable<TransactionCategory> TransactionCategories { get; set; }
        public IEnumerable<TransactionCategoryOption> TransactionCategoryOptions { get; set; }
        public IEnumerable<TransactionStatus> TransactionStatuses { get; set; }
        public IEnumerable<TransactionSubCategory> TransactionSubCategories { get; set; }
        public IEnumerable<TransactionSubCategoryFirstOption> TransactionSubCategoryFirstOptions { get; set; }
        public IEnumerable<TransactionSubCategorySecondOption> TransactionSubCategorySecondOptions { get; set; }
        public IEnumerable<TransactionType> TransactionTypes { get; set; }
    }
}
