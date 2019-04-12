using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApiCore.Models
{
    public class ApplicationUser:IdentityUser
    {

        //Custom properties added to identifyUser
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
