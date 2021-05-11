using System.Collections.Generic;

namespace MyTravelBook.Dal.Dto
{
    public class ExpenseHeader : Header
    {
        public int Id { get; set; }
        public int? TripId { get; set; }
        public string Location { get; set; }
        public string ExpenseName { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public decimal PricePerCapita { get; set; }
    }
}