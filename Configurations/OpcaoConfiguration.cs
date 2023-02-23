using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteAPI.Models;

namespace TesteAPI.Configurations
{
    public class OpcaoConfiguration : IEntityTypeConfiguration<Opcao>
    {
        public void Configure(EntityTypeBuilder<Opcao> builder)
        {
            builder.ToTable("Opcao");
            builder.HasData
            (
                new Opcao { Id = 1, Descricao = "Opção I"   },
                new Opcao { Id = 2, Descricao = "Opção II"  },
                new Opcao { Id = 3, Descricao = "Opção III" }
            );
        }
    }
}