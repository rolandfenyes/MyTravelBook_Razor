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

namespace MyTravelBook.Web.Pages.Edit
{
    public class TripEditorModel : PageModel
    {
        [BindProperty (SupportsGet = true)]
        public int Id { get; set; }

        public int UserId { get; set; }

        [BindProperty]
        public CreateTripHeader Trip { get; set; }

        public SelectList AllParticipants { get; set; }

        public SelectList SelectableParticipants { get; set; }

        [BindProperty]
        public int[] SelectedParticipants { get; set; }

        private TripService tripService { get; }
        private UserManager<User> UserManager { get; }

        public TripEditorModel(TripService tripService, UserManager<User> userManager)
        {
            this.tripService = tripService;
            UserManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            Trip = this.tripService.GetFullTrip(Id);
            
            UserId = await GetUserId();
            
            AllParticipants = new SelectList(this.tripService.GetParticipantsOfTrip(Id).FriendsList, nameof(FriendHeader.FriendId), nameof(FriendHeader.Nickname));
            var participants = this.tripService.GetParticipantsOfTrip(Id).FriendsList;
            participants.Remove(participants.Where(f => f.FriendId == Id).FirstOrDefault());
            SelectableParticipants = new SelectList(participants, nameof(FriendHeader.FriendId), nameof(FriendHeader.Nickname));

            return Page();
        }

        public async Task<int> GetUserId()
        {
            var userId = await UserManager.GetUserAsync(User);
            return userId.Id;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedParticipants != null && SelectedParticipants.Length != 0)
            {
                if (UserId == 0)
                {
                    UserId = await GetUserId();
                }
                this.tripService.RemoveParticipantFromTrip(Id, SelectedParticipants[0]);
            }
            else
            {
                Trip.Id = Id;
                this.tripService.UpdateExistingTrip(Trip);
            }
            return RedirectToPage("/Trip", new { id = Id });
        }
    }
}
