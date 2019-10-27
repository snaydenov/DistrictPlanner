using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DistrictPlanner.Models;

namespace DistrictPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadsController : ControllerBase
    {
        private readonly DistrictPlannerContext _context;

        public RoadsController(DistrictPlannerContext context)
        {
            _context = context;
        }

        // GET: api/Road
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Road>>> GetRoads()
        {
            return await _context.Roads.ToListAsync();
        }

        // GET: api/Road/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Road>> GetRoad(int id)
        {
            var road = await _context.Roads.FindAsync(id);

            if (road == null)
            {
                return NotFound();
            }

            return road;
        }

        // PUT: api/Road/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoad(int id, Road road)
        {
            if (id != road.RoadId)
            {
                return BadRequest();
            }

            _context.Entry(road).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoadExists(id))
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

        // POST: api/Road
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Road>> PostRoad(Road road)
        {
            _context.Roads.Add(road);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RoadExists(road.RoadId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRoad", new { id = road.RoadId }, road);
        }

        // DELETE: api/Road/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Road>> DeleteRoad(int id)
        {
            var road = await _context.Roads.FindAsync(id);
            if (road == null)
            {
                return NotFound();
            }

            _context.Roads.Remove(road);
            await _context.SaveChangesAsync();

            return road;
        }

        private bool RoadExists(int id)
        {
            return _context.Roads.Any(e => e.RoadId == id);
        }
    }
}
