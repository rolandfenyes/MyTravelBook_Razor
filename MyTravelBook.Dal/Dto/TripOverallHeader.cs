using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Dto
{
    public class TripOverallHeader
    {
        public string Location { get; set; }
        public int Nights { get; set; }
        public decimal AccommodationPrice { get; set; }
        public decimal TravelCosts { get; set; }
        public decimal Expenses { get; set; }
        public decimal Total { get; set; }
    }
}
