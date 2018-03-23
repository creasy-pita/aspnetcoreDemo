using System.ComponentModel.DataAnnotations;

namespace MyCookieAuthSample.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public string ConfirmPassword { get; set; }

    }
}