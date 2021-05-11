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
    public class TravelModel : PageModel
    {
        [BindProperty]
        public TravelHeader NewTravel { get; set; }

        public SelectList SelectableParticipants { get; set; }

        [BindProperty]
        public int[] SelectedParticipants { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public int UserId { get; set; }

        private TripService tripService { get; set; }
        private UserManager<User> UserManager { get; }
        public TravelsHeader Travels { get; set; }

        public TravelModel(TripService tripService, UserManager<User> userManager)
        {
            this.tripService = tripService;
            UserManager = userManager;
        }

        public async Task OnGetAsync()
        {
            Travels = this.tripService.GetTravelsOfTrip(Id);
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
            if (IsNewTravelNotNull())
            {
                NewTravel = (TravelHeader)AddParticipantsToHeader(NewTravel);
                NewTravel.TripId = Id;
                this.tripService.CreateNewTravel(NewTravel);
            }
            return RedirectToPage("/Trip", new { id = Id});
        }
        public bool IsNewTravelNotNull()
        {
            return (NewTravel != null && NewTravel.Departure != null && NewTravel.Destination != null);
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
    }
}
