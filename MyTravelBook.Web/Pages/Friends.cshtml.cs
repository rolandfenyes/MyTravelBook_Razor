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

namespace MyTravelBook.Web.Pages
{
    public class FriendsModel : PageModel
    {
        private TripService tripService { get; }
        private UserManager<User> UserManager { get; }

        public int UserId { get; set; }
        public FriendsHeader Friends { get; set; }

        [BindProperty]
        public FriendHeader NewFriend { get; set; }

        public FriendsModel(TripService tripService, UserManager<User> userManager)
        {
            this.tripService = tripService;
            UserManager = userManager;
        }

        public async Task OnGetAsync()
        {
            UserId = await GetUserId();
            Friends = this.tripService.GetFriends(UserId);
        }

        public async Task<int> GetUserId()
        {
            var user = await UserManager.GetUserAsync(User);
            return user.Id;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            UserId = await GetUserId();
            this.tripService.AddFriend(UserId, NewFriend.Nickname);
            return RedirectToPage("Index");
        }
    }
}
