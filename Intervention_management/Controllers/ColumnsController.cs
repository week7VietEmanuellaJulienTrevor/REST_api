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
    [Produces("application/json")]
    [Route("api/Columns")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly Rocket_app_developmentContext _context;

        public ColumnsController(Rocket_app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Column>>> GetColumns()
        {
            return await _context.columns.ToListAsync();
        }

        // GET: api/columns/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Column>> GetColumn(long id)
        {
            var column = await _context.columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            var status = column.status;

            Console.WriteLine(column.GetType());
            Console.WriteLine(status);

            return column;
        }

        // GET: api/columns/not-operating
        [HttpGet("not-operating")]
        public async Task<ActionResult<IEnumerable<Column>>> GetNotOperatingColumns()
        {
            return await _context.columns.Where( e => e.status != "Active" ).ToListAsync();
        }

        // PUT: api/columns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColumn(long id, Column column)
        {
            if (id != column.Id)
            {
                return BadRequest();
            }

            _context.Entry(column).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColumnExists(id))
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

        // PUT: api/columns/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/status")]
        public async Task<IActionResult> PutColumnStatus(long id, Column column)
        {
            var modifiedColumn = _context.columns.Where(e => e.Id == column.Id).FirstOrDefault<Column>();
            // FindAsync(id);


            if (id != column.Id)
            {
                return BadRequest();
            }
            
            _context.Entry(modifiedColumn).State = EntityState.Detached;
            _context.Entry(column).State = EntityState.Modified;

            try
            {
                column.Id = modifiedColumn.Id;
                column.type_of_building = modifiedColumn.type_of_building;
                column.number_of_floors_served = modifiedColumn.number_of_floors_served;
                // column.status = modifiedColumn.status;
                column.information = modifiedColumn.information;
                column.notes = modifiedColumn.notes;
                column.created_at = modifiedColumn.created_at;
                column.updated_at = DateTime.Now;
                column.battery_id = modifiedColumn.battery_id;
                column.customer_id = modifiedColumn.customer_id;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColumnExists(id))
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

        // POST: api/columns
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Column>> PostColumn(Column column)
        {
            _context.columns.Add(column);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetColumn), new { id = column.Id }, column);
        }

        // DELETE: api/columns/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Column>> DeleteColumn(long id)
        {
            var column = await _context.columns.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }

            _context.columns.Remove(column);
            await _context.SaveChangesAsync();

            return column;
        }

        private bool ColumnExists(long id)
        {
            return _context.columns.Any(e => e.Id == id);
        }
    }
}
