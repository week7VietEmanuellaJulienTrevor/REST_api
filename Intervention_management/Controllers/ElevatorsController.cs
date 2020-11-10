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
    [Route("api/elevators")]
    [ApiController]
    public class ElevatorsController : ControllerBase
    {
        private readonly Rocket_app_developmentContext _context;

        public ElevatorsController(Rocket_app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevators()
        {
            return await _context.elevators.ToListAsync();
        }

        // GET: api/Elevators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevator(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            var status = elevator.status;

            Console.WriteLine(elevator.GetType());
            Console.WriteLine(status);


            return  elevator;
            // elevator.status.ToString();
            //elevator;
        }


        // // GET: api/Elevators/5/STATUS
        // [HttpGet("{id}/status")]
        // public async Task<ActionResult<Elevator>> GetElevatorStatus(long id)
        // {
        //     var elevator = await _context.elevators.FindAsync(id) ;

        //     if (elevator == null)
        //     {
        //         return NotFound();
        //     }

        //     var status = elevator.status;

        //     ElevatorStatus returnedElevator = (
        //         Id: elevator.Id,
        //         status: status
        //     );

        //     Console.WriteLine(elevator.GetType());
        //     Console.WriteLine(status);


        //     return  elevator;
        //     // elevator.status.ToString();
        //     //elevator;
        // }
        





        // GET: api/Elevators/not-operating
        [HttpGet("not-operating")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetNotOperatingElevators()
        {
            return await _context.elevators.Where( e => e.status != "Active" ).ToListAsync();
        }

        // PUT: api/Elevators/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElevator(long id, Elevator elevator)
        {
            if (id != elevator.Id)
            {
                return BadRequest();
            }

            _context.Entry(elevator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorExists(id))
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

        // POST: api/Elevators
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Elevator>> PostElevator(Elevator elevator)
        {
            _context.elevators.Add(elevator);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetElevator), new { id = elevator.Id }, elevator);
        }

        // DELETE: api/Elevators/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Elevator>> DeleteElevator(long id)
        {
            var elevator = await _context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }

            _context.elevators.Remove(elevator);
            await _context.SaveChangesAsync();

            return elevator;
        }
        // // GET: api/Elevators/5
        // [HttpGet("not-operating")]
        // public async Task<ActionResult<IEnumerable<Elevator>>> GetNotOperatingElevators()
        // {
        //     return await _context.elevators.Where( e => e.status != "Active" ).ToListAsync();
        // }

       


        private bool ElevatorExists(long id)
        {
            return _context.elevators.Any(e => e.Id == id);
        }
    }
}
