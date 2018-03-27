using System.ComponentModel.DataAnnotations;

namespace MyCookieAuthSample.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public string ConfirmPassword { get; set; }

    }
}