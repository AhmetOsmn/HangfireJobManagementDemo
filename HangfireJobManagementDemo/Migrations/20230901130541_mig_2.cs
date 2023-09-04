using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HangfireJobManagementDemo.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomJobSchedules");

            migrationBuilder.DropTable(
                name: "CustomJobs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomJobSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomJobId = table.Column<int>(type: "int", nullable: false),
                    Minute = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomJobSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomJobSchedules_CustomJobs_CustomJobId",
                        column: x => x.CustomJobId,
                        principalTable: "CustomJobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomJobSchedules_CustomJobId",
                table: "CustomJobSchedules",
                column: "CustomJobId");
        }
    }
}
