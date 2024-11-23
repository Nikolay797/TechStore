using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RolesSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35d3a37a-f7f6-49a3-8022-8bd9044e90ac", null, "BestUser", "BESTUSER" },
                    { "c6e8e5fc-103e-4c47-804a-0fc166c6e506", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35d3a37a-f7f6-49a3-8022-8bd9044e90ac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6e8e5fc-103e-4c47-804a-0fc166c6e506");
        }
    }
}
