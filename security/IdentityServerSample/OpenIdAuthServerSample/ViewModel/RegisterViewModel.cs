using System.ComponentModel.DataAnnotations;

namespace MyCookieAuthSample.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public string ConfirmPassword { get; set; }

    }
}