using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChrisHaniHospital.Migrations
{
    /// <inheritdoc />
    public partial class addPatientAllergies3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAllergies_Allergies_AllergiesId",
                table: "PatientAllergies");

            migrationBuilder.RenameColumn(
                name: "AllergiesId",
                table: "PatientAllergies",
                newName: "AllergyId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAllergies_AllergiesId",
                table: "PatientAllergies",
                newName: "IX_PatientAllergies_AllergyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAllergies_Allergies_AllergyId",
                table: "PatientAllergies",
                column: "AllergyId",
                principalTable: "Allergies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAllergies_Allergies_AllergyId",
                table: "PatientAllergies");

            migrationBuilder.RenameColumn(
                name: "AllergyId",
                table: "PatientAllergies",
                newName: "AllergiesId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientAllergies_AllergyId",
                table: "PatientAllergies",
                newName: "IX_PatientAllergies_AllergiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAllergies_Allergies_AllergiesId",
                table: "PatientAllergies",
                column: "AllergiesId",
                principalTable: "Allergies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
