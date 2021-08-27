using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjectFinderApi.V1.Infrastructure.Migrations
{
    public partial class AddProjectMembersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pf_project_members",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", maxLength: 16, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_id = table.Column<int>(type: "integer", maxLength: 16, nullable: false),
                    user_id = table.Column<int>(type: "integer", maxLength: 16, nullable: false),
                    project_role = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pf_project_members", x => x.id);
                    table.ForeignKey(
                        name: "FK_pf_project_members_pf_project_project_id",
                        column: x => x.project_id,
                        principalTable: "pf_project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pf_project_members_pf_user_user_id",
                        column: x => x.user_id,
                        principalTable: "pf_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pf_project_members_project_id",
                table: "pf_project_members",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_pf_project_members_user_id",
                table: "pf_project_members",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pf_project_members");
        }
    }
}
