using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventApi.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Event",
                newName: "EventName");

            migrationBuilder.AddColumn<DateTime>(
                name: "EventEnd",
                table: "Event",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EventStart",
                table: "Event",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventEnd",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventStart",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "EventName",
                table: "Event",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Event",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
