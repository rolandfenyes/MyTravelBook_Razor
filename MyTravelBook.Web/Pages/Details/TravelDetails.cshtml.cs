using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTravelBook.Dal.Dto;
using MyTravelBook.Dal.Entities;
using MyTravelBook.Dal.Services;

namespace MyTravelBook.Web.Pages.Details
{
    public class TravelDetailsModel : PageModel
    {
        [BindProperty (SupportsGet = true)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TripOwnerId { get; set; }
        public int TripId { get; set; }
        private TripService tripService { get; }
        private UserManager<User> UserManager { get; }

        public TravelsHeader Travels { get; set; }
        public TravelHeader Travel { get; set; }
        public FriendsHeader Participants { get; set; }
        public string TravelType { get; set; }

        public TravelDetailsModel(TripService tripService, UserManager<User> userManager)
        {
            this.tripService = tripService;
            UserManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Travel = this.tripService.TravelService.GetTravel(Id);
            TripOwnerId = this.tripService.GetFullTrip((int)Travel.TripId).TripOwnerId;
            TripId = (int)this.tripService.GetFullTrip((int)Travel.TripId).Id;
            SetTravelType();
            Travels = this.tripService.GetTravelsOfTrip((int)Travel.TripId);
            Participants = this.tripService.TravelService.GetParticipantsOfTravel(Travel.Id);
            var userId = await UserManager.GetUserAsync(User);

            if (userId != null)
            {
                UserId = userId.Id;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Travel = this.tripService.TravelService.GetTravel(Id);
            TripId = (int)this.tripService.GetFullTrip((int)Travel.TripId).Id;
            this.tripService.RemoveTravelFromTrip(Id);
            return RedirectToPage("/Trip", new { id = TripId});
        }

        public void SetTravelType()
        {
            switch (Travel.TransportType)
            {
                case 0: TravelType = "Plane";
                    break;
                case 1: TravelType = "Public transport";
                    break;
                case 2: TravelType = "Car";
                    break;
            }
        }
    }
}
