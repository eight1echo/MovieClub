using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieClub.Infrastructure.Persistence.Migrations
{
    public partial class MeetupLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Meetups",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Meetups");
        }
    }
}
