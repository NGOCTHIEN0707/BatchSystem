using BatchSystem.Domain.Alarms;
using BatchSystem.Infrastructure.Repositories.Common;
using Domain.Alarms;
using Domain.Lines;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Infrastructure.Repositories
{
    public class AlarmDefinitionRepository : BaseRepository, IAlarmDefinitionRepository
    {
        public AlarmDefinitionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task AddAsync(AlarmDefinition alarmDefinition)
        {
            await _context.AlarmDefinitions.AddAsync(alarmDefinition);
        }

        public void Delete(AlarmDefinition alarmDefinition)
        {
            _context.AlarmDefinitions.Remove(alarmDefinition);
        }

        public async Task<AlarmDefinition?> GetById(string alarmDefinitionId)
        {
            var alarmDefi = await _context.AlarmDefinitions.AsNoTracking().FirstOrDefaultAsync(x=>x.AlarmDefinitionId == alarmDefinitionId);
            return alarmDefi;
        }

        public void UpdateAsync(AlarmDefinition alarmDefinition)
        {
            _context.AlarmDefinitions.Update(alarmDefinition);
        }
    }
}
