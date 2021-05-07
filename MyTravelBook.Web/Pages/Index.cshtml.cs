using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MyTravelBook.Dal.Dto;
using MyTravelBook.Dal.Entities;
using MyTravelBook.Dal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTravelBook.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public IndexModel(ILogger<IndexModel> logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IEnumerable<TripHeader> Trips { get; set; }

        public TripService TripService { get; set; }

        public async Task<IActionResult> OnGet([FromServices] TripService tripService)
        {
            if (this.signInManager.IsSignedIn(User))
            {
                TripService = tripService;
                var userId = await this.userManager.GetUserAsync(User);
                Trips = await tripService.GetTrips(userId.Id);
            }
            
            return Page();
        }
    }
}
