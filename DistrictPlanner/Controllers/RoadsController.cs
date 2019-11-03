using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DistrictPlanner.Models;
using DistrictPlanner.Services;

namespace DistrictPlanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoadsController : ControllerBase
    {
        private readonly RoadsService _roadsService;

        public RoadsController(DistrictPlannerContext context)
        {
            _roadsService = new RoadsService(context);
        }

        // GET: api/Road
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Road>>> GetRoads()
        {
            return await _roadsService.GetRoads();
        }

        // GET: api/Road/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Road>> GetRoad(int id)
        {
            var road = await _roadsService.GetRoad(id);

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
            await _roadsService.PutRoad(id, road);
            
            return NoContent();
        }

        // POST: api/Road
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Road>> PostRoad(Road road)
        {
            await _roadsService.PostRoad(road);
            
            return CreatedAtAction("GetRoad", new { id = road.RoadId }, road);
        }

        // DELETE: api/Road/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Road>> DeleteRoad(int id)
        {
            return await _roadsService.DeleteRoad(id);
        }
    }
}
