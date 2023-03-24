using HotelAPI.Data.Context;
using HotelAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomKindsController : ControllerBase
{
    private readonly HotelContext _context;

    public RoomKindsController(HotelContext context)
    {
        _context = context;
    }

    // GET: api/RoomKinds
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomKind>>> GetRoomKinds()
    {
        if (_context.RoomKinds == null) return NotFound();
        return await _context.RoomKinds.ToListAsync();
    }

    // GET: api/RoomKinds/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomKind>> GetRoomKind(int id)
    {
        if (_context.RoomKinds == null) return NotFound();
        var roomKind = await _context.RoomKinds.FindAsync(id);

        if (roomKind == null) return NotFound();

        return roomKind;
    }

    // PUT: api/RoomKinds/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoomKind(int id, RoomKind roomKind)
    {
        if (id != roomKind.Id) return BadRequest();

        _context.Entry(roomKind).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RoomKindExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/RoomKinds
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<RoomKind>> PostRoomKind(RoomKind roomKind)
    {
        if (_context.RoomKinds == null) return Problem("Entity set 'HotelContext.RoomKinds'  is null.");
        _context.RoomKinds.Add(roomKind);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRoomKind), new {id = roomKind.Id}, roomKind);
    }

    // DELETE: api/RoomKinds/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoomKind(int id)
    {
        if (_context.RoomKinds == null) return NotFound();
        var roomKind = await _context.RoomKinds.FindAsync(id);
        if (roomKind == null) return NotFound();

        _context.RoomKinds.Remove(roomKind);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool RoomKindExists(int id)
    {
        return (_context.RoomKinds?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}