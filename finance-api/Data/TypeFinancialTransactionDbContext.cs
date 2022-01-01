using Microsoft.EntityFrameworkCore;

namespace finance_api.Data
{
    public class TypeFinancialTransactionDbContext: DbContext
    {
        public TypeFinancialTransactionDbContext(DbContextOptions<TypeFinancialTransactionDbContext> options): base(options)
        {

        }

        public DbSet<TypeFinancialTransaction> TypesFinancialTransaction { get; set; }
    }
}
