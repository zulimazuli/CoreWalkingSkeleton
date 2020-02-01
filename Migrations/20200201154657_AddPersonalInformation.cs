using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreTemplate.Migrations
{
    public partial class AddPersonalInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonalInformationId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonalInformation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInformation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonalInformationId",
                table: "AspNetUsers",
                column: "PersonalInformationId",
                unique: true,
                filter: "[PersonalInformationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PersonalInformation_PersonalInformationId",
                table: "AspNetUsers",
                column: "PersonalInformationId",
                principalTable: "PersonalInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PersonalInformation_PersonalInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "PersonalInformation");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonalInformationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PersonalInformationId",
                table: "AspNetUsers");
        }
    }
}
