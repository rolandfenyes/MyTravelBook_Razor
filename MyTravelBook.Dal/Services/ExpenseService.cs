using MyTravelBook.Dal.Dto;
using MyTravelBook.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyTravelBook.Dal.Services
{
    public class ExpenseService
    {
        public MyDbContext DbContext { get; set; }

        public ExpenseService(MyDbContext dbContext)
        {
            DbContext = dbContext;
        }

        // Create

        public void CreateNewExpense(ExpenseHeader expenseHeader)
        {
            var expense = new Expense
            {
                Location = expenseHeader.Location,
                ExpenseName = expenseHeader.ExpenseName,
                Description = expenseHeader.Description,
                Price = expenseHeader.Price
            };
            DbContext.Expenses.Add(expense);
            DbContext.SaveChanges();

            DbContext.TripExpenses.Add(
                new TripExpense
                {
                    TripId = expenseHeader.TripId.Value,
                    ExpenseId = expense.Id
                });

            foreach (var id in expenseHeader.ParticipantIds)
            {
                DbContext.ExpenseParticipants.Add(
                new ExpenseParticipants
                {
                    ExpenseId = expense.Id,
                    UserId = id
                });
            }
            DbContext.SaveChanges();

        }

        // Read

        public ExpenseHeader GetExpense(int expenseId)
        {
            var expense = DbContext.Expenses.Where(e => e.Id == expenseId).FirstOrDefault();
            var participants = DbContext.ExpenseParticipants.Where(e => e.ExpenseId == expenseId).ToList();
            var participantIds = new List<int>();
            foreach (var participant in participants)
            {
                participantIds.Add(participant.UserId);
            }
            var pricePerCapita = CalculateCost(expense, participantIds.Count);
            return new ExpenseHeader
            {
                Id = expense.Id,
                TripId = DbContext.TripExpenses.Where(e => e.ExpenseId == expenseId).FirstOrDefault().TripId,
                Location = expense.Location,
                ExpenseName = expense.ExpenseName,
                Description = expense.Description,
                Price = expense.Price,
                ParticipantIds = participantIds,
                PricePerCapita = pricePerCapita
            };
        }

        public decimal CalculateCost(Expense expense, int numOfParticipants)
        {
            return new decimal(expense.Price / numOfParticipants);
        }

        public FriendsHeader GetParticipantsOfExpense(int expenseId)
        {
            var participants = DbContext.ExpenseParticipants.Where(t => t.ExpenseId == expenseId).ToList();
            var participantHeaders = new FriendsHeader();
            participantHeaders.FriendsList = new List<FriendHeader>();
            foreach (var user in participants)
            {
                participantHeaders.FriendsList.Add(
                    new FriendHeader
                    {
                        FriendId = user.UserId,
                        Nickname = DbContext.Users.Where(u => u.Id == user.UserId).FirstOrDefault().Name
                    });
            }
            return participantHeaders;
        }

        // Update

        public void UpdateExistingExpense(ExpenseHeader expenseHeader)
        {
            var expense = DbContext.Expenses.Where(e => e.Id == expenseHeader.Id).FirstOrDefault();
            expense.Location = expenseHeader.Location;
            expense.ExpenseName = expenseHeader.ExpenseName;
            expense.Description = expenseHeader.Description;
            expense.Price = expenseHeader.Price;

            DbContext.Expenses.Update(expense);

            var expenseParticipants = DbContext.ExpenseParticipants.Where(e => e.ExpenseId == expenseHeader.Id).ToList();
            foreach (var id in expenseHeader.ParticipantIds)
            {
                var isIdInExpense = expenseParticipants.Find(t => t.UserId == id);
                if (isIdInExpense == null)
                {
                    DbContext.ExpenseParticipants.Add(
                        new ExpenseParticipants
                        {
                            ExpenseId = expenseHeader.Id,
                            UserId = id
                        });
                }
            }
            DbContext.SaveChanges();
        }

        // Remove

        public void RemoveExistingExpense(int expenseId)
        {
            // TODO
        }

    }
}
