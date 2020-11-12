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
            List<User> UsersAll = _context.admin_users.ToList();
            List<Lead> openLeads = new List<Lead>();
            List<Lead> recentLeads = new List<Lead>();
            List<User> userCustomer = new List<User>();
            DateTime currentDate  = DateTime.Today;
            DateTime daysAgo30 = currentDate.AddDays(-30);

            // create a list of recent leads
            foreach(Lead lead in LeadsAll)
            {
                if (lead.created_at > daysAgo30)
                {
                    recentLeads.Add(lead);
                }
            }

            // create a list of users that are customers
            foreach (User user in UsersAll)
            {
                foreach( Customer customer in CustomersAll)
                {
                    if (user.Id == customer.admin_user_id)
                    {
                        userCustomer.Add(user);
                    }
                }
            }

          
            foreach(Lead lead in recentLeads)
            {
                Int64 counter = 0;
                // compare customer and lead emails
                foreach (Customer customer in CustomersAll)
                {
                    if (lead.email == customer.email_company_contact)
                    {
                        counter ++;
                    }
                }
                // compare lead and customer company name
                if (lead.company_name != null )
                {
                    foreach (Customer customer in CustomersAll)
                    {
                        if (lead.company_name == customer.company_name)
                        {
                            counter ++;
                        }
                    }
                }
                // compare email linked to user id end lead email
                foreach (User user in userCustomer)
                {
                    if(lead.email == user.email)
                    {
                        counter ++;
                    }
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

        private bool LeadExists(long id)
        {
            return _context.leads.Any(e => e.Id == id);
        }
    }
}
