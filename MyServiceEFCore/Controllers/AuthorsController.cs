using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Biblio.Domain;
using Biblio.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly LibraryContext _db;

    public AuthorsController(LibraryContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAll() =>
        await _db.Authors.Include(a => a.Books).ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Author>> Get(int id)
    {
        var author = await _db.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);

        return author is null ? NotFound() : author;
    }

    [HttpPost]
    public async Task<ActionResult<Author>> Create(Author author)
    {
        _db.Authors.Add(author);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = author.Id }, author);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Author author)
    {
        if (id != author.Id) return BadRequest();

        _db.Entry(author).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var author = await _db.Authors.FindAsync(id);
        if (author is null) return NotFound();

        _db.Authors.Remove(author);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
