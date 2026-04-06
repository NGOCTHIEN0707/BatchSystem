using BatchSystem.Domain.Stations;
using Domain.Stations;
using Infrastructure;
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

        public Task AddAsync(Station station)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Station station)
        {
            throw new NotImplementedException();
        }

        public Task<Station?> GetById(string stationId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Station station)
        {
            throw new NotImplementedException();
        }
    }
}
