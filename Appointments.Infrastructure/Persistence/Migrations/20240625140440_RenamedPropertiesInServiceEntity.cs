using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointments.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenamedPropertiesInServiceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ServiceIsActive",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "SpecializationIsActive",
                table: "Service",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Service",
                newName: "SpecializationIsActive");

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "Service",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "ServiceIsActive",
                table: "Service",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
