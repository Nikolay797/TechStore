using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserRolesSeeded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b129438-03c0-4f93-8d80-16fa6d4afa54",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b350a637-54f3-403b-b623-7ddd6e0997f8", "AQAAAAIAAYagAAAAELBSkdetpmXbVuuyEbGo1i73SUG/NaRonx+LtVHh4RaxyqcTehSN5dhIbgaSUH5Tyg==", "98da26af-c757-4632-ba30-cdc3fdf30141" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "69d44205-edfe-47b9-8d27-6366c018f434",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e7a7111-5491-4f1d-8455-e8c48fdb2bc8", "AQAAAAIAAYagAAAAEN92gehplTwUEW4ORDq/Tm6jd8gx9UGQRmbkPw40nzwZ7vVnp9vTx+XLRrB1xuV2Yw==", "0181cba0-5ff7-4167-925c-b3fb7fc5df78" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80c1bdd8-73f9-4713-a939-090e9e07281b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6939002e-91cc-442e-8094-d695cdcbd4a2", "AQAAAAIAAYagAAAAEN90SBuGLKupcRDVtCZ8j5HN5a0HRUsQFa7zZ8y/Kn/XWcA1VoWE2Y7xNz/qWRNsMA==", "da16f798-9a42-4051-954d-02825521bd52" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
