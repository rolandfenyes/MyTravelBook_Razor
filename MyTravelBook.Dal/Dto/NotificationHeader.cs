using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Dto
{
    public class NotificationHeader
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
    }
}
