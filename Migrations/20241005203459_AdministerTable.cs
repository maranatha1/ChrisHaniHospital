using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChrisHaniHospital.Migrations
{
    /// <inheritdoc />
    public partial class AdministerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "administeredMedications",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdministeredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_administeredMedications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_administeredMedications_Medication_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medication",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_administeredMedications_Precription_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Precription",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_administeredMedications_MedicationId",
                table: "administeredMedications",
                column: "MedicationId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_administeredMedications_PrescriptionId",
                table: "administeredMedications",
                column: "PrescriptionId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "administeredMedications");
        }
    }
}
