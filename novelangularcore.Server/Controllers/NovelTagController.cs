using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using novelangularcore.Server.Domain.Entities;
using novelangularcore.Server.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace novelangularcore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovelTagController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NovelTagController(AppDbContext context)
        {
            _context = context;
        }

        // Associa uma tag a uma novel
        [HttpPost("associate")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AssociateTagToNovel(int novelId, int tagId)
        {
            try
            {
                var novel = await _context.Novels.FindAsync(novelId);
                var tag = await _context.Tags.FindAsync(tagId);

                if (novel == null || tag == null)
                {
                    return NotFound("Novel ou tag não encontrada.");
                }

                var existing = await _context.NovelTags
                    .FirstOrDefaultAsync(nt => nt.IdNovel == novelId && nt.IdTag == tagId);

                if (existing != null)
                {
                    return BadRequest("A tag já está associada à novel.");
                }

                _context.NovelTags.Add(new NovelTag { IdNovel = novelId, IdTag = tagId });
                await _context.SaveChangesAsync();

                return Ok("Tag associada à novel com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // Remove associação de uma tag com uma novel
        [HttpDelete("dissociate")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DissociateTagFromNovel(int novelId, int tagId)
        {
            try
            {
                var association = await _context.NovelTags
                    .FirstOrDefaultAsync(nt => nt.IdNovel == novelId && nt.IdTag == tagId);

                if (association == null)
                {
                    return NotFound("Associação não encontrada.");
                }

                _context.NovelTags.Remove(association);
                await _context.SaveChangesAsync();

                return Ok("Tag removida da novel.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // Lista as tags associadas a uma novel
        [HttpGet("tags-by-novel/{novelId}")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagsByNovel(int novelId)
        {
            var tags = await _context.NovelTags
                .Where(nt => nt.IdNovel == novelId)
                .Include(nt => nt.Tag)
                .Select(nt => nt.Tag)
                .ToListAsync();

            return Ok(tags);
        }

        // Lista as novels associadas a uma tag
        [HttpGet("novels-by-tag/{tagId}")]
        public async Task<ActionResult<IEnumerable<Novel>>> GetNovelsByTag(int tagId)
        {
            var novels = await _context.NovelTags
                .Where(nt => nt.IdTag == tagId)
                .Include(nt => nt.Novel)
                .Select(nt => nt.Novel)
                .ToListAsync();

            return Ok(novels);
        }
    }
}