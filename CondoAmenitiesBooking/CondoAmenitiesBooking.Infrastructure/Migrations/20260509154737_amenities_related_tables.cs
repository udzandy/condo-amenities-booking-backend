using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CondoAmenitiesBooking.Infrastructure.Migrations
{
    public partial class amenities_related_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Amenities_AmenityId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "AmenityRules");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Amenities");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Bookings",
                newName: "BookingDate");

            migrationBuilder.RenameColumn(
                name: "IsPaid",
                table: "Amenities",
                newName: "IsActive");

            migrationBuilder.AddColumn<int>(
                name: "SlotId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Amenities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoutePath",
                table: "Amenities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AmenityPolicies",
                columns: table => new
                {
                    PolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmenityId = table.Column<int>(type: "int", nullable: false),
                    CancellationHours = table.Column<int>(type: "int", nullable: false),
                    MaxBookingsPerMonth = table.Column<int>(type: "int", nullable: true),
                    MaxBookingsPerWeek = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityPolicies", x => x.PolicyId);
                    table.ForeignKey(
                        name: "FK_AmenityPolicies_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "AmenityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AmenityUnits",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmenityId = table.Column<int>(type: "int", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityUnits", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_AmenityUnits_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "AmenityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AmenityTimeSlots",
                columns: table => new
                {
                    SlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityTimeSlots", x => x.SlotId);
                    table.ForeignKey(
                        name: "FK_AmenityTimeSlots_AmenityUnits_UnitId",
                        column: x => x.UnitId,
                        principalTable: "AmenityUnits",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingDate_UnitId_SlotId",
                table: "Bookings",
                columns: new[] { "BookingDate", "UnitId", "SlotId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SlotId",
                table: "Bookings",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UnitId",
                table: "Bookings",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AmenityPolicies_AmenityId",
                table: "AmenityPolicies",
                column: "AmenityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmenityTimeSlots_UnitId",
                table: "AmenityTimeSlots",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AmenityUnits_AmenityId",
                table: "AmenityUnits",
                column: "AmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Amenities_AmenityId",
                table: "Bookings",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "AmenityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AmenityTimeSlots_SlotId",
                table: "Bookings",
                column: "SlotId",
                principalTable: "AmenityTimeSlots",
                principalColumn: "SlotId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AmenityUnits_UnitId",
                table: "Bookings",
                column: "UnitId",
                principalTable: "AmenityUnits",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Amenities_AmenityId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AmenityTimeSlots_SlotId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AmenityUnits_UnitId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "AmenityPolicies");

            migrationBuilder.DropTable(
                name: "AmenityTimeSlots");

            migrationBuilder.DropTable(
                name: "AmenityUnits");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookingDate_UnitId_SlotId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_SlotId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UnitId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "RoutePath",
                table: "Amenities");

            migrationBuilder.RenameColumn(
                name: "BookingDate",
                table: "Bookings",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Amenities",
                newName: "IsPaid");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Amenities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Amenities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "AmenityRules",
                columns: table => new
                {
                    RuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmenityId = table.Column<int>(type: "int", nullable: false),
                    CancellationPolicy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxDurationMinutes = table.Column<int>(type: "int", nullable: false),
                    TimeSlotIntervalMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityRules", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_AmenityRules_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenities",
                        principalColumn: "AmenityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AmenityRules_AmenityId",
                table: "AmenityRules",
                column: "AmenityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Amenities_AmenityId",
                table: "Bookings",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "AmenityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
