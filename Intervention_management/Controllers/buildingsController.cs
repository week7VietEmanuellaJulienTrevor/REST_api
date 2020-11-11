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

         // GET: api/buildings
        [HttpGet("needing-intervention")]

        public ActionResult<List<Building>> GetBuildingsNeedingIntervention()
        // public async Task<ActionResult<IEnumerable<Building>>> GetbuildingsNeedingIntervention()
        {
                        // return await _context.elevators.Where( e => e.status != "Active" ).ToListAsync();

            var BatteriesAll =  _context.batteries.ToList();
            var BuildingsAll = _context.buildings.ToList();
            // var ColumnsAll = _context.column.ToList();
            var ElevatorsAll = _context.elevators.ToList();
            // List<Column> interventionCol = new List<CannotUnloadAppDomainException>();
            List<Elevator> interventionEle = new List<Elevator>();
            List<Battery> interventionBatt = new List<Battery>() ;
            List<Building> buildingsInIntervention = new List<Building>() ;

            // foreach (Elevator ele in ElevatorsAll)
            // {
            //     if (ele.status == "Active" || ele.status == "active")
            //     {
            //         Int64 counter = 0;
            //         foreach (Elevator E in interventionEle)
            //         {
            //             if (E.column_id == )
            //         }
            //     }
            // }




            foreach (Battery batt in BatteriesAll)
            {
                if (batt.status == "Active" || batt.status == "active")
                {
                    System.Console.WriteLine(batt.building_id);
                    Int64 counter = 0;
                    foreach (Battery B in interventionBatt)
                    {   
                        System.Console.WriteLine("int");
                        if (B.building_id == batt.building_id)
                        {
                            counter ++ ;
                        }                     

                    }
                    if (counter == 0)
                    {
                        interventionBatt.Add(batt);
                    }
                }
            }
            foreach (Battery batt in interventionBatt)
            {
                foreach (Building build in BuildingsAll)
                {
                    if (build.Id == long.Parse(batt.building_id))
                    {
                        buildingsInIntervention.Add(build);
                    }
                } 
            }
            return buildingsInIntervention;

            // var buildingBatterie = await _context.batteries.Where(b => b.status == "Intervention" || b.status == "intervention" ).ToListAsync();
            
            // foreach (Battery batt in buildingBatterie)
            // {
            //   var buildingsWithBatteriesIntervention =  await _context.buildings.
            //     Where(b => b.Id == long.Parse(batt.building_id)).ToListAsync();
                
            //     buildingsInIntervention.Add(buildingsWithBatteriesIntervention);
            // }
            
            // IEnumerable<Building> buildingsInInterventionEnumerable = buildingsInIntervention;

            // return buildingsInInterventionEnumerable;
            // return await _context.buildings.ToListAsync();
        }

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
