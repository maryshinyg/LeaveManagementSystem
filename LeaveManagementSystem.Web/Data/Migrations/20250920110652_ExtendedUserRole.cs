using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f0061d7-a714-421d-8f00-7420644ac1564f0061d7-a714-421d-8f00-7420644ac156",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "af101fee-9d2b-4e7c-b807-3e4dc6b00286", new DateOnly(1990, 1, 1), "Default", "Admin", "AQAAAAIAAYagAAAAECjo5ktWES3IHF+NShCkWFu1DCOcTAIHND9gLOjrPXm0YW+ifxlif4BAjiIXsRUZng==", "b1708264-3243-4967-b8d3-598bd2b5898f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4f0061d7-a714-421d-8f00-7420644ac1564f0061d7-a714-421d-8f00-7420644ac156",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24af43ed-df7e-4cb1-b025-82c682052289", "AQAAAAIAAYagAAAAEG6IUT9vwF/ccDeqFho+JgsalxgZ7kyE7A+rcwdLAMXveUXO79/uMPxlVpBTHmD4jg==", "da5c0e0c-0e50-4dae-9956-876141c1b3ec" });
        }
    }
}
