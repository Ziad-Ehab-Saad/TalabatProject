using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TalabatCore;
using TalabatCore.Entities;
using TalabatCore.Specifications;
using TalabatCore.Specifications.ProductSpecifications;
using TalabatPL.DTOs;

namespace TalabatPL.Controllers
{
    public class ProductController : ApiBaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllAsync()
        {
            var spec = new ProductsWithCategoryAndBrandSpec();

            var products = await unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

            if (products is null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
            //var products = await unitOfWork.Repository<Product>().GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductsWithCategoryAndBrandSpec(id);
       
            var product = await unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            if (product is null)
            {
                return NotFound();

            }
            return Ok(mapper.Map<Product,ProductToReturnDto>(product));
        }
        //public async Task Test()
        //{
        //    //    Expression<Predicate<Product>> filter = p => p.Category == "Book";
        //    //    Expression<Func<Product, string>> filter = p => p.Name;
        //    //    Expression<Action<Product>> print = p => Console.WriteLine(p.Name+p.Price);
        //    //}
        //}
    }
}
