using Ecommercer.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Ecommercer.Infra.Context
{
    public class DbEcommecer : DbContext
    {
        public DbEcommecer(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbEcommecer).Assembly);
        }
    }
}
