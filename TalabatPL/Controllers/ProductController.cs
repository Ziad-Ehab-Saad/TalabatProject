using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TalabatCore;
using TalabatCore.Entities;
using TalabatCore.Specifications;
using TalabatCore.Specifications.ProductSpecifications;
using TalabatPL.DTOs;
using TalabatPL.Erros;

namespace TalabatPL.Controllers
{
    public class ProductController : ApiBaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        
        [ProducesResponseType(typeof(ProductToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseError),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetAllAsync(string sort)
        {
            var spec = new ProductsWithCategoryAndBrandSpec(sort);


            var products = await unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

            if (products is null || !products.Any())
            {
                return NotFound(new ApiResponseError(404));
            }
            return Ok(mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
            //var products = await unitOfWork.Repository<Product>().GetAllAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
        {
            var spec = new ProductsWithCategoryAndBrandSpec(id);

            var product = await unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            if (product is null)
            {
                return NotFound(new ApiResponseError(404));

            }
            return Ok(mapper.Map<Product, ProductToReturnDto>(product));
        }


        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrandsAsync()
        {
            var brands=await  unitOfWork.Repository<ProductBrand>().GetAllAsync();
            if (brands is null)
            {
                return NotFound(new ApiResponseError(404));
            }
            return Ok(brands);
            
        }
        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategoryAsync()
        {
            var categories = await unitOfWork.Repository<ProductCategory>().GetAllAsync();
            if (categories is null)
            {
                return NotFound(new ApiResponseError(404));
            }
            return Ok(categories);

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
