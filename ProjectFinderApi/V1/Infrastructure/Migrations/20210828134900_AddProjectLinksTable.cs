using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjectFinderApi.V1.Infrastructure.Migrations
{
    public partial class AddProjectLinksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pf_project_links",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", maxLength: 16, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_id = table.Column<int>(type: "integer", maxLength: 16, nullable: false),
                    link_title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    link = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pf_project_links", x => x.id);
                    table.ForeignKey(
                        name: "FK_pf_project_links_pf_project_project_id",
                        column: x => x.project_id,
                        principalTable: "pf_project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pf_project_links_project_id",
                table: "pf_project_links",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pf_project_links");
        }
    }
}
