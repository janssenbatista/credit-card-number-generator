using CreditCardNumberGeneratorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CreditCardNumberGeneratorApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) {}
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
    }
}