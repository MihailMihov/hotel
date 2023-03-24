using HotelAPI.Data.Context;
using HotelAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly HotelContext _context;

    public VehiclesController(HotelContext context)
    {
        _context = context;
    }

    // GET: api/Vehicles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
    {
        if (_context.Vehicles == null) return NotFound();
        return await _context.Vehicles.ToListAsync();
    }

    // GET: api/Vehicles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Vehicle>> GetVehicle(string id)
    {
        if (_context.Vehicles == null) return NotFound();
        var vehicle = await _context.Vehicles.FindAsync(id);

        if (vehicle == null) return NotFound();

        return vehicle;
    }

    // PUT: api/Vehicles/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVehicle(string id, Vehicle vehicle)
    {
        if (id != vehicle.Registration) return BadRequest();

        _context.Entry(vehicle).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!VehicleExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Vehicles
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
    {
        if (_context.Vehicles == null) return Problem("Entity set 'HotelContext.Vehicles'  is null.");
        _context.Vehicles.Add(vehicle);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (VehicleExists(vehicle.Registration))
                return Conflict();
            throw;
        }

        return CreatedAtAction(nameof(GetVehicle), new {id = vehicle.Registration}, vehicle);
    }

    // DELETE: api/Vehicles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVehicle(string id)
    {
        if (_context.Vehicles == null) return NotFound();
        var vehicle = await _context.Vehicles.FindAsync(id);
        if (vehicle == null) return NotFound();

        _context.Vehicles.Remove(vehicle);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool VehicleExists(string id)
    {
        return (_context.Vehicles?.Any(e => e.Registration == id)).GetValueOrDefault();
    }
}