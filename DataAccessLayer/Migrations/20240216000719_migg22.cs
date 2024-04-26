using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class migg22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientOperationImg_Operations_OperationID",
                table: "PatientOperationImg");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientOperationImg_ReminderDates_ReminderDateID",
                table: "PatientOperationImg");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientOperationImg",
                table: "PatientOperationImg");

            migrationBuilder.DropColumn(
                name: "EighthRemind",
                table: "ReminderDates");

            migrationBuilder.DropColumn(
                name: "FifthRemind",
                table: "ReminderDates");

            migrationBuilder.DropColumn(
                name: "FirstRemind",
                table: "ReminderDates");

            migrationBuilder.DropColumn(
                name: "FourthRemind",
                table: "ReminderDates");

            migrationBuilder.DropColumn(
                name: "NinthRemind",
                table: "ReminderDates");

            migrationBuilder.DropColumn(
                name: "SecondRemind",
                table: "ReminderDates");

            migrationBuilder.DropColumn(
                name: "SeventhRemind",
                table: "ReminderDates");

            migrationBuilder.DropColumn(
                name: "SixthRemind",
                table: "ReminderDates");

            migrationBuilder.DropColumn(
                name: "TenthRemind",
                table: "ReminderDates");

            migrationBuilder.RenameTable(
                name: "PatientOperationImg",
                newName: "patientOperationImgs");

            migrationBuilder.RenameColumn(
                name: "ThirdRemind",
                table: "ReminderDates",
                newName: "RemindDayCount");

            migrationBuilder.RenameIndex(
                name: "IX_PatientOperationImg_ReminderDateID",
                table: "patientOperationImgs",
                newName: "IX_patientOperationImgs_ReminderDateID");

            migrationBuilder.RenameIndex(
                name: "IX_PatientOperationImg_OperationID",
                table: "patientOperationImgs",
                newName: "IX_patientOperationImgs_OperationID");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ReminderDates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RemindDayCountName",
                table: "ReminderDates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_patientOperationImgs",
                table: "patientOperationImgs",
                column: "PatientImgID");

            migrationBuilder.AddForeignKey(
                name: "FK_patientOperationImgs_Operations_OperationID",
                table: "patientOperationImgs",
                column: "OperationID",
                principalTable: "Operations",
                principalColumn: "OperationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_patientOperationImgs_ReminderDates_ReminderDateID",
                table: "patientOperationImgs",
                column: "ReminderDateID",
                principalTable: "ReminderDates",
                principalColumn: "ReminderDateID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_patientOperationImgs_Operations_OperationID",
                table: "patientOperationImgs");

            migrationBuilder.DropForeignKey(
                name: "FK_patientOperationImgs_ReminderDates_ReminderDateID",
                table: "patientOperationImgs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_patientOperationImgs",
                table: "patientOperationImgs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ReminderDates");

            migrationBuilder.DropColumn(
                name: "RemindDayCountName",
                table: "ReminderDates");

            migrationBuilder.RenameTable(
                name: "patientOperationImgs",
                newName: "PatientOperationImg");

            migrationBuilder.RenameColumn(
                name: "RemindDayCount",
                table: "ReminderDates",
                newName: "ThirdRemind");

            migrationBuilder.RenameIndex(
                name: "IX_patientOperationImgs_ReminderDateID",
                table: "PatientOperationImg",
                newName: "IX_PatientOperationImg_ReminderDateID");

            migrationBuilder.RenameIndex(
                name: "IX_patientOperationImgs_OperationID",
                table: "PatientOperationImg",
                newName: "IX_PatientOperationImg_OperationID");

            migrationBuilder.AddColumn<int>(
                name: "EighthRemind",
                table: "ReminderDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FifthRemind",
                table: "ReminderDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FirstRemind",
                table: "ReminderDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FourthRemind",
                table: "ReminderDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NinthRemind",
                table: "ReminderDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondRemind",
                table: "ReminderDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeventhRemind",
                table: "ReminderDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SixthRemind",
                table: "ReminderDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenthRemind",
                table: "ReminderDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientOperationImg",
                table: "PatientOperationImg",
                column: "PatientImgID");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientOperationImg_Operations_OperationID",
                table: "PatientOperationImg",
                column: "OperationID",
                principalTable: "Operations",
                principalColumn: "OperationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientOperationImg_ReminderDates_ReminderDateID",
                table: "PatientOperationImg",
                column: "ReminderDateID",
                principalTable: "ReminderDates",
                principalColumn: "ReminderDateID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
