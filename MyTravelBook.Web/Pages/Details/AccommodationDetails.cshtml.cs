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
    public class AccommodationDetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TripOwnerId { get; set; }
        public int TripId { get; set; }
        private TripService tripService { get; }
        private UserManager<User> UserManager { get; }

        public AccommodationsHeader Accommodations { get; set; }
        public AccommodationHeader Accommodation { get; set; }
        public FriendsHeader Participants { get; set; }
        public string AccommodationType { get; set; }

        public AccommodationDetailsModel(TripService tripService, UserManager<User> userManager)
        {
            this.tripService = tripService;
            UserManager = userManager;
        }
        public async Task OnGetAsync()
        {
            Accommodation = this.tripService.AccommodationService.GetAccommodation(Id);
            SetAccommodationType();
            TripOwnerId = this.tripService.GetFullTrip((int)Accommodation.TripId).TripOwnerId;
            TripId = (int)this.tripService.GetFullTrip((int)Accommodation.TripId).Id;
            Accommodations = this.tripService.GetAccommodationsOfTrip((int)Accommodation.TripId);
            Participants = this.tripService.AccommodationService.GetParticipantsOfAccommodation(Accommodation.Id);
            var userId = await UserManager.GetUserAsync(User);

            if (userId != null)
            {
                UserId = userId.Id;
            }
        }

        public void SetAccommodationType()
        {
            switch (Accommodation.AccommodationType)
            {
                case 0: AccommodationType = "Own apartment";
                    break;
                case 1: AccommodationType = "Rented";
                    break;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Accommodation = this.tripService.AccommodationService.GetAccommodation(Id);
            TripId = (int)this.tripService.GetFullTrip((int)Accommodation.TripId).Id;
            this.tripService.RemoveAccommodationFromTrip(Id);
            return RedirectToPage("/Trip", new { id = TripId });
        }

    }
}
