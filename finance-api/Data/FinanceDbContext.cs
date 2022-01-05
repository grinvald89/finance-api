using finance_api.Models;
using Microsoft.EntityFrameworkCore;

namespace finance_api.Data
{
    public class FinanceDbContext: DbContext
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserFullName> UserFullNames { get; set; }
        public DbSet<BusinessAccount> BusinessAccounts { get; set; }
        public DbSet<PersonalAccount> PersonalAccounts { get; set; }
        public DbSet<FamilyAccount> FamilyAccounts { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserAuthorization> UserAuthorizations { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public DbSet<TransactionSubCategory> TransactionSubCategories { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        public DbSet<TransactionCategoryOption> TransactionCategoryOptions { get; set; }
        public DbSet<TransactionSubCategoryFirstOption> TransactionSubCategoryFirstOptions { get; set; }
        public DbSet<TransactionSubCategorySecondOption> TransactionSubCategorySecondOptions { get; set; }
    }
}
