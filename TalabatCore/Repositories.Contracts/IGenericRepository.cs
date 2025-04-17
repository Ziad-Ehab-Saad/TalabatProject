using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Specifications;

namespace TalabatCore.Repositories.Contracts
{
   public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        public  Task<T?> GetByIdWithSpecAsync(ISpecification<T> spec);

        public Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
