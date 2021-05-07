using System;
using System.Collections.Generic;
using System.Text;

namespace MyTravelBook.Dal.Entities
{
    public class ExpenseParticipants
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
    }
}
