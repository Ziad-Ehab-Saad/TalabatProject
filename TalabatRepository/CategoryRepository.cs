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
    class CategoryRepository :GenericRepository<ProductCategory> ,ICategoryRepository
    {

        public CategoryRepository(StoreContext context):base(context)
        {
        }
    }
}
