using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "DateOfBirth", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "A software engineer turned writer, Marcus specializes in technology and its impact on modern society.", new DateTime(1985, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marcus", "Chen" },
                    { 2, "Sarah is a historian with a passion for uncovering forgotten stories from the past.", new DateTime(1978, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarah", "Rodriguez" },
                    { 3, "David is a science writer who makes complex topics accessible to general readers.", new DateTime(1982, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "David", "Thompson" },
                    { 4, "Elena is a novelist known for her character-driven stories and vivid descriptions.", new DateTime(1990, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elena", "Petrova" },
                    { 5, "James is a researcher and writer focusing on environmental science and sustainability.", new DateTime(1975, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "James", "Mitchell" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fiction" },
                    { 2, "Science" },
                    { 3, "History" },
                    { 4, "Technology" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Riverside Press" },
                    { 2, "Horizon Books" },
                    { 3, "Summit Publishing" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "NumberOfCopies", "PublicationYear", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 1, 15, 2022, 1, "The Digital Frontier" },
                    { 2, 12, 2021, 2, "Lost Cities of the Ancient World" },
                    { 3, 8, 2023, 3, "Quantum Computing for Beginners" },
                    { 4, 20, 2020, 1, "The Last Lighthouse Keeper" },
                    { 5, 10, 2022, 2, "Climate Change: A Global Perspective" },
                    { 6, 18, 2021, 3, "The Art of Problem Solving" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
