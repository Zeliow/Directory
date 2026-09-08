using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DirectoryService.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_department_location_locations_LocationId1",
                table: "department_location");

            migrationBuilder.DropForeignKey(
                name: "FK_department_position_positions_PositionId1",
                table: "department_position");

            migrationBuilder.DropIndex(
                name: "IX_department_position_PositionId1",
                table: "department_position");

            migrationBuilder.DropIndex(
                name: "IX_department_location_LocationId1",
                table: "department_location");

            migrationBuilder.DropColumn(
                name: "PositionId1",
                table: "department_position");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "department_location");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PositionId1",
                table: "department_position",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId1",
                table: "department_location",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_department_position_PositionId1",
                table: "department_position",
                column: "PositionId1");

            migrationBuilder.CreateIndex(
                name: "IX_department_location_LocationId1",
                table: "department_location",
                column: "LocationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_department_location_locations_LocationId1",
                table: "department_location",
                column: "LocationId1",
                principalTable: "locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_department_position_positions_PositionId1",
                table: "department_position",
                column: "PositionId1",
                principalTable: "positions",
                principalColumn: "Id");
        }
    }
}
