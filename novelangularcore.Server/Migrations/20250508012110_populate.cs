using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace novelangularcore.Server.Migrations
{
    /// <inheritdoc />
    public partial class populate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Complemento", "Cpf", "DataNascimento", "Discriminator", "Email", "Estado", "Login", "Nome", "Numero", "Rua", "Salt", "Senha", "Sexo", "TipoCasa" },
                values: new object[,]
                {
                    { 1, "Complemento 1", "12345678901", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Usuario", "usuario1@email.com", "SP", "usuario1", "Usuário 1", "123", "Rua A", "salt1", "senha1", "Masculino", "Apartamento" },
                    { 2, "Complemento 2", "23456789012", new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Usuario", "usuario2@email.com", "RJ", "usuario2", "Usuário 2", "456", "Rua B", "salt2", "senha2", "Feminino", "Casa" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Complemento", "Cpf", "DataNascimento", "Discriminator", "Email", "Estado", "Login", "Nome", "Numero", "Rua", "Salt", "Senha", "Sexo", "TipoCasa", "UsuarioId" },
                values: new object[,]
                {
                    { 3, "Complemento 3", "34567891011", new DateTime(1985, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Autor", "usuario3@email.com", "RJ", "usuario3", "Usuário 4", "4569", "Rua C", "salt3", "senha4", "Feminino", "Casa", 3 },
                    { 4, "Complemento 4", "45678910111", new DateTime(1991, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Autor", "usuario4@email.com", "RJ", "usuario3", "Usuário 3", "4567", "Rua D", "salt4", "senha4", "Feminino", "Casa", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
