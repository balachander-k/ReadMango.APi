using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReadMango.APi.Data;
using ReadMango.APi.Models;
using ReadMango.APi.Models.DTO;
using ReadMango.APi.Services;
using ReadMango.APi.Utility;

namespace ReadMango.APi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly ApplicationDbContext _DbContext;
        private ApiResponse _response;
        private readonly IBlobService _blobService;
        private readonly ILogger<MenuItemController> _logger;
        public MenuItemController(ApplicationDbContext dbContext, IBlobService blobService, ILogger<MenuItemController> logger)
        {
            _DbContext = dbContext;
            _response = new ApiResponse();
            _blobService = blobService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuItems()
        {
            _response.Result = _DbContext.MenuItems;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name ="GetMenuItem")]
        public async Task<IActionResult> GetMenuItems(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            MenuItem menuItem = _DbContext.MenuItems.FirstOrDefault(u => u.Id == id);
            if (menuItem == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Result = menuItem;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateMenu([FromForm] MenuItemCreateDTO menuItemCreate)
        {
            _logger.LogInformation("CreateMenu called with Name: {Name}", menuItemCreate?.Name);
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemCreate.File == null || menuItemCreate.File.Length == 0)
                    {
                        _logger.LogWarning("CreateMenu failed: No file uploaded.");
                        return BadRequest();
                    }
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(menuItemCreate.File.FileName)}";
                    MenuItem menuItemDTO = new()
                    {
                        Name = menuItemCreate.Name,
                        Price = menuItemCreate.Price,
                        Category = menuItemCreate.Category,
                        SepcialTag = menuItemCreate.SepcialTag,
                        Description = menuItemCreate.Description,
                        Image = await _blobService.UploadBlob(fileName, SD.SD_Storage_Container, menuItemCreate.File)
                    };

                    _DbContext.MenuItems.Add(menuItemDTO);
                    _DbContext.SaveChanges();
                    _logger.LogInformation("MenuItem created successfully: ID = {Id}", menuItemDTO.Id);

                    _response.Result = menuItemDTO;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetMenuItem", new { id = menuItemDTO.Id }, _response);
                }
                else
                {
                    _logger.LogWarning("CreateMenu failed: Model state is invalid.");
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            { 
            
                _logger.LogError(ex, "Error occurred in CreateMenu.");
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> UpdateMenu(int id, [FromForm] MenuItemUpdateDTO menuItemUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemUpdate == null || id != menuItemUpdate.Id)
                    {
                        return BadRequest();
                    }

                    MenuItem menuItemFromDb = await _DbContext.MenuItems.FindAsync(id);

                    if (menuItemFromDb == null)
                    {
                        return BadRequest();
                    }

                    menuItemFromDb.Name = menuItemUpdate.Name;
                    menuItemFromDb.Price = menuItemUpdate.Price;
                    menuItemFromDb.Category = menuItemUpdate.Category;
                    menuItemFromDb.SepcialTag = menuItemUpdate.SepcialTag;
                    menuItemFromDb.Description = menuItemUpdate.Description;

                    if (menuItemUpdate.File != null || menuItemUpdate.File.Length > 0)
                    {
                        String fileName = $"{Guid.NewGuid()}{Path.GetExtension(menuItemUpdate.File.FileName)}";
                        await _blobService.DeleteBlob(menuItemFromDb.Image.Split('/').Last(), SD.SD_Storage_Container);
                        menuItemFromDb.Image = await _blobService.UploadBlob(fileName, SD.SD_Storage_Container, menuItemUpdate.File);

                    }

                    _DbContext.MenuItems.Update(menuItemFromDb);
                    _DbContext.SaveChanges();
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);
                }
                else
                    _response.IsSuccess = false;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<String>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse>> DeleteMenu(int id)
        {
            try
            {
                if(id==0)
                {
                    return BadRequest();
                }
                MenuItem menuItemFromDb = await _DbContext.MenuItems.FindAsync(id);

                if(menuItemFromDb==null)
                {
                    return BadRequest();

                }
                await _blobService.DeleteBlob(menuItemFromDb.Image.Split('/').Last(), SD.SD_Storage_Container);

                int millseconds = 2000;
                Thread.Sleep(millseconds);

                _DbContext.MenuItems.Remove(menuItemFromDb);
                _DbContext.SaveChanges();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


    }
}
