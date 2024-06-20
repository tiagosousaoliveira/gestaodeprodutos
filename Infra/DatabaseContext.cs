using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) :
            base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.Entity<Produto>().HasQueryFilter(u => u.SituacaoProduto == true);
        }
    }
}
