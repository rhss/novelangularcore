﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using novelangularcore.Server.Infrastructure;

#nullable disable

namespace novelangularcore.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("novelangularcore.Server.Domain.Entities.Administrador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("novelangularcore.Server.Domain.Entities.Novel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagemUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.ToTable("Novels");
                });

            modelBuilder.Entity("novelangularcore.Server.Domain.Entities.NovelTag", b =>
                {
                    b.Property<int>("IdNovel")
                        .HasColumnType("int");

                    b.Property<int>("IdTag")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("IdNovel", "IdTag");

                    b.HasIndex("IdTag");

                    b.ToTable("NovelTags");
                });

            modelBuilder.Entity("novelangularcore.Server.Domain.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("novelangularcore.Server.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoCasa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("Usuarios");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");

                    b.UseTphMappingStrategy();

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Complemento = "Complemento 1",
                            Cpf = "12345678901",
                            DataNascimento = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "usuario1@email.com",
                            Estado = "SP",
                            Login = "usuario1",
                            Nome = "Usuário 1",
                            Numero = "123",
                            Rua = "Rua A",
                            Salt = "salt1",
                            Senha = "senha1",
                            Sexo = "Masculino",
                            TipoCasa = "Apartamento"
                        },
                        new
                        {
                            Id = 2,
                            Complemento = "Complemento 2",
                            Cpf = "23456789012",
                            DataNascimento = new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "usuario2@email.com",
                            Estado = "RJ",
                            Login = "usuario2",
                            Nome = "Usuário 2",
                            Numero = "456",
                            Rua = "Rua B",
                            Salt = "salt2",
                            Senha = "senha2",
                            Sexo = "Feminino",
                            TipoCasa = "Casa"
                        });
                });

            modelBuilder.Entity("novelangularcore.Server.Domain.Entities.Autor", b =>
                {
                    b.HasBaseType("novelangularcore.Server.Domain.Entities.Usuario");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasIndex("UsuarioId")
                        .IsUnique()
                        .HasFilter("[UsuarioId] IS NOT NULL");

                    b.HasDiscriminator().HasValue("Autor");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Complemento = "Complemento 3",
                            Cpf = "34567891011",
                            DataNascimento = new DateTime(1985, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "usuario3@email.com",
                            Estado = "RJ",
                            Login = "usuario3",
                            Nome = "Usuário 4",
                            Numero = "4569",
                            Rua = "Rua C",
                            Salt = "salt3",
                            Senha = "senha4",
                            Sexo = "Feminino",
                            TipoCasa = "Casa",
                            UsuarioId = 3
                        },
                        new
                        {
                            Id = 4,
                            Complemento = "Complemento 4",
                            Cpf = "45678910111",
                            DataNascimento = new DateTime(1991, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "usuario4@email.com",
                            Estado = "RJ",
                            Login = "usuario3",
                            Nome = "Usuário 3",
                            Numero = "4567",
                            Rua = "Rua D",
                            Salt = "salt4",
                            Senha = "senha4",
                            Sexo = "Feminino",
                            TipoCasa = "Casa",
                            UsuarioId = 4
                        });
                });

            modelBuilder.Entity("novelangularcore.Server.Domain.Entities.Novel", b =>
                {
                    b.HasOne("novelangularcore.Server.Domain.Entities.Autor", "Autor")
                        .WithMany()
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");
                });

            modelBuilder.Entity("novelangularcore.Server.Domain.Entities.NovelTag", b =>
                {
                    b.HasOne("novelangularcore.Server.Domain.Entities.Novel", "Novel")
                        .WithMany("NovelTags")
                        .HasForeignKey("IdNovel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("novelangularcore.Server.Domain.Entities.Tag", "Tag")
                        .WithMany("NovelTags")
                        .HasForeignKey("IdTag")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Novel");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("novelangularcore.Server.Domain.Entities.Autor", b =>
                {
                    b.HasOne("novelangularcore.Server.Domain.Entities.Usuario", "Usuario")
                        .WithOne()
                        .HasForeignKey("novelangularcore.Server.Domain.Entities.Autor", "UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("novelangularcore.Server.Domain.Entities.Novel", b =>
                {
                    b.Navigation("NovelTags");
                });

            modelBuilder.Entity("novelangularcore.Server.Domain.Entities.Tag", b =>
                {
                    b.Navigation("NovelTags");
                });
#pragma warning restore 612, 618
        }
    }
}
