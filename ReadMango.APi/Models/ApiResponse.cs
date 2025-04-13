using System.Net;

namespace ReadMango.APi.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }

        public List<String> ErrorMessages { get; set; }

        public Object Result { get; set; }

    }
}
