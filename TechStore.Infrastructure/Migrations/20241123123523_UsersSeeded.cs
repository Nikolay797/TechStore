using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UsersSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0b129438-03c0-4f93-8d80-16fa6d4afa54", 0, "ba987df9-fed7-4ea0-b56b-76e17be675ce", "bestUser@mail.com", false, "BestUser-FN", "BestUser-LN", false, null, "BESTUSER@MAIL.COM", "BESTUSER", "AQAAAAIAAYagAAAAEIq+VIcdMD2YlcKqCNhvQX0A2G/Lev0VP2plVRLyREvrbKdBA47Md4tZ72BtoF2idQ==", null, false, "0f387c5e-df1a-483a-97dc-c0a90aabc1c4", false, "bestUser" },
                    { "69d44205-edfe-47b9-8d27-6366c018f434", 0, "f90d65d4-11dd-4547-b5a5-3a9b2f116940", "admin@mail.com", false, "Admin-FN", "Admin-LN", false, null, "ADMIN@MAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEHgM/W5TF/iyr9qvoYvAgDRXao1e/6hKQSDzBe/hj9obfR7j3TfUC2dWx+QHQuaaQQ==", null, false, "b4e7a088-3980-4877-a407-ef959d68fdb4", false, "admin" },
                    { "80c1bdd8-73f9-4713-a939-090e9e07281b", 0, "e9dbc9ab-a69e-43d6-a099-1be4ba61efdb", "user@mail.com", false, "User-FN", "User-LN", false, null, "USER@MAIL.COM", "USER", "AQAAAAIAAYagAAAAEApchckbpjT5VH89ka7U8v6hr7NU3WZ8EKigsTQWdZ5geEG7z+21DvAlaWBAoHgl4w==", null, false, "f6c79908-3420-4262-bfbc-741b2057b4f0", false, "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b129438-03c0-4f93-8d80-16fa6d4afa54");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69d44205-edfe-47b9-8d27-6366c018f434");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80c1bdd8-73f9-4713-a939-090e9e07281b");
        }
    }
}
