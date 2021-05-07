using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
    }
}
