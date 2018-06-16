using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.ViewModels
{
    public class LoginViewModel : IdentityUser<int>
    {
        public string EMail { get; set; }
        public string Password { get; set; }
    }
}
