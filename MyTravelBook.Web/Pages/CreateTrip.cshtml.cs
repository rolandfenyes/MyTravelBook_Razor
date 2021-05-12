using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ganss.XSS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyTravelBook.Dal.Dto;
using MyTravelBook.Dal.Entities;
using MyTravelBook.Dal.Services;

namespace MyTravelBook.Web.Pages
{
    public class CreateTripModel : PageModel
    {
        private TripService tripService { get; }
        public UserManager<User> UserManager { get; }
        public int UserId { get; set; }

        public SelectList SelectableParticipants { get; set; }

        [BindProperty]
        public int[] SelectedParticipants { get; set; }

        [BindProperty]
        public CreateTripHeader NewTrip { get; set; }

        public CreateTripModel(TripService tripService, UserManager<User> userManager)
        {
            this.tripService = tripService;
            UserManager = userManager;
        }

        public async Task OnGetAsync()
        {
            UserId = await GetUserId();
            var participants = this.tripService.GetFriends(UserId);
            SelectableParticipants = new SelectList(participants.FriendsList, nameof(FriendHeader.FriendId), nameof(FriendHeader.Nickname));

        }

        public async Task<int> GetUserId()
        {
            var userId = await UserManager.GetUserAsync(User);

            if (userId != null)
            {
                return UserId = userId.Id;
            } 
            else
            {
                return 0;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // TODO
            if (UserId == 0)
            {
                UserId = await GetUserId();
            }
            NewTrip.TripOwnerId = UserId;
            NewTrip.TripName = new HtmlSanitizer().Sanitize(NewTrip.TripName);
            NewTrip.StartDay = new HtmlSanitizer().Sanitize(NewTrip.StartDay);
            NewTrip.StartMonth = new HtmlSanitizer().Sanitize(NewTrip.StartMonth);
            NewTrip.StartYear = new HtmlSanitizer().Sanitize(NewTrip.StartYear);
            NewTrip.EndDay = new HtmlSanitizer().Sanitize(NewTrip.EndDay);
            NewTrip.EndMonth = new HtmlSanitizer().Sanitize(NewTrip.EndMonth);
            NewTrip.EndYear = new HtmlSanitizer().Sanitize(NewTrip.EndYear);
            NewTrip.Description = new HtmlSanitizer().Sanitize(NewTrip.Description);

            NewTrip.ParticipantIds = SelectedParticipants.ToList();
            var tripId = this.tripService.CreateNewTrip(NewTrip);

            return RedirectToPage("/Trip", new { id = tripId });
        }
    }
}
