using BatchSystem.Domain.Alarms;
using BatchSystem.Infrastructure.Repositories.Common;
using Domain.Alarms;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
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

        public async Task AddAsync(AlarmEvent alarmEvent)
        {
            await _context.AlarmEvents.AddAsync(alarmEvent);
        }

        public void Delete(AlarmEvent alarmEvent)
        {
            _context.AlarmEvents.Remove(alarmEvent);
        }

        public async Task<AlarmEvent?> GetById(Guid alarmEventId)
        {
            var alarmEvent = await _context.AlarmEvents.AsNoTracking().FirstOrDefaultAsync(x=>x.AlarmEventId == alarmEventId);
            return alarmEvent;
        }

        public void UpdateAsync(AlarmEvent alarmEvent)
        {
            _context.AlarmEvents.Update(alarmEvent);
        }
    }
}
