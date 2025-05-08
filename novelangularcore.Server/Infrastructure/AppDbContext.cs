using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using novelangularcore.Server.Domain.Entities;

namespace novelangularcore.Server.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Novel> Novels { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NovelTag> NovelTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<Usuario>("Usuario")
            .HasValue<Autor>("Autor");

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Cpf).IsUnique();

            modelBuilder.Entity<Autor>()
                .HasOne(a => a.Usuario)
                .WithOne()
                .HasForeignKey<Autor>(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Administrador>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<NovelTag>()
                .HasKey(nt => new { nt.IdNovel, nt.IdTag });

            modelBuilder.Entity<NovelTag>()
                .HasOne(nt => nt.Novel)
                .WithMany(n => n.NovelTags)
                .HasForeignKey(nt => nt.IdNovel)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NovelTag>()
                .HasOne(nt => nt.Tag)
                .WithMany(t => t.NovelTags)
                .HasForeignKey(nt => nt.IdTag)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nome = "Usuário 1",
                    Sexo = "Masculino",
                    Cpf = "12345678901",
                    DataNascimento = DateTime.Parse("1990-01-01"),
                    Estado = "SP",
                    Rua = "Rua A",
                    Numero = "123",
                    TipoCasa = "Apartamento",
                    Complemento = "Complemento 1", // Adicionando campo obrigatório
                    Login = "usuario1",
                    Senha = "senha1",
                    Salt = "salt1",
                    Email = "usuario1@email.com"
                },
                new Usuario
                {
                    Id = 2,
                    Nome = "Usuário 2",
                    Sexo = "Feminino",
                    Cpf = "23456789012",
                    DataNascimento = DateTime.Parse("1995-05-15"),
                    Estado = "RJ",
                    Rua = "Rua B",
                    Numero = "456",
                    TipoCasa = "Casa",
                    Complemento = "Complemento 2", // Adicionando campo obrigatório
                    Login = "usuario2",
                    Senha = "senha2",
                    Salt = "salt2",
                    Email = "usuario2@email.com"
                }
            );

            modelBuilder.Entity<Autor>().HasData(
            new Autor
            {   
                Id = 3,
                UsuarioId = 3,
                Nome = "Usuário 4",
                Sexo = "Feminino",
                Cpf = "34567891011",
                DataNascimento = DateTime.Parse("1985-03-15"),
                Estado = "RJ",
                Rua = "Rua C",
                Numero = "4569",
                TipoCasa = "Casa",
                Complemento = "Complemento 3", // Adicionando campo obrigatório
                Login = "usuario3",
                Senha = "senha4",
                Salt = "salt3",
                Email = "usuario3@email.com"
            },
            new Autor
            {
                Id=4,
                UsuarioId = 4,
                Nome = "Usuário 3",
                Sexo = "Feminino",
                Cpf = "45678910111",
                DataNascimento = DateTime.Parse("1991-02-15"),
                Estado = "RJ",
                Rua = "Rua D",
                Numero = "4567",
                TipoCasa = "Casa",
                Complemento = "Complemento 4", // Adicionando campo obrigatório
                Login = "usuario3",
                Senha = "senha4",
                Salt = "salt4",
                Email = "usuario4@email.com"
            });

        }
    }
}
