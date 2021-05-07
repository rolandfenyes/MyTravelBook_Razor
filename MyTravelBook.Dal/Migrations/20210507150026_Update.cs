using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTravelBook.Dal.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TripParticipants",
                columns: new[] { "Id", "TripId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "TripParticipants",
                columns: new[] { "Id", "TripId", "UserId" },
                values: new object[] { 2, 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TripParticipants",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TripParticipants",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
