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
    public class ExpenseDetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public int UserId { get; set; }
        private TripService tripService { get; }
        private UserManager<User> UserManager { get; }

        public ExpensesHeader Expenses { get; set; }
        public ExpenseHeader Expense { get; set; }
        public FriendsHeader Participants { get; set; }

        public ExpenseDetailsModel(TripService tripService, UserManager<User> userManager)
        {
            this.tripService = tripService;
            UserManager = userManager;
        }

        public async Task OnGetAsync()
        {
            Expense = this.tripService.ExpenseService.GetExpense(Id);
            
            Expenses = this.tripService.GetExpensesOfTrip((int)Expense.TripId);
            Participants = this.tripService.GetParticipantsOfTrip((int)Expense.TripId);
            var userId = await UserManager.GetUserAsync(User);

            if (userId != null)
            {
                UserId = userId.Id;
            }
        }
    }
}
