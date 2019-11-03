using DistrictPlanner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DistrictPlanner.Services
{
    public class SettlementsService
    {
        DistrictPlannerContext _context;

        private const decimal MAX_VALUE = 100000000;

        public SettlementsService(DistrictPlannerContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Settlement>>> GetSettlements()
        {
            return await _context.Settlements.ToListAsync();
        }

        public async Task<ActionResult<Settlement>> GetMainSettlement()
        {
            List<Settlement> settlements = await _context.Settlements.ToListAsync();
            List<Road> roads = await _context.Roads.ToListAsync();

            decimal[,] distances = new decimal[settlements.Count, settlements.Count];

            for (int i = 0; i < settlements.Count; i++)
            {
                for (int j = 0; j < settlements.Count; j++)
                {
                    decimal? distance = roads
                        .Where(r => (r.StartSettlementId == settlements[i].SettlementId && r.EndSettlementId == settlements[j].SettlementId)
                            || (r.StartSettlementId == settlements[j].SettlementId && r.EndSettlementId == settlements[i].SettlementId))
                        .Select(r => (decimal?)r.Distance)
                        .DefaultIfEmpty()
                        .Min();
                    if (distance.HasValue)
                    {
                        distances[i, j] = distance.Value;
                    }
                    else
                    {
                        distances[i, j] = i != j ? MAX_VALUE : 0;
                    }
                }
            }
            for (int i = 0; i < settlements.Count; i++)
            {
                for (int j = 0; j < settlements.Count; j++)
                {
                    for (int k = 0; k < settlements.Count; k++)
                    {
                        if (distances[i, j] > distances[i, k] + distances[k, j])
                        {
                            distances[i, j] = distances[i, k] + distances[k, j];
                        }
                    }
                }
            }

            decimal[] maximumDistances = new decimal[settlements.Count];

            for (int i = 0; i < settlements.Count; i++)
            {
                maximumDistances[i] = 0;

                for (int j = 0; j < settlements.Count; j++)
                {
                    if (distances[i, j] > maximumDistances[i])
                    {
                        maximumDistances[i] = distances[i, j];
                    }
                }
            }

            var mainSettlement = settlements[Array.IndexOf(maximumDistances, maximumDistances.Min())];
            mainSettlement.RoadsEndSettlement = null;
            mainSettlement.RoadsStartSettlement = null;

            return mainSettlement;
        }

        public async Task<ActionResult<Settlement>> GetSettlement(int id)
        {
            return await _context.Settlements.FindAsync(id);
        }

        public async Task<int> PutSettlement(int id, Settlement settlement)
        {
            _context.Entry(settlement).State = EntityState.Modified;

             return await _context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task<int> PostSettlement(Settlement settlement)
        {
            _context.Settlements.Add(settlement);
            
            return await _context.SaveChangesAsync();
        }


        public async Task<ActionResult<Settlement>> DeleteSettlement(int id)
        {
            var settlement = await _context.Settlements.FindAsync(id);
            if (settlement == null)
            {
                return null;
            }

            _context.Settlements.Remove(settlement);
            await _context.SaveChangesAsync();

            return settlement;
        }
    }
}
