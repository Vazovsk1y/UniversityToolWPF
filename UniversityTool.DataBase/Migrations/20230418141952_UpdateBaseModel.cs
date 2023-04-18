using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityTool.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBaseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "date_updated",
                table: "students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "date_updated",
                table: "groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "date_updated",
                table: "departaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_updated",
                table: "students");

            migrationBuilder.DropColumn(
                name: "date_updated",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "date_updated",
                table: "departaments");
        }
    }
}
