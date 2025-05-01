using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TalabatPL.Controllers
{
    public class BuggyController : ApiBaseController
    {
        [HttpGet("Error")]
        public async Task GetServerError()
        {
            throw new NullReferenceException();
        }



    }
}
