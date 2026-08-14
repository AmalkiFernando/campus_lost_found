using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CampusLostFound.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LostFoundItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    DateReported = table.Column<DateOnly>(type: "date", nullable: false),
                    Type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Status = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ContactName = table.Column<string>(type: "text", nullable: false),
                    ContactEmail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LostFoundItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LostFoundItems",
                columns: new[] { "Id", "Category", "ContactEmail", "ContactName", "DateReported", "Description", "Location", "Status", "Title", "Type" },
                values: new object[,]
                {
                    { 1, "Accessories", "alex.chen@example.edu", "Alex Chen", new DateOnly(2026, 8, 10), "Small black leather wallet with a few cards inside.", "Library", "ACTIVE", "Black Wallet", "LOST" },
                    { 2, "Personal Items", "priya.nair@example.edu", "Priya Nair", new DateOnly(2026, 8, 11), "Blue metal water bottle with a dented cap.", "Cafeteria", "ACTIVE", "Blue Water Bottle", "FOUND" },
                    { 3, "Documents", "jordan.lee@example.edu", "Jordan Lee", new DateOnly(2026, 8, 12), "University ID card, name partially visible.", "Computer Lab", "ACTIVE", "Student ID Card", "LOST" },
                    { 4, "Bags", "sam.ortiz@example.edu", "Sam Ortiz", new DateOnly(2026, 8, 12), "Black backpack with a laptop sleeve and a keychain.", "Main Auditorium", "ACTIVE", "Black Backpack", "FOUND" },
                    { 5, "Electronics", "maria.gomez@example.edu", "Maria Gomez", new DateOnly(2026, 8, 13), "White wireless earbuds in a charging case.", "Lecture Hall", "ACTIVE", "Wireless Earbuds", "LOST" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LostFoundItems");
        }
    }
}
