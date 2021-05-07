using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTravelBook.Dal.Dto;
using MyTravelBook.Dal.Services;

namespace MyTravelBook.Web.Pages
{
    public class TripModel : PageModel
    {
        public TripHeader Trip { get; set; }
        public TripService tripService { get; set; }

        [BindProperty (SupportsGet = true)]
        public int Id { get; set; }

        public TripModel(TripService tripService)
        {
            this.tripService = tripService;
        }

        public void OnGet()
        {
            Trip = this.tripService.GetTrip(Id);
        }
    }
}
