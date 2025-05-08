using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using novelangularcore.Server.Infrastructure;
using novelangularcore.Server.Domain.Entities;

namespace novelangularcore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // Registro do usuário
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] Usuario usuario)
        {
            try
            {
                // Verificar se o CPF já está cadastrado
                if (await _context.Usuarios.AnyAsync(u => u.Cpf == usuario.Cpf))
                    return Conflict("CPF já cadastrado.");

                // Gerar salt e hash para a senha
                string salt = PasswordUtils.GerarSalt();
                string senhaHash = PasswordUtils.GerarHashSenha(usuario.Senha, salt);

                usuario.Senha = senhaHash; // Armazena o hash da senha
                usuario.Salt = salt; // Armazena o salt gerado

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return Ok("Usuário registrado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // Login do usuário
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario credenciais)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Cpf == credenciais.Cpf);

                if (usuario == null)
                    return Unauthorized("CPF não encontrado.");

                // Verificar se a senha está correta
                bool senhaValida = PasswordUtils.VerificarSenha(credenciais.Senha, usuario.Salt, usuario.Senha);

                if (!senhaValida)
                    return Unauthorized("Senha incorreta.");

                return Ok("Login bem-sucedido.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}