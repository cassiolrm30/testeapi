using Microsoft.EntityFrameworkCore;
using System.Linq;
using TesteAPI.Configurations;

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

            #region Perfil
            //if (modelBuilder.Entity<Opcao>().Property(t => t.Id) == 0)
            if (1 == 1)
            {
                modelBuilder.ApplyConfiguration(new OpcaoConfiguration());
                modelBuilder.Entity<Opcao>(entity => { entity.HasIndex(f => f.Descricao).IsUnique(); });
                modelBuilder.Entity<Opcao>().HasData
                (
                    new Opcao { Id = 1, Descricao = "Opção I"   },
                    new Opcao { Id = 2, Descricao = "Opção II"  },
                    new Opcao { Id = 3, Descricao = "Opção III" }
                );
            }
            #endregion

            modelBuilder.ApplyConfiguration(new OpcaoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}