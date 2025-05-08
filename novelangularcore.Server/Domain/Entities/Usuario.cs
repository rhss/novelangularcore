using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace novelangularcore.Server.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Estado { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string TipoCasa { get; set; }
        public string Complemento { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
    }
}
