using Microsoft.EntityFrameworkCore.Migrations;

namespace BulgarianProducers.Data.Migrations
{
    public partial class RenamedFieldDescriptionInServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalInformation",
                table: "Services",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Services",
                newName: "AdditionalInformation");
        }
    }
}
