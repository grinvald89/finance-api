using finance_api.Models;
using Microsoft.EntityFrameworkCore;

namespace finance_api.Data
{
    public class FinanceDbContext: DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options): base(options) { }
        public DbSet<TypeFinancialTransaction> TypesFinancialTransaction { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFullName> UserFullNames { get; set; }
        public DbSet<BusinessAccount> BusinessAccounts { get; set; }
        public DbSet<PersonalAccount> PersonalAccounts { get; set; }
        public DbSet<FamilyAccount> FamilyAccounts { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserAuthorization> UserAuthorizations { get; set; }
    }
}
