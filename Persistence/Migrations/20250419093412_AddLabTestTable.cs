using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StrokePrediction.Migrations
{
    /// <inheritdoc />
    public partial class AddLabTestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Hypertension = table.Column<int>(type: "int", nullable: false),
                    HasHeartDisease = table.Column<int>(type: "int", nullable: false),
                    EverMarried = table.Column<bool>(type: "bit", nullable: false),
                    WorkType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Residence_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avg_glucose_level = table.Column<float>(type: "real", nullable: false),
                    Bmi = table.Column<float>(type: "real", nullable: false),
                    smoking_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrokeResult = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabTests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_UserId",
                table: "LabTests",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabTests");
        }
    }
}
