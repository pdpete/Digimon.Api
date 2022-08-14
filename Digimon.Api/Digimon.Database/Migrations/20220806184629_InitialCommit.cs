using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Digimon.Database.Migrations
{
    public partial class InitialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "colors",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_colors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rarities",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rarities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "traits",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_traits", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cards",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None),
                    set_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    level = table.Column<int>(type: "integer", nullable: true),
                    dp = table.Column<int>(type: "integer", nullable: true),
                    play_cost = table.Column<int>(type: "integer", nullable: true),
                    rarity_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cards", x => x.id);
                    table.ForeignKey(
                        name: "fk_cards_rarities_rarity_id",
                        column: x => x.rarity_id,
                        principalTable: "rarities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "decks",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_decks", x => x.id);
                    table.ForeignKey(
                        name: "fk_decks_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "card_color",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    cards_id = table.Column<long>(type: "bigint", nullable: false),
                    colors_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_card_color", x => x.id);
                    table.ForeignKey(
                        name: "fk_card_color_cards_cards_id",
                        column: x => x.cards_id,
                        principalTable: "cards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_card_color_colors_colors_id",
                        column: x => x.colors_id,
                        principalTable: "colors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "card_trait",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    cards_id = table.Column<long>(type: "bigint", nullable: false),
                    traits_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_card_trait", x => x.id);
                    table.ForeignKey(
                        name: "fk_card_trait_cards_cards_id",
                        column: x => x.cards_id,
                        principalTable: "cards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_card_trait_traits_traits_id",
                        column: x => x.traits_id,
                        principalTable: "traits",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "digivolutions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    color_id = table.Column<long>(type: "bigint", nullable: false),
                    cost = table.Column<int>(type: "integer", nullable: false),
                    card_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_digivolutions", x => x.id);
                    table.ForeignKey(
                        name: "fk_digivolutions_cards_card_id",
                        column: x => x.card_id,
                        principalTable: "cards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_digivolutions_colors_color_id",
                        column: x => x.color_id,
                        principalTable: "colors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_cards",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    card_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_cards", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_cards_cards_card_id",
                        column: x => x.card_id,
                        principalTable: "cards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_cards_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deck_cards",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    card_id = table.Column<long>(type: "bigint", nullable: false),
                    deck_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deck_cards", x => x.id);
                    table.ForeignKey(
                        name: "fk_deck_cards_cards_card_id",
                        column: x => x.card_id,
                        principalTable: "cards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_deck_cards_decks_deck_id",
                        column: x => x.deck_id,
                        principalTable: "decks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_card_color_colors_id",
                table: "card_color",
                column: "colors_id");

            migrationBuilder.CreateIndex(
                name: "ix_card_trait_traits_id",
                table: "card_trait",
                column: "traits_id");

            migrationBuilder.CreateIndex(
                name: "ix_cards_rarity_id",
                table: "cards",
                column: "rarity_id");

            migrationBuilder.CreateIndex(
                name: "ix_deck_cards_card_id",
                table: "deck_cards",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "ix_deck_cards_deck_id",
                table: "deck_cards",
                column: "deck_id");

            migrationBuilder.CreateIndex(
                name: "ix_decks_user_id",
                table: "decks",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_digivolutions_card_id",
                table: "digivolutions",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "ix_digivolutions_color_id",
                table: "digivolutions",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_cards_card_id",
                table: "user_cards",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_cards_user_id",
                table: "user_cards",
                column: "user_id");

            migrationBuilder.Sql(@"INSERT INTO colors (id, name) VALUES
                (1, 'Black'),
                (2, 'Red'),
                (3, 'Green'),
                (4, 'Blue'),
                (5, 'Yellow'),
                (6, 'Purple'),
                (7, 'White')");

            migrationBuilder.Sql(@"INSERT INTO rarities (id, name) VALUES
                (1, 'Promo'),
                (2, 'Common'),
                (3, 'Uncommon'),
                (4, 'Rare'),
                (5, 'SuperRare'),
                (6, 'SecretRare')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "card_color");

            migrationBuilder.DropTable(
                name: "card_trait");

            migrationBuilder.DropTable(
                name: "deck_cards");

            migrationBuilder.DropTable(
                name: "digivolutions");

            migrationBuilder.DropTable(
                name: "user_cards");

            migrationBuilder.DropTable(
                name: "traits");

            migrationBuilder.DropTable(
                name: "decks");

            migrationBuilder.DropTable(
                name: "colors");

            migrationBuilder.DropTable(
                name: "cards");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "rarities");
        }
    }
}
