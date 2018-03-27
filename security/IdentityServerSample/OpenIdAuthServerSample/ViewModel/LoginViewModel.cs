using System.ComponentModel.DataAnnotations;

namespace MyCookieAuthSample.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

    }
}