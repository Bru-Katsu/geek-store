using Microsoft.EntityFrameworkCore;

namespace GeekStore.Product.Data.Context
{
    public class ProductDataContext : DbContext
    {
        public ProductDataContext() { }
        public ProductDataContext(DbContextOptions<ProductDataContext> options) : base(options) { }

        public DbSet<Domain.Products.Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Products.Product>().HasData(new[] {
                new Domain.Products.Product("Luz do Sabre de Luz", 199.99m, "Uma réplica em miniatura da icônica luz do sabre de luz de Star Wars", "Star Wars", "https://example.com/lightsaber.jpg"),
                new Domain.Products.Product("Pokebola", 29.99m, "Uma réplica da pokebola usada para capturar pokemons no anime e jogo Pokémon", "Pokémon", "https://example.com/pokeball.jpg"),
                new Domain.Products.Product("Óculos de Realidade Virtual", 599.99m, "Óculos que proporcionam uma experiência de realidade virtual imersiva", "Tecnologia", "https://example.com/vr-glasses.jpg"),
                new Domain.Products.Product("Camiseta do Pac-Man", 49.99m, "Uma camiseta com o icônico personagem do jogo Pac-Man estampado", "Jogos", "https://example.com/pacman-shirt.jpg"),
                new Domain.Products.Product("Miniatura do DeLorean do De Volta para o Futuro", 149.99m, "Uma miniatura detalhada do carro DeLorean que viaja no tempo no filme De Volta para o Futuro", "Filmes", "https://example.com/delorean.jpg"),
                new Domain.Products.Product("Darth Vader Action Figure", 79.99m, "Uma action figure detalhada do personagem Darth Vader de Star Wars", "Star Wars", "https://example.com/darth-vader-action-figure.jpg"),
                new Domain.Products.Product("Tardis - Doctor Who", 149.99m, "Uma miniatura da icônica Tardis, máquina do tempo da série Doctor Who", "TV Shows", "https://example.com/tardis.jpg"),
                new Domain.Products.Product("Plush Toy Pikachu", 19.99m, "Um bichinho de pelúcia fofo do personagem Pikachu, do anime e jogo Pokémon", "Pokémon", "https://example.com/pikachu-plush-toy.jpg"),
                new Domain.Products.Product("Fone de Ouvido Bluetooth", 99.99m, "Fones de ouvido sem fio com tecnologia Bluetooth para uma experiência de áudio sem restrições", "Tecnologia", "https://example.com/bluetooth-headphones.jpg"),
                new Domain.Products.Product("Mochila Resistente à Água", 79.99m, "Uma mochila durável e resistente à água para proteger seus pertences durante atividades ao ar livre", "Aventura", "https://example.com/waterproof-backpack.jpg"),
                new Domain.Products.Product("Caneca Térmica", 19.99m, "Uma caneca isolada termicamente para manter suas bebidas quentes por mais tempo", "Casa e Cozinha", "https://example.com/thermal-mug.jpg"),
                new Domain.Products.Product("Console de Video Game", 399.99m, "Um console de video game com gráficos avançados e uma vasta biblioteca de jogos", "Jogos", "https://example.com/game-console.jpg")
            });
        }
    }
}
