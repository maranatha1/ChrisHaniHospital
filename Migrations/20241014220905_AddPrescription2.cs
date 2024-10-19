using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChrisHaniHospital.Migrations
{
    /// <inheritdoc />
    public partial class AddPrescription2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patients",
                newName: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "Patients",
                newName: "Id");
        }
    }
}
