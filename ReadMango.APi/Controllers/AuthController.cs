using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReadMango.APi.Data;
using ReadMango.APi.Models;
using ReadMango.APi.Models.DTO;
using ReadMango.APi.Services;
using ReadMango.APi.Utility;

namespace ReadMango.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _DbContext;
        private ApiResponse _response;
        private readonly ILogger<MenuItemController> _logger;
        private string secretKey;
        private readonly UserManager<Applicationuser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthController(ApplicationDbContext dbContext, IBlobService blobService, ILogger<MenuItemController> logger,
            UserManager<Applicationuser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            _DbContext = dbContext;
            _response = new ApiResponse();
            _logger = logger;
             secretKey=configuration.GetValue<String>("ApiSettings:Secret");
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            Applicationuser userFromDb=_DbContext.Applicationusers.FirstOrDefault(u=>u.UserName.ToLower()==model.UserName.ToLower());
            
            bool isValid =await _userManager.CheckPasswordAsync(userFromDb,model.Password);

            if(isValid==false)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }

            //need to Generate JWT TOKEN 
            var roles = await _userManager.GetRolesAsync(userFromDb);
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key=Encoding.ASCII.GetBytes(secretKey);


            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("fullName", userFromDb.Name),
                    new Claim("id", userFromDb.Id.ToString()),
                    new Claim(ClaimTypes.Email,userFromDb.UserName.ToString()),
                    new Claim(ClaimTypes.Role,roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token=tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginresponse = new()
            {
                Email = userFromDb.Email,
                Token = tokenHandler.WriteToken(token)
            };

            if(loginresponse.Email==null || string.IsNullOrEmpty(loginresponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }


            _response.StatusCode=HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginresponse;
            return Ok(_response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            Applicationuser userFromDb = _DbContext.Applicationusers.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

            if (userFromDb != null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("User already exists");
                return BadRequest(_response);
            }

            Applicationuser newUser = new()
            {
                UserName = model.UserName,
                Email = model.UserName,
                NormalizedEmail = model.UserName.ToUpper(),
                Name = model.Name
            };

            try
            {
                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
                    {
                        //create Role in DB
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                        await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
                    }
                    if (model.Role.ToLower() == SD.Role_Admin)
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(newUser, SD.Role_Customer);
                    }
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
            }
            catch (Exception)
            {

            }
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("User registration failed");
            return BadRequest(_response);
        }

    }
}
