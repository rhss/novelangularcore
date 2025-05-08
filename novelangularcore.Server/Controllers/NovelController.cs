using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using novelangularcore.Server.Domain.Entities;
using novelangularcore.Server.Infrastructure;

namespace NovelAngularCore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovelController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;  // Usando o UserManager para identificar o usuário logado

        public NovelController(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Novel>>> GetNovels()
        {
            try
            {
                var novels = await _context.Novels.Include(n => n.Autor).ToListAsync();
                return Ok(novels);  // Retorna 200 OK com a lista de novels
            }
            catch (Exception ex)
            {
                // Log da exceção e retorno de erro
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Novel>> GetNovel(int id)
        {
            try
            {
                var novel = await _context.Novels.Include(n => n.Autor).FirstOrDefaultAsync(n => n.Id == id);

                if (novel == null)
                {
                    return NotFound();  // Retorna 404 se a novel não for encontrada
                }

                return Ok(novel);  // Retorna 200 OK com a novel encontrada
            }
            catch (Exception ex)
            {
                // Log da exceção e retorno de erro
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]  // Garante que apenas usuários autenticados possam criar uma novel
        public async Task<ActionResult<Novel>> PostNovel(Novel novel)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                novel.AutorId = int.Parse(userId);  // Associar o autor logado à novel

                _context.Novels.Add(novel);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetNovel), new { id = novel.Id }, novel);  // Retorna 201 Created com a URL da novel criada
            }
            catch (DbUpdateException dbEx)
            {
                // Caso ocorra um erro ao tentar salvar no banco, retornamos um erro mais específico
                return StatusCode(500, $"Database update error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                // Log da exceção e retorno de erro genérico
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize]  // Garante que apenas usuários autenticados possam editar uma novel
        public async Task<IActionResult> PutNovel(int id, Novel novel)
        {
            if (id != novel.Id)
            {
                return BadRequest("O ID da novel não corresponde ao ID do parâmetro.");
            }

            try
            {
                var existingNovel = await _context.Novels.FirstOrDefaultAsync(n => n.Id == id);

                if (existingNovel == null)
                {
                    return NotFound();  // Retorna 404 se a novel não for encontrada
                }

                var userId = _userManager.GetUserId(User);

                if (existingNovel.AutorId.ToString() != userId)
                {
                    return Unauthorized();  // Se o usuário logado não for o autor, retorna 401 Unauthorized
                }

                _context.Entry(novel).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();  // Retorna 204 No Content se a edição for bem-sucedida
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]  // Garante que apenas usuários autenticados possam excluir uma novel
        public async Task<IActionResult> DeleteNovel(int id)
        {
            try
            {
                var novel = await _context.Novels.Include(n => n.Autor).FirstOrDefaultAsync(n => n.Id == id);

                if (novel == null)
                {
                    return NotFound();
                }

                var userId = _userManager.GetUserId(User);

                if (novel.AutorId.ToString() != userId)
                {
                    return Unauthorized();
                }

                _context.Novels.Remove(novel);
                await _context.SaveChangesAsync();

                return NoContent();  // Retorna 204 No Content se a exclusão for bem-sucedida
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database update error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
