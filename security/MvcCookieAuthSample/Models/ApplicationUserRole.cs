using Microsoft.AspNetCore.Identity;

namespace MvcCookieAuthSample.Models
{
    public class ApplicationUserRole: IdentityRole<int>//不加int的话是默认主键为guid
    {
    }
}
