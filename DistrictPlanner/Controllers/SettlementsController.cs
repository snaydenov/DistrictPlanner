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
    public class SettlementsController : ControllerBase
    {
        private readonly DistrictPlannerContext _context;

        public SettlementsController(DistrictPlannerContext context)
        {
            _context = context;
        }

        // GET: api/Settlement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Settlement>>> GetSettlements()
        {
            return await _context.Settlements.ToListAsync();
        }

        // GET: api/Settlement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Settlement>> GetSettlement(int id)
        {
            var settlement = await _context.Settlements.FindAsync(id);

            if (settlement == null)
            {
                return NotFound();
            }

            return settlement;
        }

        // PUT: api/Settlement/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSettlement(int id, Settlement settlement)
        {
            if (id != settlement.SettlementId)
            {
                return BadRequest();
            }

            _context.Entry(settlement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SettlementExists(id))
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

        // POST: api/Settlement
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Settlement>> PostSettlement(Settlement settlement)
        {
            _context.Settlements.Add(settlement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SettlementExists(settlement.SettlementId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSettlement", new { id = settlement.SettlementId }, settlement);
        }

        // DELETE: api/Settlement/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Settlement>> DeleteSettlement(int id)
        {
            var settlement = await _context.Settlements.FindAsync(id);
            if (settlement == null)
            {
                return NotFound();
            }

            _context.Settlements.Remove(settlement);
            await _context.SaveChangesAsync();

            return settlement;
        }

        private bool SettlementExists(int id)
        {
            return _context.Settlements.Any(e => e.SettlementId == id);
        }
    }
}
