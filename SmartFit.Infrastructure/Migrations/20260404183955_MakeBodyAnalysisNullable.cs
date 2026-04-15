using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeBodyAnalysisNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetDays",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "TargetWeight",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "UserGoal",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<double>(
                name: "BMI",
                table: "BodyAnalyses",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartWeight = table.Column<double>(type: "float", nullable: false),
                    TargetWeight = table.Column<double>(type: "float", nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.AddColumn<int>(
                name: "TargetDays",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TargetWeight",
                table: "UserProfiles",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserGoal",
                table: "UserProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "BMI",
                table: "BodyAnalyses",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
