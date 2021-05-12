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
        public int TripOwnerId { get; set; }
        public int TripId { get; set; }
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
            TripOwnerId = this.tripService.GetFullTrip((int)Expense.TripId).TripOwnerId;
            TripId = (int)this.tripService.GetFullTrip((int)Expense.TripId).Id;
            Expenses = this.tripService.GetExpensesOfTrip((int)Expense.TripId);
            Participants = this.tripService.ExpenseService.GetParticipantsOfExpense(Expense.Id);
            var userId = await UserManager.GetUserAsync(User);

            if (userId != null)
            {
                UserId = userId.Id;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Expense = this.tripService.ExpenseService.GetExpense(Id);
            var expenseId = (int)this.tripService.GetFullTrip((int)Expense.TripId).Id;
            this.tripService.RemoveExpenseFromTrip(Id);
            return RedirectToPage("/Trip", new { id = expenseId });
        }

    }
}
