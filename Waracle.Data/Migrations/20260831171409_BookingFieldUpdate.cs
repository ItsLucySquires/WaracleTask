using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaracleTask.Data.Migrations
{
    /// <inheritdoc />
    public partial class BookingFieldUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Room_RoomId",
                table: "Booking");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Room_RoomId",
                table: "Booking",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Room_RoomId",
                table: "Booking");

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomId",
                table: "Booking",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Room_RoomId",
                table: "Booking",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");
        }
    }
}
