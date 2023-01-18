using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TesteAPI.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetComFiltro(Expression<Func<T, bool>> predicate);
        Task Post(T entity);
        Task Put(T entity);
        Task Delete(T t);
    }
}