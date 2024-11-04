using System.ComponentModel.DataAnnotations;

namespace simple.API.Models
{
    public class AuthenRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username không được để trống")]
        public required string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password không được để trống")]
        public required string Password { get; set; }
    }
}
