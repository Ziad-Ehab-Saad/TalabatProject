using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TalabatCore;
using TalabatCore.Entities;

namespace TalabatPL.Controllers
{
    public class ProductController : ApiBaseController
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
        {
            var products = await unitOfWork.Repository<Product>().GetAllAsync();
            if (products is null|| !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product= await unitOfWork.Repository<Product>().GetByIdAsync(id);
            if (product is null )
            {
                return NotFound();

            }
            return Ok(product);
        }
        public async Task Test()
        {
            //    Expression<Predicate<Product>> filter = p => p.Category == "Book";
            //    Expression<Func<Product, string>> filter = p => p.Name;
            //    Expression<Action<Product>> print = p => Console.WriteLine(p.Name+p.Price);
            //}
        }
    }
}
