using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieClub.Infrastructure.Persistence.Migrations
{
    public partial class RemoveUnusedMovieProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackdropURL",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "IMDbId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Revenue",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "LanguageTitle",
                table: "Movies",
                newName: "IMDbPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IMDbPath",
                table: "Movies",
                newName: "LanguageTitle");

            migrationBuilder.AddColumn<string>(
                name: "BackdropURL",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Budget",
                table: "Movies",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IMDbId",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Revenue",
                table: "Movies",
                type: "bigint",
                nullable: true);
        }
    }
}
