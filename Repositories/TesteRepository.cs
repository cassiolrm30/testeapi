using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteAPI.Interfaces;
using TesteAPI.Models;

namespace TesteAPI.Repositories
{
    public class TesteRepository : Repository<Teste>, ITesteRepository
    {
        private readonly IMapper _mapper;
        public TesteRepository(Contexto contexto, IMapper mapper) : base(contexto) => _mapper = mapper;

        public override async Task<List<Teste>> GetAll()
        {
            List<Teste> registros = await DbSet.ToListAsync();
            registros.Sort((x, y) => x.Descricao.CompareTo(y.Descricao));
            var resultado = registros;
            return resultado;
        }
        public override async Task<Teste> GetById(int id)
        {
            var resultado = await Db.Teste.AsNoTracking().Include(x => x.Opcao).FirstOrDefaultAsync(x => x.Id == id);
            return resultado;
        }
    }
}