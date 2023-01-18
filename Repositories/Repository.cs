using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TesteAPI.Interfaces;
using TesteAPI.Models;

namespace TesteAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly Contexto Db;
        protected readonly DbSet<T> DbSet;

        protected Repository(Contexto db)
        {
            Db = db;
            DbSet = db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetComFiltro(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Post(T entity)
        {
            try
            {
                DbSet.Add(entity);
                await Db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                string mensagem = e.Message;
                throw;
            }
        }

        public virtual async Task Put(T entity)
        {
            try
            {
                DbSet.Update(entity);
                await Db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                string mensagem = e.Message;
                throw;
            }
        }

        public virtual async Task Delete(T entity)
        {
            try
            {
                DbSet.Remove(entity);
                await Db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                string mensagem = e.Message;
                throw;
            }
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}