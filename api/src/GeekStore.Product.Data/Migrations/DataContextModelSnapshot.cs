﻿// <auto-generated />
using System;
using GeekStore.Product.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeekStore.Product.Data.Migrations
{
    [DbContext(typeof(ProductDataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GeekStore.Product.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("05b4fbcd-b8a5-4c56-ae75-a3d7f8a11f42"),
                            Category = "Star Wars",
                            Description = "Uma réplica em miniatura da icônica luz do sabre de luz de Star Wars",
                            ImageURL = "https://example.com/lightsaber.jpg",
                            Name = "Luz do Sabre de Luz",
                            Price = 199.99m
                        },
                        new
                        {
                            Id = new Guid("f5097f06-bc16-45df-bec5-3c81cac79068"),
                            Category = "Pokémon",
                            Description = "Uma réplica da pokebola usada para capturar pokemons no anime e jogo Pokémon",
                            ImageURL = "https://example.com/pokeball.jpg",
                            Name = "Pokebola",
                            Price = 29.99m
                        },
                        new
                        {
                            Id = new Guid("22012463-3eca-4721-92c9-837f05464ead"),
                            Category = "Tecnologia",
                            Description = "Óculos que proporcionam uma experiência de realidade virtual imersiva",
                            ImageURL = "https://example.com/vr-glasses.jpg",
                            Name = "Óculos de Realidade Virtual",
                            Price = 599.99m
                        },
                        new
                        {
                            Id = new Guid("fc405dd0-7171-4f07-a568-5fefb6eb7b69"),
                            Category = "Jogos",
                            Description = "Uma camiseta com o icônico personagem do jogo Pac-Man estampado",
                            ImageURL = "https://example.com/pacman-shirt.jpg",
                            Name = "Camiseta do Pac-Man",
                            Price = 49.99m
                        },
                        new
                        {
                            Id = new Guid("f6b3d9e3-7427-4786-b14d-835278921fe1"),
                            Category = "Filmes",
                            Description = "Uma miniatura detalhada do carro DeLorean que viaja no tempo no filme De Volta para o Futuro",
                            ImageURL = "https://example.com/delorean.jpg",
                            Name = "Miniatura do DeLorean do De Volta para o Futuro",
                            Price = 149.99m
                        },
                        new
                        {
                            Id = new Guid("8faf681b-269a-436a-9f0f-22de84081191"),
                            Category = "Star Wars",
                            Description = "Uma action figure detalhada do personagem Darth Vader de Star Wars",
                            ImageURL = "https://example.com/darth-vader-action-figure.jpg",
                            Name = "Darth Vader Action Figure",
                            Price = 79.99m
                        },
                        new
                        {
                            Id = new Guid("7a84cf12-421e-4273-af5a-0f5898aa0881"),
                            Category = "TV Shows",
                            Description = "Uma miniatura da icônica Tardis, máquina do tempo da série Doctor Who",
                            ImageURL = "https://example.com/tardis.jpg",
                            Name = "Tardis - Doctor Who",
                            Price = 149.99m
                        },
                        new
                        {
                            Id = new Guid("92aed48f-4efc-4539-9a44-8a66a7d0cd41"),
                            Category = "Pokémon",
                            Description = "Um bichinho de pelúcia fofo do personagem Pikachu, do anime e jogo Pokémon",
                            ImageURL = "https://example.com/pikachu-plush-toy.jpg",
                            Name = "Plush Toy Pikachu",
                            Price = 19.99m
                        },
                        new
                        {
                            Id = new Guid("5a6022ea-db3b-488c-918e-94acf13d839e"),
                            Category = "Tecnologia",
                            Description = "Fones de ouvido sem fio com tecnologia Bluetooth para uma experiência de áudio sem restrições",
                            ImageURL = "https://example.com/bluetooth-headphones.jpg",
                            Name = "Fone de Ouvido Bluetooth",
                            Price = 99.99m
                        },
                        new
                        {
                            Id = new Guid("727af68d-d8a7-450b-97bc-2017f5cf7384"),
                            Category = "Aventura",
                            Description = "Uma mochila durável e resistente à água para proteger seus pertences durante atividades ao ar livre",
                            ImageURL = "https://example.com/waterproof-backpack.jpg",
                            Name = "Mochila Resistente à Água",
                            Price = 79.99m
                        },
                        new
                        {
                            Id = new Guid("e1e91b45-c371-4556-a91b-5c5e574267e3"),
                            Category = "Casa e Cozinha",
                            Description = "Uma caneca isolada termicamente para manter suas bebidas quentes por mais tempo",
                            ImageURL = "https://example.com/thermal-mug.jpg",
                            Name = "Caneca Térmica",
                            Price = 19.99m
                        },
                        new
                        {
                            Id = new Guid("c04e1830-96d9-4974-acbd-5026f917fd1e"),
                            Category = "Jogos",
                            Description = "Um console de video game com gráficos avançados e uma vasta biblioteca de jogos",
                            ImageURL = "https://example.com/game-console.jpg",
                            Name = "Console de Video Game",
                            Price = 399.99m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
