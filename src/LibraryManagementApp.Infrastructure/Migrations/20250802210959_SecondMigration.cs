using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Biography", "FirstName", "LastName" },
                values: new object[] { "AutherBiography1", "AutherName1", "AutherLastName1" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Biography", "FirstName", "LastName" },
                values: new object[] { "AutherBiography2", "AutherName2", "AutherLastName2" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Biography", "FirstName", "LastName" },
                values: new object[] { "AutherBiography3", "AutherName3", "AutherLastName3" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Biography", "FirstName", "LastName" },
                values: new object[] { "AutherBiography4", "AutherName4", "AutherLastName4" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Biography", "FirstName", "LastName" },
                values: new object[] { "AutherBiography5", "AutherName5", "AutherLastName5" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "BookTitle1");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "BookTitle2");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "BookTitle3");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "Title",
                value: "BookTitle4");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "Title",
                value: "BookTitle5");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "Title",
                value: "BookTitle6");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Publisher 1");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Publisher 2");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Publisher 3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Biography", "FirstName", "LastName" },
                values: new object[] { "A software engineer turned writer, Marcus specializes in technology and its impact on modern society.", "Marcus", "Chen" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Biography", "FirstName", "LastName" },
                values: new object[] { "Sarah is a historian with a passion for uncovering forgotten stories from the past.", "Sarah", "Rodriguez" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Biography", "FirstName", "LastName" },
                values: new object[] { "David is a science writer who makes complex topics accessible to general readers.", "David", "Thompson" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Biography", "FirstName", "LastName" },
                values: new object[] { "Elena is a novelist known for her character-driven stories and vivid descriptions.", "Elena", "Petrova" });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Biography", "FirstName", "LastName" },
                values: new object[] { "James is a researcher and writer focusing on environmental science and sustainability.", "James", "Mitchell" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "The Digital Frontier");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Lost Cities of the Ancient World");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Quantum Computing for Beginners");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                column: "Title",
                value: "The Last Lighthouse Keeper");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                column: "Title",
                value: "Climate Change: A Global Perspective");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6,
                column: "Title",
                value: "The Art of Problem Solving");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Riverside Press");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Horizon Books");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Summit Publishing");
        }
    }
}
