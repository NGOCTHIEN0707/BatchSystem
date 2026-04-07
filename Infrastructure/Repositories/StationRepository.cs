using BatchSystem.Domain.Stations;
using BatchSystem.Infrastructure.Repositories.Common;
using Domain.Stations;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class StationRepository : BaseRepository, IStationRepository
    {
        public StationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Station station)
        {
            await _context.Stations.AddAsync(station);
        }

        public void Delete(Station station)
        {
            _context.Stations.Remove(station);
        }

        public async Task<Station?> GetById(string stationId)
        {
            var station = await _context.Stations.AsNoTracking().FirstOrDefaultAsync(x=>x.StationId == stationId);
            return station;
        }

        public void UpdateAsync(Station station)
        {
            _context.Update(station);
        }
    }
}
