using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChrisHaniHospital.Migrations
{
    /// <inheritdoc />
    public partial class AddPrescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Precription_Medication_MedicationId",
                table: "Precription"
            );

            migrationBuilder.DropIndex(name: "IX_Precription_MedicationId", table: "Precription");

            migrationBuilder.DropColumn(name: "Instructions", table: "Precription");

            migrationBuilder.DropColumn(name: "MedicationId", table: "Precription");

            migrationBuilder.DropColumn(name: "Quantity", table: "Precription");

            migrationBuilder.CreateTable(
                name: "PrescriptionMedications",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMedications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrescriptionMedications_Medication_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_PrescriptionMedications_Precription_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Precription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedications_MedicationId",
                table: "PrescriptionMedications",
                column: "MedicationId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedications_PrescriptionId",
                table: "PrescriptionMedications",
                column: "PrescriptionId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PrescriptionMedications");

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "Precription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<int>(
                name: "MedicationId",
                table: "Precription",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Precription",
                type: "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.CreateIndex(
                name: "IX_Precription_MedicationId",
                table: "Precription",
                column: "MedicationId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Precription_Medication_MedicationId",
                table: "Precription",
                column: "MedicationId",
                principalTable: "Medication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
