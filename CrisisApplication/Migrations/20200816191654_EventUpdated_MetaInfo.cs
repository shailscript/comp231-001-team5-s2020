using Microsoft.EntityFrameworkCore.Migrations;

namespace CrisisApplication.Migrations
{
    public partial class EventUpdated_MetaInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RespondentMetaInfo",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RespondentMetaInfo",
                table: "Events");
        }
    }
}
