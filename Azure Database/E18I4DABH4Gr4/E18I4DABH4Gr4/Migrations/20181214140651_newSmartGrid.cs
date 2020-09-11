using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E18I4DABH4Gr4.Migrations
{
    public partial class newSmartGrid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmartGrids",
                columns: table => new
                {
                    SmartGridId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConsumerForeignKey = table.Column<int>(nullable: false),
                    ProducerForeignKey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartGrids", x => x.SmartGridId);
                });

            migrationBuilder.CreateTable(
                name: "Prosumers",
                columns: table => new
                {
                    ProsumerId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    prosumerType = table.Column<int>(nullable: false),
                    KWhAmount = table.Column<int>(nullable: false),
                    ConsumerForeignKey = table.Column<int>(nullable: true),
                    ProducerForeignKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prosumers", x => x.ProsumerId);
                    table.ForeignKey(
                        name: "FK_Prosumers_SmartGrids_ConsumerForeignKey",
                        column: x => x.ConsumerForeignKey,
                        principalTable: "SmartGrids",
                        principalColumn: "SmartGridId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prosumers_SmartGrids_ProducerForeignKey",
                        column: x => x.ProducerForeignKey,
                        principalTable: "SmartGrids",
                        principalColumn: "SmartGridId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prosumers_ConsumerForeignKey",
                table: "Prosumers",
                column: "ConsumerForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Prosumers_ProducerForeignKey",
                table: "Prosumers",
                column: "ProducerForeignKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prosumers");

            migrationBuilder.DropTable(
                name: "SmartGrids");
        }
    }
}
