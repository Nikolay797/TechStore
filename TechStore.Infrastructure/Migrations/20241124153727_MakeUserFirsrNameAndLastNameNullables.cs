using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserFirsrNameAndLastNameNullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b129438-03c0-4f93-8d80-16fa6d4afa54",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1eae4017-30ed-4a05-a32d-cd1d1d318c60", "AQAAAAIAAYagAAAAEMILjNDiV14D/lshoKIF9HK8gBrEgx2mUj67ht5HXjbA7TQLbPitBXhQWGiJKl6iFQ==", "144bcbcc-0379-49f3-8c29-6d2433e80ebc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69d44205-edfe-47b9-8d27-6366c018f434",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e2db38e-c560-41ce-bd92-d2d8a1d554e6", "AQAAAAIAAYagAAAAEMcsSwxnbhNsUBwT3PG5JubXP1JiQ7sqfMdCQiEXv6FqxWdFFoamWRnHdfj+lwUPYw==", "7b135c89-3b69-4262-8da1-749336161712" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80c1bdd8-73f9-4713-a939-090e9e07281b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c0ff5ed-c927-4413-a696-0cbb945cc89b", "AQAAAAIAAYagAAAAEC6vYGAFwXCiY/I6hoc2JvH1wmDAZrE1Rsnu9kxIVRM5yldnGHN4V67n30tUa95yoA==", "223ac4ab-3f24-4608-983d-fc565771a38f" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "CountOfPurchases", "UserId" },
                values: new object[] { 1, 3, "0b129438-03c0-4f93-8d80-16fa6d4afa54" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b129438-03c0-4f93-8d80-16fa6d4afa54",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84d2e3b4-a5ef-4f9c-bd4d-d80b69d6fa0d", "AQAAAAIAAYagAAAAEDz0PO5MYmRKOQgE/E9pFMV0npJWP6DdaizfyOERfBpl6w42t3ppqeWwZNcsIJReLQ==", "37505365-f6d1-4bd0-b53b-9cc24e41505a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69d44205-edfe-47b9-8d27-6366c018f434",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c52776dc-f323-40c5-9212-42c9514e7cf3", "AQAAAAIAAYagAAAAEAtCTduqXt6GeLxTNSsj7vkIH24Xl80TDhI/ylCRkuITvvcYWAgfPGDcCDkkuW+uQA==", "e2eb52bf-8a47-454f-8e76-8b5bf6f44a6a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80c1bdd8-73f9-4713-a939-090e9e07281b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6269af26-59d1-45a6-bb37-23d0f4835d97", "AQAAAAIAAYagAAAAEGaZiR74d1Qz0E2UcLosGWwJ/w0fIN4mI8R+ncAe3GEUqS1AIj2truYJCpXm/V73fQ==", "620670c9-1088-4134-a24c-6762f7a3a237" });
        }
    }
}
