using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Dto
{
    public class UserHeader
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
