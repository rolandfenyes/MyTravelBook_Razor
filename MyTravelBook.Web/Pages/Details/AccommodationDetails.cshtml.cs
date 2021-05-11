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
            Accommodations = this.tripService.GetAccommodationsOfTrip((int)Accommodation.TripId);
            Participants = this.tripService.GetParticipantsOfTrip((int)Accommodation.TripId);
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
    }
}
