using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatCore;
using TalabatCore.Repositories.Contracts;

namespace TalabatPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }




    }
}
