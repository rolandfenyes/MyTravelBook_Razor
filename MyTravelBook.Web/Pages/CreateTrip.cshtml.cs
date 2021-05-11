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
    public class CreateTripModel : PageModel
    {
        private TripService tripService { get; }
        private UserManager<User> UserManager { get; }
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
            var userId = await UserManager.GetUserAsync(User);

            if (userId != null)
            {
                UserId = userId.Id;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // TODO

            NewTrip.TripOwnerId = UserId;
            NewTrip.ParticipantIds = SelectedParticipants.ToList();
            var tripId = this.tripService.CreateNewTrip(NewTrip);

            return RedirectToPage("/Trip", new { id = tripId });
        }
    }
}
