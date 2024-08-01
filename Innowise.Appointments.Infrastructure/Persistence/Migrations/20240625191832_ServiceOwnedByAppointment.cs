using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointments.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class ServiceOwnedByAppointment : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Appointments_Service_ServiceId",
            table: "Appointments");

        migrationBuilder.DropTable(
            name: "Service");

        migrationBuilder.DropIndex(
            name: "IX_Appointments_ServiceId",
            table: "Appointments");

        migrationBuilder.AddColumn<Guid>(
            name: "Service_Id",
            table: "Appointments",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<bool>(
            name: "Service_IsActive",
            table: "Appointments",
            type: "boolean",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<decimal>(
            name: "Service_Price",
            table: "Appointments",
            type: "numeric",
            nullable: false,
            defaultValue: 0m);

        migrationBuilder.AddColumn<Guid>(
            name: "Service_ServiceCategoryId",
            table: "Appointments",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<string>(
            name: "Service_ServiceCategoryName",
            table: "Appointments",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Service_ServiceName",
            table: "Appointments",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<Guid>(
            name: "Service_SpecializationId",
            table: "Appointments",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<string>(
            name: "Service_SpecializationName",
            table: "Appointments",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<int>(
            name: "Service_TimeSlotSize",
            table: "Appointments",
            type: "integer",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Service_Id",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "Service_IsActive",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "Service_Price",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "Service_ServiceCategoryId",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "Service_ServiceCategoryName",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "Service_ServiceName",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "Service_SpecializationId",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "Service_SpecializationName",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "Service_TimeSlotSize",
            table: "Appointments");

        migrationBuilder.CreateTable(
            name: "Service",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                Price = table.Column<decimal>(type: "numeric", nullable: false),
                ServiceCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                ServiceCategoryName = table.Column<string>(type: "text", nullable: false),
                ServiceName = table.Column<string>(type: "text", nullable: false),
                SpecializationId = table.Column<Guid>(type: "uuid", nullable: false),
                SpecializationName = table.Column<string>(type: "text", nullable: false),
                TimeSlotSize = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Service", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_ServiceId",
            table: "Appointments",
            column: "ServiceId");

        migrationBuilder.AddForeignKey(
            name: "FK_Appointments_Service_ServiceId",
            table: "Appointments",
            column: "ServiceId",
            principalTable: "Service",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}