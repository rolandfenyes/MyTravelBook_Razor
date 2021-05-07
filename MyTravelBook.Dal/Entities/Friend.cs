using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class Friend
    {
        public int Id { get; set; }
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }
    }
}
