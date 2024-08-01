using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointments.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AddedEmailToPatientModifiedAppointment : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DoctorFirstName",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "DoctorLastName",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "DoctorMiddleName",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "PatientFirstName",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "PatientLastName",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "PatientMiddleName",
            table: "Appointments");

        migrationBuilder.AddColumn<string>(
            name: "Email",
            table: "Patients",
            type: "text",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_DoctorId",
            table: "Appointments",
            column: "DoctorId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_PatientId",
            table: "Appointments",
            column: "PatientId");

        migrationBuilder.AddForeignKey(
            name: "FK_Appointments_Doctors_DoctorId",
            table: "Appointments",
            column: "DoctorId",
            principalTable: "Doctors",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Appointments_Patients_PatientId",
            table: "Appointments",
            column: "PatientId",
            principalTable: "Patients",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Appointments_Doctors_DoctorId",
            table: "Appointments");

        migrationBuilder.DropForeignKey(
            name: "FK_Appointments_Patients_PatientId",
            table: "Appointments");

        migrationBuilder.DropIndex(
            name: "IX_Appointments_DoctorId",
            table: "Appointments");

        migrationBuilder.DropIndex(
            name: "IX_Appointments_PatientId",
            table: "Appointments");

        migrationBuilder.DropColumn(
            name: "Email",
            table: "Patients");

        migrationBuilder.AddColumn<string>(
            name: "DoctorFirstName",
            table: "Appointments",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "DoctorLastName",
            table: "Appointments",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "DoctorMiddleName",
            table: "Appointments",
            type: "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "PatientFirstName",
            table: "Appointments",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "PatientLastName",
            table: "Appointments",
            type: "text",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "PatientMiddleName",
            table: "Appointments",
            type: "text",
            nullable: true);
    }
}