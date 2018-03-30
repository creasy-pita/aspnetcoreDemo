using System.ComponentModel.DataAnnotations;

namespace MyCookieAuthSample.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }
        public bool Rememberme { get ; set ; }
    }
}