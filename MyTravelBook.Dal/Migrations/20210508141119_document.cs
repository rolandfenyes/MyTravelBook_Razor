using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTravelBook.Dal.Migrations
{
    public partial class document : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "DrivingLicense", "HealthCard", "IdCard", "InternationalPassport" },
                values: new object[] { 1, true, true, true, false });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "DrivingLicense", "HealthCard", "IdCard", "InternationalPassport" },
                values: new object[] { 2, true, true, true, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
