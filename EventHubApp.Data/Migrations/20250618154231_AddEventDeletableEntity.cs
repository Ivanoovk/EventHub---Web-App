using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventHubApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEventDeletableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Event identifier"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Event title"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Event type"),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false, comment: "Event release date"),
                    Sponsor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Event sponsor"),
                    Duration = table.Column<int>(type: "int", nullable: false, comment: "Event duration"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Event description"),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true, comment: "Event image url from the image store"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "Shows if Event is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                },
                comment: "Event in the system");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
