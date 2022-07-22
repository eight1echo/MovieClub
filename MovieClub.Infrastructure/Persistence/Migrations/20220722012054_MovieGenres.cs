﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieClub.Infrastructure.Persistence.Migrations
{
    public partial class MovieGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genres",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Movies");
        }
    }
}