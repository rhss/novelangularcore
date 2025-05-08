using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using novelangularcore.Server.Domain.Entities;
using novelangularcore.Server.Infrastructure;

namespace novelangularcore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }

        // GET: api/tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
                return NotFound();

            return tag;
        }

        // POST: api/tag
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            try
            {
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar tag: {ex.Message}");
            }
        }

        // PUT: api/tag/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTag(int id, Tag tag)
        {
            if (id != tag.Id)
                return BadRequest("ID inválido.");

            var existingTag = await _context.Tags.FindAsync(id);
            if (existingTag == null)
                return NotFound();

            existingTag.Nome = tag.Nome;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(existingTag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar tag: {ex.Message}");
            }
        }

        // DELETE: api/tag/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
                return NotFound();

            try
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir tag: {ex.Message}");
            }
        }
    }
}
