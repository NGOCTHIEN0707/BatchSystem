using BatchSystem.Domain.Alarms;
using Domain.Alarms;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class AlarmEventRepository : BaseRepository, IAlarmEventRepository
    {
        public AlarmEventRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task AddAsync(AlarmEvent alarmEvent)
        {
            throw new NotImplementedException();
        }

        public Task Delete(AlarmEvent alarmEvent)
        {
            throw new NotImplementedException();
        }

        public Task<AlarmEvent?> GetById(Guid alarmEventId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AlarmEvent alarmEvent)
        {
            throw new NotImplementedException();
        }
    }
}
