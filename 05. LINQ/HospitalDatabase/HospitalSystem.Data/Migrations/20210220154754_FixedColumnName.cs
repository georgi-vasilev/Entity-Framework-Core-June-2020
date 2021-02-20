using Microsoft.EntityFrameworkCore.Migrations;

namespace HospitalSystem.Data.Migrations
{
    public partial class FixedColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientMedicament_Patient_PatientId",
                table: "PatientMedicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament");

            migrationBuilder.DropIndex(
                name: "IX_PatientMedicament_PatientId",
                table: "PatientMedicament");

            migrationBuilder.DropColumn(
                name: "PatiendId",
                table: "PatientMedicament");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "PatientMedicament",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament",
                columns: new[] { "PatientId", "MedicamentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PatientMedicament_Patient_PatientId",
                table: "PatientMedicament",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientMedicament_Patient_PatientId",
                table: "PatientMedicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "PatientMedicament",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PatiendId",
                table: "PatientMedicament",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientMedicament",
                table: "PatientMedicament",
                columns: new[] { "PatiendId", "MedicamentId" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedicament_PatientId",
                table: "PatientMedicament",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientMedicament_Patient_PatientId",
                table: "PatientMedicament",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
