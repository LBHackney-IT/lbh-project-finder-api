using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjectFinderApi.V1.Infrastructure.Migrations
{
    public partial class AddProjectsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pf_project",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", maxLength: 16, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    project_contact = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phase = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    size = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    category = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: true),
                    priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    product_users = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    dependencies = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pf_project", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pf_project");
        }
    }
}
