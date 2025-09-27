using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ec77200-9bf7-430c-8219-9f974df6e024", null, "Supervisor", "SUPERVISOR" },
                    { "9e318efc-0882-43fa-b6ed-28617225dda7", null, "Employee", "EMPLOYEE" },
                    { "b9daafe4-ead8-4d88-adc5-b17104b65c25", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4f0061d7-a714-421d-8f00-7420644ac1564f0061d7-a714-421d-8f00-7420644ac156", 0, "24af43ed-df7e-4cb1-b025-82c682052289", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEG6IUT9vwF/ccDeqFho+JgsalxgZ7kyE7A+rcwdLAMXveUXO79/uMPxlVpBTHmD4jg==", null, false, "da5c0e0c-0e50-4dae-9956-876141c1b3ec", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b9daafe4-ead8-4d88-adc5-b17104b65c25", "4f0061d7-a714-421d-8f00-7420644ac1564f0061d7-a714-421d-8f00-7420644ac156" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ec77200-9bf7-430c-8219-9f974df6e024");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e318efc-0882-43fa-b6ed-28617225dda7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b9daafe4-ead8-4d88-adc5-b17104b65c25", "4f0061d7-a714-421d-8f00-7420644ac1564f0061d7-a714-421d-8f00-7420644ac156" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9daafe4-ead8-4d88-adc5-b17104b65c25");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f0061d7-a714-421d-8f00-7420644ac1564f0061d7-a714-421d-8f00-7420644ac156");
        }
    }
}
