using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekStore.Product.Data.Migrations
{
    /// <inheritdoc />
    public partial class seedsproduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "Category", "Description", "ImageURL", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("4b5e329c-2687-437b-8cfa-745e654d1dc2"), "Tecnologia", "Óculos que proporcionam uma experiência de realidade virtual imersiva", "https://example.com/vr-glasses.jpg", "Óculos de Realidade Virtual", 599.99m },
                    { new Guid("4f99339f-b347-494a-a8ed-3b1d54f00665"), "Filmes", "Uma miniatura detalhada do carro DeLorean que viaja no tempo no filme De Volta para o Futuro", "https://example.com/delorean.jpg", "Miniatura do DeLorean do De Volta para o Futuro", 149.99m },
                    { new Guid("80b982ee-ab71-493f-b02f-854b2a5909e5"), "Pokémon", "Uma réplica da pokebola usada para capturar pokemons no anime e jogo Pokémon", "https://example.com/pokeball.jpg", "Pokebola", 29.99m },
                    { new Guid("91cd2751-1622-44c1-95e9-4b3fcef35fd3"), "Jogos", "Uma camiseta com o icônico personagem do jogo Pac-Man estampado", "https://example.com/pacman-shirt.jpg", "Camiseta do Pac-Man", 49.99m },
                    { new Guid("d75ab164-d6d1-4250-88ba-a4a2ede4443b"), "Star Wars", "Uma réplica em miniatura da icônica luz do sabre de luz de Star Wars", "https://example.com/lightsaber.jpg", "Luz do Sabre de Luz", 199.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("4b5e329c-2687-437b-8cfa-745e654d1dc2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("4f99339f-b347-494a-a8ed-3b1d54f00665"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("80b982ee-ab71-493f-b02f-854b2a5909e5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("91cd2751-1622-44c1-95e9-4b3fcef35fd3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("d75ab164-d6d1-4250-88ba-a4a2ede4443b"));
        }
    }
}
