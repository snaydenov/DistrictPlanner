using DistrictPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistrictPlanner.Services
{
    public class RoadsService
    {
        DistrictPlannerContext _context;
        public RoadsService(DistrictPlannerContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Road>>> GetRoads()
        {
            return await _context.Roads.ToListAsync();
        }


        public async Task<ActionResult<Road>> GetRoad(int id)
        {
            var road = await _context.Roads.FindAsync(id);

            return road;
        }

        // PUT: api/Road/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<int> PutRoad(int id, Road road)
        {
            _context.Entry(road).State = EntityState.Modified;

            
            return await _context.SaveChangesAsync();
        }

        // POST: api/Road
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<int> PostRoad(Road road)
        {
            _context.Roads.Add(road);
            
            return await _context.SaveChangesAsync();
        }

        // DELETE: api/Road/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Road>> DeleteRoad(int id)
        {
            var road = await _context.Roads.FindAsync(id);

            _context.Roads.Remove(road);
            await _context.SaveChangesAsync();

            return road;
        }

    }
}
