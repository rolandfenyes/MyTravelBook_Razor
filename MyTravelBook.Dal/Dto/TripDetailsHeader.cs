using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Dto
{
    public class TripDetailsHeader
    {
        public int TripId { get; set; }
        public bool IDCard { get; set; }
        public bool InternationalPassport { get; set; }
        public bool DrivingLicense { get; set; }
        public bool HealthCard { get; set; }
    }
}
