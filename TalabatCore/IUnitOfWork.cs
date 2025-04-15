using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Repositories.Contracts;

namespace TalabatCore
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        public IGenericRepository<T> Repository<T>() where T : BaseEntity;

        Task<int> CompleteAsync();
    }
}
