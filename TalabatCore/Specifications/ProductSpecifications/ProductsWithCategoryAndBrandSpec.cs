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

        public ProductsWithCategoryAndBrandSpec(string sort):base()
        {
            AddIncludes();
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                    
                        OrderByAsc(p=>p.Price);
                        break;
                    case "priceDesc":
                        OrderByDescending(p => p.Price);
                        break;
                    default:
                        OrderBy = p => p.Name;
                        break;
                }



            }
            else
            {
                OrderBy = p => p.Name;

            }
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
