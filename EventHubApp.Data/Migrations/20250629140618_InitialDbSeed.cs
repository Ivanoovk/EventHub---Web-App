using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHubApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "Duration", "ImageUrl", "ReleaseDate", "Sponsor", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"), "Spanish flamenco sensation Chambao and La Marie are returning to Bulgarian soil for a magical tour, starting in Sofia on June 26th at Maimunarnika, filled with smiles, positive energy and unforgettable moments.", 2, "https://cdn-az.allevents.in/events6/banners/bd3bb8c233ade380597b152ac9841527f036a4b3873cb59bbf89b17ad8bdd15f-rimg-w1200-h628-dcdcd3c0-gmir?v=1750693364", new DateOnly(2025, 6, 26), "Maimunarnika", "CHAMBAO Live in Sofia", "Dancing" },
                    { new Guid("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"), "Guns N' Roses have announced their long-awaited return to Sofia as part of their massive tour in 2025. On July 21, Vasil Levski Stadium will transform into an arena of rock greatness, where fans will be able to experience the magic of the iconic anthems of Axl Rose, Slash and Duff McKagan.", 1, "https://cdn-az.allevents.in/events9/banners/2b7fe9b1de287bd9803132ef352770734d64b76479ccc851124629a61a1e8847-rimg-w1200-h628-dc010101-gmir?v=1750650103", new DateOnly(2025, 6, 21), "EventHub", "Guns N' Roses", "Entertaining" },
                    { new Guid("68fb84b9-ef2a-402f-b4fc-595006f5c275"), "On July 1, 2025, at Vidas Art Arena, Borisova Gradina, one of the most influential figures on the contemporary music scene will present a show that combines the best of Latin rhythms, urban sounds and club energy. Summer 2025 promises to be even hotter with the soundtrack of the hot Colombian star J Balvin.", 1, "https://cdn-az.allevents.in/events4/banners/68d1519bb5a0c93c4aa36e8127f3d32e427a50e45a09e00b79a1e8abafdb1d4c-rimg-w1200-h628-dc030616-gmir?v=1750733582", new DateOnly(2025, 7, 1), "Fest Team", "J Balvin @ Sofia Solid - Vidas Art Arena", "Music" },
                    { new Guid("777634e2-3bb6-4748-8e91-7a10b70c78ac"), "A to JazZ Festival 14th Edition, 3-4-5-6 July 2025, South Park 2, Sofia, Bulgaria.\r\nA to JazZ Festival is FOUR DAYS of diverse music, culture and art, fun, education, and environmental responsibility!\r\nMore info: www.atojazz.bg", 2, "https://cdn-az.allevents.in/events8/banners/effe12725d73373d6229ebf5a38957238278d7f2ae2e066eb422670b44b7b131-rimg-w1200-h444-dcf08ca6-gmir?v=1750237454", new DateOnly(2025, 7, 3), "JazZ Fondation", "A to JazZ 2025", "Music" },
                    { new Guid("811a1a9e-61a8-4f6f-acb0-55a252c2b713"), "Welcome to the sixth edition of Sofia Graffiti Battle, which this year will begin the transformation of the underpass at the entrance to South Park!", 2, "https://cdn-az.allevents.in/events5/banners/2908baee49bb75a5e82e011b2abed4940932618e7e6050f188dd35340ee004ba-rimg-w1200-h600-dc8ebf2f-gmir?v=1750638902", new DateOnly(2025, 6, 28), "Sofia Grafiti Tour", "SOFIA URBAN ART: Green walls of Sofia", "Art" },
                    { new Guid("ae50a5ab-9642-466f-b528-3cc61071bb4c"), "Get ready for three unforgettable days of music, energy, and festival magic! ✨\r\n\r\nSofia Live Fest returns in 2025 for its biggest edition yet — with Massive Attack headlining June 29 in their long-awaited return to the region!", 7, "https://cdn-az.allevents.in/events3/banners/cd2b2df36cf25faf4191a726e93b883ca6a8d7469b5b9372daa6ff0892064a81-rimg-w1200-h628-dcc3a5cf-gmir?v=1750226192", new DateOnly(2025, 6, 27), "Fan Zone", "Sofia Live Fest 2025", "Entertaining" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("68fb84b9-ef2a-402f-b4fc-595006f5c275"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("777634e2-3bb6-4748-8e91-7a10b70c78ac"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("811a1a9e-61a8-4f6f-acb0-55a252c2b713"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("ae50a5ab-9642-466f-b528-3cc61071bb4c"));
        }
    }
}
