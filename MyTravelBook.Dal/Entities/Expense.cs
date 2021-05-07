using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string ExpenseName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}
