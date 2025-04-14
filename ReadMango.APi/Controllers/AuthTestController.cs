using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadMango.APi.Utility;

namespace ReadMango.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthTestController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string>> GetSomething()
        {
            return "You are Authenticated";
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles =SD.Role_Admin)]
        public async Task<ActionResult<string>> GetSomething(int somevalue)
        { 
            //Authorization -> Authentication + Some Access / Roles 
            return "You are Authorized With Role of Admin ";
        }
    }
}
