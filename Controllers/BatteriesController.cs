using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using intervention_management.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Intervention_management.Controllers
{
    [Produces("application/json")]
    [Route("api/Batteries")]
    [ApiController]
    public class BatteriesController : ControllerBase
    {
        private readonly Rocket_app_developmentContext _context;

        public BatteriesController(Rocket_app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Battery>>> GetBatteries()
        {
            return await _context.batteries.ToListAsync();
        }

        // GET: api/batteries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Battery>> GetBattery(long id)
        {
            var battery = await _context.batteries.FindAsync(id);

            if (battery == null)
            {
                return NotFound();
            }

            return battery;
        }

        
        // PUT: api/Batteries/5/status
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/status")]
        public async Task<IActionResult> PutBatteryStatus(long id, Battery battery)
        {   
            
            var originalBattery = _context.batteries.Where(e => e.Id == battery.Id).FirstOrDefault<Battery>();

            if (id != battery.Id)
            {
                return BadRequest();
            }

            _context.Entry(originalBattery).State = EntityState.Detached;

            _context.Entry(battery).State = EntityState.Modified;

            try
            {

                battery.Id = originalBattery.Id;
                battery.building_id = originalBattery.building_id;
                battery.type_of_building = originalBattery.type_of_building;
                // battery.status = originalBattery.status;
                battery.employee_id = originalBattery.employee_id ;
                battery.commissioning_date = originalBattery.commissioning_date;
                battery.last_inspection_date = originalBattery.last_inspection_date;
                battery.operations_certificate = originalBattery.operations_certificate;
                battery.information = originalBattery.information;
                battery.notes = originalBattery.notes;
                battery.created_at = originalBattery.created_at;
                battery.updated_at = DateTime.Now;
                battery.customer_id = originalBattery.customer_id;


                

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteryExists(id))
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


        

        private bool BatteryExists(long id)
        {
            return _context.batteries.Any(e => e.Id == id);
        }
    }
}
