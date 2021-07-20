using Microsoft.EntityFrameworkCore.Migrations;

namespace BulgarianProducers.Data.Migrations
{
    public partial class AddedEventImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.CreateTable(
                name: "EventImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgriculturalEventId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventImage_AgriculturalEvents_AgriculturalEventId",
                        column: x => x.AgriculturalEventId,
                        principalTable: "AgriculturalEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventImage_AgriculturalEventId",
                table: "EventImage",
                column: "AgriculturalEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventImage");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgriculturalEventId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_AgriculturalEvents_AgriculturalEventId",
                        column: x => x.AgriculturalEventId,
                        principalTable: "AgriculturalEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_AgriculturalEventId",
                table: "Images",
                column: "AgriculturalEventId");
        }
    }
}
