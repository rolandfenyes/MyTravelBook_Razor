using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Dto
{
    public class StatisticsHeader
    {
        public int UserId { get; set; }
        public int FriendsCount { get; set; }
        public int TripsCount { get; set; }
        public int NightsCount { get; set; }
        public int LongestTrip { get; set; }
        public string MostUsedTransportationType { get; set; }
        public float AverageDailyCost { get; set; }
        public float CheapestTrip { get; set; }
        public float MostExpensiveTrip { get; set; }
        public float TotalOutcome { get; set; }
    }
}
