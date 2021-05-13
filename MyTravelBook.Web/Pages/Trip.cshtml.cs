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
        public List<TripOverallHeader> TripOverall { get; set; }
        public CreateTripHeader Trip { get; set; }
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

        public SelectList SelectableParticipants { get; set; }

        [BindProperty]
        public int[] SelectedParticipants { get; set; }

        public TripModel(TripService tripService, UserManager<User> userManager)
        {
            this.tripService = tripService;
            UserManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            if (Id != null)
            {
                Trip = this.tripService.GetFullTrip(Id);
                Travels = this.tripService.GetTravelsOfTrip(Id);
                Accommodations = this.tripService.GetAccommodationsOfTrip(Id);
                Expenses = this.tripService.GetExpensesOfTrip(Id);
                Participants = this.tripService.GetParticipantsOfTrip(Id);
                var userId = await UserManager.GetUserAsync(User);

                if (userId != null)
                {
                    UserId = userId.Id;
                }

                var friends = this.tripService.GetFriends(UserId).FriendsList;
                var participantIds = Participants.FriendsList.Select(p => p.FriendId);
                var friendsNotInTrip = new List<FriendHeader>();
                
                if (friends == null)
                {
                    friendsNotInTrip = new List<FriendHeader>();
                }
                else
                {
                    foreach (var friend in friends)
                    {
                        if (!participantIds.Contains(friend.FriendId))
                        {
                            friendsNotInTrip.Add(friend);
                        }
                    }
                }

                SelectableParticipants = new SelectList(friendsNotInTrip, nameof(FriendHeader.FriendId), nameof(FriendHeader.Nickname));


                TripOverall = this.tripService.GetOverallOfTrip(Id, UserId);

            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var participantIds = SelectedParticipants;
            foreach (var participantId in participantIds)
            {
                this.tripService.AddParticipantToTrip(Id, participantId);
            }
            
            return RedirectToPage("/Trip", new { id = Id });
        }

        public decimal GetTotalCosts()
        {
            var total = new decimal(0);
            foreach (var cost in TripOverall)
            {
                total += cost.Total;
            }
            return total;
        }


    }
}
