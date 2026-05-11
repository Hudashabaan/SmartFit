using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCaloriesPredictionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaloriesPredictions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WeightKg = table.Column<double>(type: "float(5)", precision: 5, scale: 2, nullable: false),
                    WorkoutType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SessionDurationHours = table.Column<double>(type: "float(4)", precision: 4, scale: 2, nullable: false),
                    AvgBPM = table.Column<int>(type: "int", nullable: false),
                    MaxBPM = table.Column<int>(type: "int", nullable: false),
                    WorkoutFrequencyDaysWeek = table.Column<int>(type: "int", nullable: false),
                    BMI = table.Column<double>(type: "float(5)", precision: 5, scale: 2, nullable: false),
                    PredictedBurnedCalories = table.Column<double>(type: "float(8)", precision: 8, scale: 2, nullable: false),
                    WorkoutAnalysis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    WorkoutSummary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaloriesPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaloriesPredictions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaloriesPredictions_UserId",
                table: "CaloriesPredictions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaloriesPredictions");
        }
    }
}
