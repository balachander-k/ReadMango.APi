using Microsoft.AspNetCore.Identity;

namespace ReadMango.APi.Models
{
    public class Applicationuser :IdentityUser
    {
        public string Name {  get; set; }
    }
}
