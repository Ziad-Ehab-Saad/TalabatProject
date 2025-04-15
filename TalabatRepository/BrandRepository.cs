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
    class BrandRepository :GenericRepository<ProductBrand>, IBrandRepository
    {

        public BrandRepository(StoreContext context):base(context)
        {
            
        }
    }
}
