using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTravelBook.Dal.Migrations
{
    public partial class tripupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "TripOwnerId",
                table: "Trips",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Trips",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Splitbe megyünk");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Balcsira megyünk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "TripOwnerId",
                table: "Trips",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                column: "DocumentId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                column: "DocumentId",
                value: 2);
        }
    }
}
