using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTravelBook.Dal.Migrations
{
    public partial class highwayfee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "HighwayFee",
                table: "Travels",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HighwayFee",
                table: "Travels");
        }
    }
}
