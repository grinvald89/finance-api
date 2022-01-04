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

        public IEnumerable<TypeFinancialTransaction> TypesFinancialTransaction => dbContext.TypesFinancialTransaction;
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
    }
}
