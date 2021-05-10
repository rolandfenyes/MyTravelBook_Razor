using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyTravelBook.Dal.Dto;
using MyTravelBook.Dal.Entities;
using MyTravelBook.Dal.Services;

namespace MyTravelBook.Web.Pages
{
    public class TripModel : PageModel
    {
        public TripHeader Trip { get; set; }
        public TripDetailsHeader TripDetails { get; set; }
        public TravelsHeader Travels { get; set; }
        public AccommodationsHeader Accommodations { get; set; }
        public ExpensesHeader Expenses { get; set; }
        public FriendsHeader Participants { get; set; }
        public TripService tripService { get; set; }
        public UserManager<User> UserManager { get; }

        [BindProperty (SupportsGet = true)]
        public int Id { get; set; }

        public int UserId{ get; set; }

        [BindProperty]
        public TravelHeader NewTravel { get; set; }

        [BindProperty]
        public AccommodationHeader NewAccommodation { get; set; }

        [BindProperty]
        public ExpenseHeader NewExpense { get; set; }

        public TripModel(TripService tripService, UserManager<User> userManager)
        {
            this.tripService = tripService;
            UserManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            Trip = this.tripService.GetTrip(Id);
            Travels = this.tripService.GetTravelsOfTrip(Id);
            Accommodations = this.tripService.GetAccommodationsOfTrip(Id);
            Expenses = this.tripService.GetExpensesOfTrip(Id);
            Participants = this.tripService.GetParticipantsOfTrip(Id);
            var userId = await UserManager.GetUserAsync(User);
            if (userId != null)
            {
                UserId = userId.Id;
            }
            return Page();
        }

        public int GetNightsAtDestination(string destination)
        {
            var accommodations = Accommodations.Accommodations.Where(a => a.Location == destination && a.TripId == Id).ToList();
            
            var accommodation = accommodations.Where(a => a.ParticipantIds.Contains(UserId)).FirstOrDefault();

            if (accommodation != null)
            {
                return accommodation.Ends.DayOfYear - accommodation.Starts.DayOfYear;
            }
            else
            {
                return 0;
            }
        }

        public decimal GetAccommodationFees(string destination)
        {
            var accommodationFees = Accommodations.Accommodations.Where(a => a.Location == destination && a.TripId == Id).ToList();
            var accommodationFee = accommodationFees.Where(a => a.ParticipantIds.Contains(UserId)).FirstOrDefault();

            if (accommodationFee != null)
            {
                return decimal.Round(new decimal(accommodationFee.PricePerNight * (accommodationFee.Ends.DayOfYear - accommodationFee.Starts.DayOfYear)));
            }
            else
            {
                return 0;
            }
        }

        public decimal GetTravelFees(string destination)
        {
            var travelFees = Travels.Travels.Where(t => t.Destination == destination && t.TripId == Id).ToList();
            var travelFee = travelFees.Where(t => t.ParticipantIds.Contains(UserId)).FirstOrDefault();

            if (travelFee != null)
            {
                if (travelFee.TicketPrice != 0F)
                {
                    return decimal.Round(new decimal(travelFee.TicketPrice + travelFee.SeatPrice + travelFee.LuggagePrice));
                }
                else if (travelFee.FuelPrice != 0F)
                {
                    return decimal.Round(new decimal((travelFee.Distance / 100 * travelFee.Consumption * travelFee.FuelPrice)/travelFee.ParticipantIds.Count));
                }
            }
            return new decimal(0);
            
        }

        public decimal GetExpenses(string destination)
        {
            var expenses = Expenses.Expenses.Where(e => e.Location == destination && e.TripId == Id).ToList();
            var expense = expenses.Where(e => e.ParticipantIds.Contains(UserId)).ToList();

            var totalExpense = new decimal(0);
            foreach (var e in expense)
            {
                totalExpense += new decimal(e.Price / e.ParticipantIds.Count);
            }
            return decimal.Round(totalExpense);
        }

        public decimal GetTotal(string destination)
        {
            var acFees = GetAccommodationFees(destination);
            var trFees = GetTravelFees(destination);
            var oFees = GetExpenses(destination);
            return decimal.Round(acFees + trFees + oFees);
        }
    }
}
