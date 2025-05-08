using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using novelangularcore.Server.Infrastructure;
using novelangularcore.Server.Domain.Entities;

namespace novelangularcore.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdministradorController(AppDbContext context)
        {
            _context = context;
        }

        // Registro do administrador
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] Administrador administrador)
        {
            try
            {
                // Verificar se o login já existe
                if (await _context.Administradores.AnyAsync(a => a.Login == administrador.Login))
                    return Conflict("Login já cadastrado.");

                string salt = PasswordUtils.GerarSalt();
                string senhaHash = PasswordUtils.GerarHashSenha(administrador.Senha, salt);

                administrador.Senha = senhaHash; // Armazena o hash da senha
                administrador.Salt = salt; // Armazena o salt gerado

                _context.Administradores.Add(administrador);
                await _context.SaveChangesAsync();

                return Ok("Administrador registrado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // Login do administrador
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Administrador credenciais)
        {
            try
            {
                var administrador = await _context.Administradores
                    .FirstOrDefaultAsync(a => a.Login == credenciais.Login);

                if (administrador == null)
                    return Unauthorized("Login inválido.");

                bool senhaValida = PasswordUtils.VerificarSenha(credenciais.Senha, administrador.Salt, administrador.Senha);

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