using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using intervention_management.Models;

namespace Intervention_management.Controllers
{
    [Route("api/buildings")]
    [ApiController]
    public class buildingsController : ControllerBase
    {
        private readonly Rocket_app_developmentContext _context;

        public buildingsController(Rocket_app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> Getbuildings()
        {
            return await _context.buildings.ToListAsync();
        }

        //  // GET: api/buildings
        // [HttpGet("needing-intervention")]
        // public async Task<ActionResult<IEnumerable<Building>>> GetbuildingsNeedingIntervention()
        // {
        //                 // return await _context.elevators.Where( e => e.status != "Active" ).ToListAsync();

        //     var buildingBatterie = await _context.batteries.Where(b => b.status = "Intervention" || b.status = "intervention" ).ToListAsync();
        //     return await _context.buildings.ToListAsync();
        // }

        // GET: api/buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(long id)
        {
            var building = await _context.buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        // PUT: api/buildings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(long id, Building building)
        {
            if (id != building.Id)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/buildings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
            _context.buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBuilding), new { id = building.Id }, building);
        }

        // DELETE: api/buildings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Building>> DeleteBuilding(long id)
        {
            var building = await _context.buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(building);
            await _context.SaveChangesAsync();

            return building;
        }

        private bool BuildingExists(long id)
        {
            return _context.buildings.Any(e => e.Id == id);
        }
    }
}
