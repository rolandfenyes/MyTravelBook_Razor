using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public bool IdCard { get; set; }
        public bool InternationalPassport { get; set; }
        public bool DrivingLicense { get; set; }
        public bool HealthCard { get; set; }
    }
}
