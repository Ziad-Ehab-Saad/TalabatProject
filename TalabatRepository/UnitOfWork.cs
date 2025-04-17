using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore;
using TalabatCore.Entities;
using TalabatCore.Repositories.Contracts;
using TalabatRepository.Data;

namespace TalabatRepository
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext storeContext;

        private Dictionary<string, object>? _repositories;
        public UnitOfWork(StoreContext storeContext)
        {
            this.storeContext = storeContext;
        }
        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {

            if (_repositories == null)
                _repositories = new Dictionary<string, object>();
           
            var type = typeof(T).Name;
           
            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new GenericRepository<T>(storeContext);
                _repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<T>)_repositories[type] ;

        }


        public async Task<int> CompleteAsync()
        {
            return await storeContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await storeContext.DisposeAsync();
        }
    }
}
