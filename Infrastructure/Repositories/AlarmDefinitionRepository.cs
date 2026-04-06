using BatchSystem.Domain.Alarms;
using Domain.Alarms;
using Domain.Lines;
using Infrastructure;
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

        public Task AddAsync(AlarmDefinition alarmDefinition)
        {
            throw new NotImplementedException();
        }

        public Task Delete(AlarmDefinition alarmDefinition)
        {
            throw new NotImplementedException();
        }

        public Task<AlarmDefinition?> GetById(string alarmDefinitionId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AlarmDefinition alarmDefinition)
        {
            throw new NotImplementedException();
        }
    }
}
