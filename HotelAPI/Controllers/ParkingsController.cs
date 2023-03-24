using HotelAPI.Data.Context;
using HotelAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParkingsController : ControllerBase
{
    private readonly HotelContext _context;

    public ParkingsController(HotelContext context)
    {
        _context = context;
    }

    // GET: api/Parkings
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Parking>>> GetParkings()
    {
        if (_context.Parkings == null) return NotFound();
        return await _context.Parkings.ToListAsync();
    }

    // GET: api/Parkings/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Parking>> GetParking(int id)
    {
        if (_context.Parkings == null) return NotFound();
        var parking = await _context.Parkings.FindAsync(id);

        if (parking == null) return NotFound();

        return parking;
    }

    // PUT: api/Parkings/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutParking(int id, Parking parking)
    {
        if (id != parking.Id) return BadRequest();

        _context.Entry(parking).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ParkingExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Parkings
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Parking>> PostParking(Parking parking)
    {
        if (_context.Parkings == null) return Problem("Entity set 'HotelContext.Parkings'  is null.");
        _context.Parkings.Add(parking);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetParking), new {id = parking.Id}, parking);
    }

    // DELETE: api/Parkings/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParking(int id)
    {
        if (_context.Parkings == null) return NotFound();
        var parking = await _context.Parkings.FindAsync(id);
        if (parking == null) return NotFound();

        _context.Parkings.Remove(parking);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ParkingExists(int id)
    {
        return (_context.Parkings?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}