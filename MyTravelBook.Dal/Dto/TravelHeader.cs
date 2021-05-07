using System.Collections.Generic;


namespace MyTravelBook.Dal.Dto
{
    public class TravelHeader
    {
        public int Id { get; set; }
        public int? TripId { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string TransportType { get; set; }
        public float TicketPrice { get; set; }
        public float SeatPrice { get; set; }
        public float LuggagePrice { get; set; }

        public float Distance { get; set; }
        public float Consumption { get; set; }
        public float FuelPrice { get; set; }
        public List<int>? ParticipantIds { get; set; }
    }
}
