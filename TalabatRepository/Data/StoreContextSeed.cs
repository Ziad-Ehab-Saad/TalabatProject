using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TalabatCore.Entities;

namespace TalabatRepository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext storeContext)
        {
            if (!storeContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../TalabatRepository/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands != null && brands.Count > 0)
                {
                    brands = brands.Select(e => new ProductBrand { Name = e.Name }).ToList();

                    foreach (var brand in brands)
                    {
                        storeContext.ProductBrands.Add(brand);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }

            if (!storeContext.ProductCategories.Any())
            {
                var categoriesData = File.ReadAllText("../TalabatRepository/Data/DataSeeding/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                if (categories != null && categories.Count > 0)
                {
                    categories = categories.Select(e => new ProductCategory { Name = e.Name }).ToList();

                    foreach (var category in categories)
                    {
                        storeContext.ProductCategories.Add(category);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
            if (!storeContext.Products.Any())
            {
                var ProductsData = File.ReadAllText("../TalabatRepository/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products != null && products.Count > 0)
                {
                    products = products.Select(e => new Product
                    {
                        Name = e.Name,
                        Description = e.Description,
                        PictureUrl = e.PictureUrl,
                        Price = e.Price,
                        CategoryId = e.CategoryId,
                        BrandId = e.BrandId
                    }).ToList();
                    foreach (var product in products)
                    {
                        storeContext.Products.Add(product);
                    }
                    await storeContext.SaveChangesAsync();
                }
            }
        }
    }
}
