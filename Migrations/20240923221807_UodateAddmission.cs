using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChrisHaniHospital.Migrations
{
    /// <inheritdoc />
    public partial class UodateAddmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdmittedPatients_Allergies_AlergyID",
                table: "AdmittedPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_AdmittedPatients_Conditions_ConditionID",
                table: "AdmittedPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_AdmittedPatients_Medication_MedicationID",
                table: "AdmittedPatients");

            migrationBuilder.DropIndex(
                name: "IX_AdmittedPatients_AlergyID",
                table: "AdmittedPatients");

            migrationBuilder.DropIndex(
                name: "IX_AdmittedPatients_ConditionID",
                table: "AdmittedPatients");

            migrationBuilder.DropIndex(
                name: "IX_AdmittedPatients_MedicationID",
                table: "AdmittedPatients");

            migrationBuilder.DropColumn(
                name: "AlergyID",
                table: "AdmittedPatients");

            migrationBuilder.DropColumn(
                name: "AllergiesNote",
                table: "AdmittedPatients");

            migrationBuilder.DropColumn(
                name: "ConditionID",
                table: "AdmittedPatients");

            migrationBuilder.DropColumn(
                name: "ConditionNote",
                table: "AdmittedPatients");

            migrationBuilder.DropColumn(
                name: "Instruction",
                table: "AdmittedPatients");

            migrationBuilder.DropColumn(
                name: "MedicationID",
                table: "AdmittedPatients");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "AdmittedPatients");

            migrationBuilder.AlterColumn<string>(
                name: "MedicationNotes",
                table: "PatientMedications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MedicationNotes",
                table: "PatientMedications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlergyID",
                table: "AdmittedPatients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AllergiesNote",
                table: "AdmittedPatients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConditionID",
                table: "AdmittedPatients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConditionNote",
                table: "AdmittedPatients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                table: "AdmittedPatients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicationID",
                table: "AdmittedPatients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "AdmittedPatients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdmittedPatients_AlergyID",
                table: "AdmittedPatients",
                column: "AlergyID");

            migrationBuilder.CreateIndex(
                name: "IX_AdmittedPatients_ConditionID",
                table: "AdmittedPatients",
                column: "ConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_AdmittedPatients_MedicationID",
                table: "AdmittedPatients",
                column: "MedicationID");

            migrationBuilder.AddForeignKey(
                name: "FK_AdmittedPatients_Allergies_AlergyID",
                table: "AdmittedPatients",
                column: "AlergyID",
                principalTable: "Allergies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdmittedPatients_Conditions_ConditionID",
                table: "AdmittedPatients",
                column: "ConditionID",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdmittedPatients_Medication_MedicationID",
                table: "AdmittedPatients",
                column: "MedicationID",
                principalTable: "Medication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
