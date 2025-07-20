using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHubApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewDbSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ManagerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PlacesEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvailableTickets = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Showtime = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacesEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlacesEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlacesEvents_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,6)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PlaceEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_PlacesEvents_PlaceEventId",
                        column: x => x.PlaceEventId,
                        principalTable: "PlacesEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "Location", "ManagerId", "Name" },
                values: new object[,]
                {
                    { new Guid("33c36253-9b68-4d8a-89ae-f3276f1c3f8a"), "Sofia, Bulgaria", null, "Vasil Levski Stadium" },
                    { new Guid("54082f99-023b-4d68-89ac-44c00a0958d0"), "Varna, Bulgaria", null, "The Sea Garden" },
                    { new Guid("5ae6c761-1363-4a23-9965-171c70f935de"), "Sofia, Bulgaria", null, "Vidas Art Arena" },
                    { new Guid("844d9abd-104d-41ab-b14a-ce059779ad91"), "Plovdiv, Bulgaria", null, "The Collodrum" },
                    { new Guid("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"), "Sofia, Bulgaria", null, "Arena 8888" },
                    { new Guid("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"), "Sofia, Bulgaria", null, "Maimunarnika" },
                    { new Guid("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), "Sofia, Bulgaria", null, "South Park 2" }
                });

            migrationBuilder.InsertData(
                table: "PlacesEvents",
                columns: new[] { "Id", "AvailableTickets", "EventId", "PlaceId", "Showtime" },
                values: new object[,]
                {
                    { new Guid("130f6630-5593-4165-8e9e-de718ee1fb72"), 12000, new Guid("68fb84b9-ef2a-402f-b4fc-595006f5c275"), new Guid("5ae6c761-1363-4a23-9965-171c70f935de"), "22:15" },
                    { new Guid("30864830-db09-412a-a816-6dbaccc1374c"), 150, new Guid("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"), new Guid("33c36253-9b68-4d8a-89ae-f3276f1c3f8a"), "17:45" },
                    { new Guid("30c505d0-9833-4087-9377-43ac8ab34e07"), 1000, new Guid("777634e2-3bb6-4748-8e91-7a10b70c78ac"), new Guid("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), "13:00" },
                    { new Guid("71a411ec-d23c-4abb-b50c-75571d0a3cff"), 10000, new Guid("ae50a5ab-9642-466f-b528-3cc61071bb4c"), new Guid("8a1fdfb4-08c9-44a2-a46e-0b3c45ff57b9"), "18:30" },
                    { new Guid("a22c43b7-bd1d-46cd-b419-dba244e533cc"), 550, new Guid("811a1a9e-61a8-4f6f-acb0-55a252c2b713"), new Guid("f4c3e429-0e36-47af-99a2-0c7581a7fc67"), "17:00" },
                    { new Guid("c96549ed-7a19-4e83-856e-976cf306d611"), 120, new Guid("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"), new Guid("be80d2e4-1c91-4e86-9b73-12ef08c9c3d2"), "19:00" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserId",
                table: "Managers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Places_ManagerId",
                table: "Places",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_Name_Location",
                table: "Places",
                columns: new[] { "Name", "Location" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlacesEvents_EventId_PlaceId_Showtime",
                table: "PlacesEvents",
                columns: new[] { "EventId", "PlaceId", "Showtime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlacesEvents_PlaceId",
                table: "PlacesEvents",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PlaceEventId_UserId",
                table: "Tickets",
                columns: new[] { "PlaceEventId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "PlacesEvents");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Managers");
        }
    }
}
