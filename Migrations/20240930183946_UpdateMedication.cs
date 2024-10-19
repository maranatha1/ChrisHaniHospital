using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChrisHaniHospital.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMedication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Precription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Precription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Precription_Medication_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Precription_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Precription_MedicationId",
                table: "Precription",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Precription_PatientId",
                table: "Precription",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Precription");
        }
    }
}
