using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MyCookieAuthSample.Models;

namespace MyCookieAuthSample.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser,ApplicationUserRole,int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
             
        }

    }
}
