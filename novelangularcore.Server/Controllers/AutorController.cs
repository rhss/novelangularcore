using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using novelangularcore.Server.Domain.Entities;
using novelangularcore.Server.Infrastructure;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AutorController : ControllerBase
{
    private readonly AppDbContext _context;

    public AutorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
    {
        try
        {
            return await _context.Autores.Include(a => a.Usuario).ToListAsync();
        }
        catch
        {
            return StatusCode(500, "Erro ao buscar autores.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Autor>> GetAutor(int id)
    {
        try
        {
            var autor = await _context.Autores.Include(a => a.Usuario).FirstOrDefaultAsync(a => a.Id == id);

            if (autor == null)
                return NotFound();

            return autor;
        }
        catch
        {
            return StatusCode(500, "Erro ao buscar autor.");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Autor>> PostAutor(Autor autor)
    {
        try
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAutor), new { id = autor.Id }, autor);
        }
        catch
        {
            return StatusCode(500, "Erro ao criar autor.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAutor(int id, Autor autor)
    {
        if (id != autor.Id)
            return BadRequest("ID do autor não confere.");

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || id.ToString() != userIdClaim)
            return Forbid("Você não tem permissão para editar este autor.");

        try
        {
            _context.Entry(autor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch
        {
            return StatusCode(500, "Erro ao atualizar autor.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAutor(int id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || id.ToString() != userIdClaim)
            return Forbid("Você não tem permissão para excluir este autor.");

        try
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
                return NotFound();

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch
        {
            return StatusCode(500, "Erro ao excluir autor.");
        }
    }
}