using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekStore.Product.Data.Migrations
{
    /// <inheritdoc />
    public partial class novosProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "Category", "Description", "ImageURL", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("05b4fbcd-b8a5-4c56-ae75-a3d7f8a11f42"), "Star Wars", "Uma réplica em miniatura da icônica luz do sabre de luz de Star Wars", "https://example.com/lightsaber.jpg", "Luz do Sabre de Luz", 199.99m },
                    { new Guid("22012463-3eca-4721-92c9-837f05464ead"), "Tecnologia", "Óculos que proporcionam uma experiência de realidade virtual imersiva", "https://example.com/vr-glasses.jpg", "Óculos de Realidade Virtual", 599.99m },
                    { new Guid("5a6022ea-db3b-488c-918e-94acf13d839e"), "Tecnologia", "Fones de ouvido sem fio com tecnologia Bluetooth para uma experiência de áudio sem restrições", "https://example.com/bluetooth-headphones.jpg", "Fone de Ouvido Bluetooth", 99.99m },
                    { new Guid("727af68d-d8a7-450b-97bc-2017f5cf7384"), "Aventura", "Uma mochila durável e resistente à água para proteger seus pertences durante atividades ao ar livre", "https://example.com/waterproof-backpack.jpg", "Mochila Resistente à Água", 79.99m },
                    { new Guid("7a84cf12-421e-4273-af5a-0f5898aa0881"), "TV Shows", "Uma miniatura da icônica Tardis, máquina do tempo da série Doctor Who", "https://example.com/tardis.jpg", "Tardis - Doctor Who", 149.99m },
                    { new Guid("8faf681b-269a-436a-9f0f-22de84081191"), "Star Wars", "Uma action figure detalhada do personagem Darth Vader de Star Wars", "https://example.com/darth-vader-action-figure.jpg", "Darth Vader Action Figure", 79.99m },
                    { new Guid("92aed48f-4efc-4539-9a44-8a66a7d0cd41"), "Pokémon", "Um bichinho de pelúcia fofo do personagem Pikachu, do anime e jogo Pokémon", "https://example.com/pikachu-plush-toy.jpg", "Plush Toy Pikachu", 19.99m },
                    { new Guid("c04e1830-96d9-4974-acbd-5026f917fd1e"), "Jogos", "Um console de video game com gráficos avançados e uma vasta biblioteca de jogos", "https://example.com/game-console.jpg", "Console de Video Game", 399.99m },
                    { new Guid("e1e91b45-c371-4556-a91b-5c5e574267e3"), "Casa e Cozinha", "Uma caneca isolada termicamente para manter suas bebidas quentes por mais tempo", "https://example.com/thermal-mug.jpg", "Caneca Térmica", 19.99m },
                    { new Guid("f5097f06-bc16-45df-bec5-3c81cac79068"), "Pokémon", "Uma réplica da pokebola usada para capturar pokemons no anime e jogo Pokémon", "https://example.com/pokeball.jpg", "Pokebola", 29.99m },
                    { new Guid("f6b3d9e3-7427-4786-b14d-835278921fe1"), "Filmes", "Uma miniatura detalhada do carro DeLorean que viaja no tempo no filme De Volta para o Futuro", "https://example.com/delorean.jpg", "Miniatura do DeLorean do De Volta para o Futuro", 149.99m },
                    { new Guid("fc405dd0-7171-4f07-a568-5fefb6eb7b69"), "Jogos", "Uma camiseta com o icônico personagem do jogo Pac-Man estampado", "https://example.com/pacman-shirt.jpg", "Camiseta do Pac-Man", 49.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("05b4fbcd-b8a5-4c56-ae75-a3d7f8a11f42"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("22012463-3eca-4721-92c9-837f05464ead"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("5a6022ea-db3b-488c-918e-94acf13d839e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("727af68d-d8a7-450b-97bc-2017f5cf7384"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("7a84cf12-421e-4273-af5a-0f5898aa0881"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("8faf681b-269a-436a-9f0f-22de84081191"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("92aed48f-4efc-4539-9a44-8a66a7d0cd41"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("c04e1830-96d9-4974-acbd-5026f917fd1e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("e1e91b45-c371-4556-a91b-5c5e574267e3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("f5097f06-bc16-45df-bec5-3c81cac79068"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("f6b3d9e3-7427-4786-b14d-835278921fe1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "id",
                keyValue: new Guid("fc405dd0-7171-4f07-a568-5fefb6eb7b69"));

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
    }
}
