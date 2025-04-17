using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatCore.Entities;

namespace TalabatCore.Specifications.ProductSpecifications
{
    public class ProductsWithCategoryAndBrandSpec :BaseSpecification<Product>
    {

        public ProductsWithCategoryAndBrandSpec():base()
        {
            AddIncludes();

        }
        public ProductsWithCategoryAndBrandSpec(int id):base(p=>p.Id==id)
        {
            AddIncludes();
        }
        private void AddIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
