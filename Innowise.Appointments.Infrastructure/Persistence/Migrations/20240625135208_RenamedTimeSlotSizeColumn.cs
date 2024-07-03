using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointments.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamedTimeSlotSizeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServiceCategoryTimeSlotSize",
                table: "Service",
                newName: "TimeSlotSize");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeSlotSize",
                table: "Service",
                newName: "ServiceCategoryTimeSlotSize");
        }
    }
}
