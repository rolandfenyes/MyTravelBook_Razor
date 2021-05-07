using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class Budget
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Total { get; set; }
        public float Spent { get; set; }
        public float Remains { get; set; }
        public float Transportation { get; set; }
        public float Accommodation { get; set; }
        public float Other { get; set; }
    }
}
