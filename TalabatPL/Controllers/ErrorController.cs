using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatPL.Erros;

namespace TalabatPL.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return NotFound(new ApiResponseError(code));

        }
    
    }

}
