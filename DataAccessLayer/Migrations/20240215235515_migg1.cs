using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class migg1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientOperationImg",
                columns: table => new
                {
                    PatientImgID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dateofrecord = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ReminderDateID = table.Column<int>(type: "int", nullable: false),
                    OperationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientOperationImg", x => x.PatientImgID);
                    table.ForeignKey(
                        name: "FK_PatientOperationImg_Operations_OperationID",
                        column: x => x.OperationID,
                        principalTable: "Operations",
                        principalColumn: "OperationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientOperationImg_ReminderDates_ReminderDateID",
                        column: x => x.ReminderDateID,
                        principalTable: "ReminderDates",
                        principalColumn: "ReminderDateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientOperationImg_OperationID",
                table: "PatientOperationImg",
                column: "OperationID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientOperationImg_ReminderDateID",
                table: "PatientOperationImg",
                column: "ReminderDateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientOperationImg");
        }
    }
}
