using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExerciseRecommendationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserExerciseRecommendations_Exercises_ExerciseId",
                table: "UserExerciseRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_UserExerciseRecommendations_ExerciseId",
                table: "UserExerciseRecommendations");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "UserExerciseRecommendations");

            migrationBuilder.AddColumn<string>(
                name: "RecommendedEquipment",
                table: "UserExerciseRecommendations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecommendedExercises",
                table: "UserExerciseRecommendations",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecommendedEquipment",
                table: "UserExerciseRecommendations");

            migrationBuilder.DropColumn(
                name: "RecommendedExercises",
                table: "UserExerciseRecommendations");

            migrationBuilder.AddColumn<Guid>(
                name: "ExerciseId",
                table: "UserExerciseRecommendations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserExerciseRecommendations_ExerciseId",
                table: "UserExerciseRecommendations",
                column: "ExerciseId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserExerciseRecommendations_Exercises_ExerciseId",
                table: "UserExerciseRecommendations",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
