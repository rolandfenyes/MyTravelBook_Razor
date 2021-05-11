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

namespace MyTravelBook.Web.Pages.Details
{
    public class AccommodationModel : PageModel
    {
        [BindProperty]
        public AccommodationHeader NewAccommodation { get; set; }

        public SelectList SelectableParticipants { get; set; }

        [BindProperty]
        public int[] SelectedParticipants { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public int UserId { get; set; }

        private TripService tripService { get; set; }
        private UserManager<User> UserManager { get; }
        public AccommodationsHeader Accommodations { get; set; }

        public AccommodationModel(TripService tripService, UserManager<User> userManager)
        {
            this.tripService = tripService;
            UserManager = userManager;
        }

        public async Task OnGetAsync()
        {
            Accommodations = this.tripService.GetAccommodationsOfTrip(Id);
            var participants = this.tripService.GetParticipantsOfTrip(Id);
            SelectableParticipants = new SelectList(participants.FriendsList, nameof(FriendHeader.FriendId), nameof(FriendHeader.Nickname));
            var userId = await UserManager.GetUserAsync(User);

            if (userId != null)
            {
                UserId = userId.Id;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (IsNewAccommodationNotNull())
            {
                NewAccommodation = (AccommodationHeader)AddParticipantsToHeader(NewAccommodation);
                NewAccommodation.TripId = Id;
                this.tripService.CreateNewAccommodation(NewAccommodation);
            }
            return RedirectToPage("/Trip", new { id = Id });
        }

        public bool IsNewAccommodationNotNull()
        {
            return (NewAccommodation != null && NewAccommodation.Location != null && NewAccommodation.PricePerNight != null );
        }

        public Header AddParticipantsToHeader(Header header)
        {
            header.ParticipantIds = new List<int>();
            foreach (var participantId in SelectedParticipants)
            {
                header.ParticipantIds.Add(participantId);
            }
            return header;
        }


        public int GetNightsOfAccommodation(int id)
        {
            var accommodation = Accommodations.Accommodations.Where(a => a.Id == id).FirstOrDefault();
            return accommodation.Ends.DayOfYear - accommodation.Starts.DayOfYear;
        }
    }
}
