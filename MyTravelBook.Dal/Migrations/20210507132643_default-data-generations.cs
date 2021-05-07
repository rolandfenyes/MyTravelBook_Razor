using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTravelBook.Dal.Migrations
{
    public partial class defaultdatagenerations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccommodationParticipants",
                columns: new[] { "Id", "AccommodationId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "ExpenseParticipants",
                columns: new[] { "Id", "ExpenseId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Description", "ExpenseName", "Location", "Price" },
                values: new object[,]
                {
                    { 1, "Hot dog + 2db Hell", "Shell", "Érd", 5f },
                    { 2, "Szendvics", "Benzinkút", "Letenye", 2f },
                    { 3, "BigTasty menü", "McDonalds", "Split", 5f },
                    { 4, "1db mangó ízű shisha", "Shisha bár", "Split", 10f },
                    { 5, "Hajókirándulás Hvar szigetére.", "Hajókirándulás", "Split", 30f }
                });

            migrationBuilder.InsertData(
                table: "TravelParticipants",
                columns: new[] { "Id", "TravelId", "UserId" },
                values: new object[,]
                {
                    { 4, 4, 1 },
                    { 3, 3, 1 },
                    { 2, 2, 1 },
                    { 1, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Travels",
                columns: new[] { "Id", "Consumption", "Departure", "Destination", "Distance", "FuelPrice", "LuggagePrice", "SeatPrice", "TicketPrice", "TransportType" },
                values: new object[,]
                {
                    { 3, 6.7f, "Érd", "Balatonvilágos", 76.3f, 1.33f, 0f, 0f, 0f, 2 },
                    { 2, 6.7f, "Split", "Érd", 734f, 1.33f, 0f, 0f, 0f, 2 },
                    { 1, 6.7f, "Érd", "Split", 734f, 1.33f, 0f, 0f, 0f, 2 },
                    { 4, 6.7f, "Balatonvilágos", "Érd", 76.3f, 1.33f, 0f, 0f, 0f, 2 }
                });

            migrationBuilder.InsertData(
                table: "TripExpenses",
                columns: new[] { "Id", "ExpenseId", "TripId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "TripTravels",
                columns: new[] { "Id", "TravelId", "TripId" },
                values: new object[,]
                {
                    { 3, 3, 2 },
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 4, 4, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccommodationParticipants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AccommodationParticipants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExpenseParticipants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExpenseParticipants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExpenseParticipants",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExpenseParticipants",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExpenseParticipants",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TravelParticipants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TravelParticipants",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TravelParticipants",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TravelParticipants",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Travels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TripExpenses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TripExpenses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TripExpenses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TripExpenses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TripExpenses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TripTravels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TripTravels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TripTravels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TripTravels",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
