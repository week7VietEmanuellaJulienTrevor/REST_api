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
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly Rocket_app_developmentContext _context;

        public LeadsController(Rocket_app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/Leads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lead>>> Getleads()
        {
            return await _context.leads.ToListAsync();
        }
        
        // Get: api/leads/open-leads
        [HttpGet("open-leads")]
        public ActionResult<List<Lead>> GetOpenleads()
        {
            List<Lead> LeadsAll = _context.leads.ToList();
            List<Customer> CustomersAll = _context.customers.ToList();
            List<Lead> openLeads = new List<Lead>();
            List<Lead> recentLeads = new List<Lead>();
            DateTime currentDate  = DateTime.Today;
            DateTime daysAgo30 = currentDate.AddDays(-30);

            foreach(Lead lead in LeadsAll)
            {
                if (lead.created_at > daysAgo30)
                {
                    recentLeads.Add(lead);
                }
            }
            Int64 counter = 0;
            foreach(Lead lead in recentLeads)
            {
                foreach (Customer customer in CustomersAll)
                if (lead.company_name == customer.company_name)
                {
                    counter ++;
                }
                if (counter == 0 )
                {
                    openLeads.Add(lead);
                }

            }
            
            openLeads = openLeads.Distinct().ToList();
            openLeads = openLeads.OrderBy(o => o.Id ).ToList(); 

            return openLeads;
        }


        // GET: api/Leads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lead>> GetLead(long id)
        {
            var lead = await _context.leads.FindAsync(id);

            if (lead == null)
            {
                return NotFound();
            }

            return lead;
        }

        // PUT: api/Leads/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLead(long id, Lead lead)
        {
            if (id != lead.Id)
            {
                return BadRequest();
            }

            _context.Entry(lead).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeadExists(id))
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

        // POST: api/Leads
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Lead>> PostLead(Lead lead)
        {
            _context.leads.Add(lead);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLead), new { id = lead.Id }, lead);
        }

        // DELETE: api/Leads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lead>> DeleteLead(long id)
        {
            var lead = await _context.leads.FindAsync(id);
            if (lead == null)
            {
                return NotFound();
            }

            _context.leads.Remove(lead);
            await _context.SaveChangesAsync();

            return lead;
        }

        private bool LeadExists(long id)
        {
            return _context.leads.Any(e => e.Id == id);
        }
    }
}
