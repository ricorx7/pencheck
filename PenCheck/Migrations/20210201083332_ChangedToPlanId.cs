using Microsoft.EntityFrameworkCore.Migrations;

namespace PenCheck.Migrations
{
    public partial class ChangedToPlanId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participant_Plan_SelectedPlanId",
                table: "Participant");

            migrationBuilder.DropTable(
                name: "Plan");

            migrationBuilder.DropIndex(
                name: "IX_Participant_SelectedPlanId",
                table: "Participant");

            migrationBuilder.DropColumn(
                name: "SelectedPlanId",
                table: "Participant");

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Participant",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Participant");

            migrationBuilder.AddColumn<int>(
                name: "SelectedPlanId",
                table: "Participant",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Plan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participant_SelectedPlanId",
                table: "Participant",
                column: "SelectedPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participant_Plan_SelectedPlanId",
                table: "Participant",
                column: "SelectedPlanId",
                principalTable: "Plan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
