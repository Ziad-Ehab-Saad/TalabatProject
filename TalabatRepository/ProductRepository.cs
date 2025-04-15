using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;
using TalabatCore.Repositories.Contracts;
using TalabatRepository.Data;

namespace TalabatRepository
{
    class ProductRepository : GenericRepository<Product>, IproductRepository
    {
        public ProductRepository(StoreContext context) : base(context)
        {

        }
    }
}
