using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadMango.APi.Data;
using ReadMango.APi.Models;

namespace ReadMango.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        protected ApiResponse _response;
        private readonly ApplicationDbContext _dbContext;

        public ShoppingCartController(ApplicationDbContext dbContext)
        {
            _response = new();
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddOrUpdateItemInCart(string userId,int menuItemId,int UpdateQuantityBy)
        {

        }


    }
}
