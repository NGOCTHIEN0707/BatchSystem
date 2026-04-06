using Domain.Alarms;
using Domain.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.Alarms
{
    public interface IAlarmDefinitionRepository
    {
        Task AddAsync(AlarmDefinition alarmDefinition);
        Task Delete(AlarmDefinition alarmDefinition);
        Task<AlarmDefinition?> GetById(string alarmDefinitionId);
        // Ở đây vẫn cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
        Task UpdateAsync(AlarmDefinition alarmDefinition);
    }
}
