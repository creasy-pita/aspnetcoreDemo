using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCookieAuthSample.ViewModels
{
    public class LoginViewModel
    {
        [Required]//必须的
        [DataType(DataType.EmailAddress)]//内容检查是否为邮箱
        public string Email { get; set; }

        [Required]//必须的
        [DataType(DataType.Password)]//内容检查是否为密码
        public string Password { get; set; }
    }
}
