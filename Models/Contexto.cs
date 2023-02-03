using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TesteAPI.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions options) : base(options) { }

        public DbSet<Opcao> Opcao { get; set; }
        public DbSet<Teste> Teste { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);

            modelBuilder.Entity<Teste>().Property(t => t.Valor).HasColumnType("decimal(18,2)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}