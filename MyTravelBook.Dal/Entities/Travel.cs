using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class Travel
    {
        public int Id { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public TravellingType TransportType { get; set; }
        public float TicketPrice { get; set; }
        public float SeatPrice { get; set; }
        public float LuggagePrice { get; set; }
        public float Distance { get; set; }
        public float Consumption { get; set; }
        public float FuelPrice { get; set; }
        public float HighwayFee { get; set; }
    }
}
