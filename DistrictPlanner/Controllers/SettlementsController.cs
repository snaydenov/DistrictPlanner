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
    public class SettlementsController : ControllerBase
    {
        private readonly SettlementsService _settlementService; 

        public SettlementsController(DistrictPlannerContext context)
        {
            this._settlementService = new SettlementsService(context);
        }

        // GET: api/Settlement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Settlement>>> GetSettlements()
        {
            return await _settlementService.GetSettlements();
        }

        // POST: api/Settlement
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpGet("main")]
        public async Task<ActionResult<Settlement>> GetMainSettlement()
        {
            return await _settlementService.GetMainSettlement();
        }

        // GET: api/Settlement/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Settlement>> GetSettlement(int id)
        {
            var settlement = await _settlementService.GetSettlement(id);

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

            await _settlementService.PutSettlement(id,settlement);

            return NoContent();
        }

        // POST: api/Settlement
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Settlement>> PostSettlement(Settlement settlement)
        {
            await _settlementService.PostSettlement(settlement);

            return CreatedAtAction("GetSettlement", new { id = settlement.SettlementId }, settlement);
        }

        

        // DELETE: api/Settlement/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Settlement>> DeleteSettlement(int id)
        {
            return await _settlementService.DeleteSettlement(id);
        }

    }
}
