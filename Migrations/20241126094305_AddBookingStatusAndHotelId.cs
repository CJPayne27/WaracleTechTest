using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelWaracleBookingApi.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingStatusAndHotelId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HotelId",
                schema: "dbo",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "dbo",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelId",
                schema: "dbo",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "Bookings");
        }
    }
}
