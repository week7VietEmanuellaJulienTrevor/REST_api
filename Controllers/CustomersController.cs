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
    public class CustomersController : ControllerBase
    {
        private readonly Rocket_app_developmentContext _context;

        public CustomersController(Rocket_app_developmentContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Getcustomers()
        {
            return await _context.customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(long id)
        {
            var customer = await _context.customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
        // GET: api/Customers/count-in-last-50-days
        [HttpGet("count-in-last-{delay}-days")]
        public async Task<ActionResult<Int64>> CountRecentCustomers(long delay)
        {
            var customers = await _context.customers.FindAsync(delay);

            DateTime currentDate  = DateTime.Today;
            DateTime dateDelayAgo = currentDate.AddDays(-delay);

            System.Console.WriteLine(dateDelayAgo);
            List<Customer> customersAll = _context.customers.ToList();
            List<Customer> recentCustomers = new List<Customer>();

            foreach (Customer customer in customersAll)
            {
                System.Console.WriteLine(customer.customer_creation_date);
                if (customer.customer_creation_date > dateDelayAgo)
                {
                    System.Console.WriteLine("found");
                    recentCustomers.Add(customer);
                }
            }
            if (customers == null)
            {
                return NotFound();
            }

            return recentCustomers.Count;
        }
        // GET: api/Customers/count-in-between-date-and-date
        [HttpGet("count-in-between-{year1}-{month1}-{day1}-and-{year2}-{month2}-{day2}")]
        public async Task<ActionResult<Int64>> CountCustomersInPeriod( long year1, long month1, long day1,long year2, long month2, long day2)
        {
            // var customers = await _context.customers.FindAsync(delay);

            DateTime firstDate = new DateTime((int)year1, (int)month1, (int)day1);
            DateTime lastDate = new DateTime((int)year2, (int)month2, (int)day2);

            DateTime currentDate  = DateTime.Today;
            // DateTime dateDelayAgo = currentDate.AddDays(-delay);
            List<Customer> customersAll = _context.customers.ToList();
            List<Customer> recentCustomers = new List<Customer>();

            foreach (Customer customer in customersAll)
            {
                if (customer.customer_creation_date > firstDate && customer.customer_creation_date < lastDate )
                {
                    recentCustomers.Add(customer);
                }
            }
            if (recentCustomers == null)
            {
                return NotFound();
            }

            return recentCustomers.Count;
        }
        // GET: api/Customers/count-in-between-date-and-date
        [HttpGet("customer-{id}-pruducts")]
        public async Task<ActionResult<Dictionary<string,Int64>>> CountCustomersproducts( long id)
        {
            // var customers = await _context.customers.FindAsync(delay);

            var customer = await _context.customers.FindAsync(id);

            List<Building> buildingsAll = _context.buildings.ToList();
            List<Building> customerBuilding = new List<Building>();
            List<Elevator> elevatorsAll = _context.elevators.ToList();
            List<Elevator> customerElevator = new List<Elevator>();


            foreach( Building building in buildingsAll)
            {
                if (building.customer_id == id)
                {
                customerBuilding.Add(building);
                }

            }
            foreach (Elevator elevator in elevatorsAll)
            {
                if (elevator.customer_id == id)
                {
                    customerElevator.Add(elevator);
                }
            }
            Dictionary<string,Int64> buildingsAndElevatorsPerCustomer = new Dictionary<string, Int64>();

            buildingsAndElevatorsPerCustomer.Add("Buildings", customerBuilding.Count);
            buildingsAndElevatorsPerCustomer.Add("Elevators", customerElevator.Count);

            
            if (buildingsAndElevatorsPerCustomer == null)
            {
                return NotFound();
            }

            Dictionary<string,Int64> testDict = new Dictionary<string, Int64>();
            testDict.Add("test",42);
            return buildingsAndElevatorsPerCustomer;
        }


        // PUT: api/Customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        

        private bool CustomerExists(long id)
        {
            return _context.customers.Any(e => e.Id == id);
        }
    }
}
