using Microsoft.EntityFrameworkCore.Migrations;

namespace BulgarianProducers.Data.Migrations
{
    public partial class EventImagesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventImage_AgriculturalEvents_AgriculturalEventId",
                table: "EventImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventImage",
                table: "EventImage");

            migrationBuilder.RenameTable(
                name: "EventImage",
                newName: "EventImages");

            migrationBuilder.RenameIndex(
                name: "IX_EventImage_AgriculturalEventId",
                table: "EventImages",
                newName: "IX_EventImages_AgriculturalEventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventImages",
                table: "EventImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventImages_AgriculturalEvents_AgriculturalEventId",
                table: "EventImages",
                column: "AgriculturalEventId",
                principalTable: "AgriculturalEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventImages_AgriculturalEvents_AgriculturalEventId",
                table: "EventImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventImages",
                table: "EventImages");

            migrationBuilder.RenameTable(
                name: "EventImages",
                newName: "EventImage");

            migrationBuilder.RenameIndex(
                name: "IX_EventImages_AgriculturalEventId",
                table: "EventImage",
                newName: "IX_EventImage_AgriculturalEventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventImage",
                table: "EventImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EventImage_AgriculturalEvents_AgriculturalEventId",
                table: "EventImage",
                column: "AgriculturalEventId",
                principalTable: "AgriculturalEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
