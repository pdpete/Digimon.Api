using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Digimon.Database.Migrations
{
    public partial class AddCardType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "type_id",
                table: "cards",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.CreateTable(
                name: "card_types",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_card_types", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cards_type_id",
                table: "cards",
                column: "type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cards_card_types_type_id",
                table: "cards",
                column: "type_id",
                principalTable: "card_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.Sql(@"INSERT INTO card_types (id, name) VALUES
                (1, 'Digimon'),
                (2, 'Option'),
                (3, 'Egg'),
                (4, 'Tamer')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cards_card_types_type_id",
                table: "cards");

            migrationBuilder.DropTable(
                name: "card_types");

            migrationBuilder.DropIndex(
                name: "ix_cards_type_id",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "type_id",
                table: "cards");
        }
    }
}
