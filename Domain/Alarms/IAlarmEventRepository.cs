using Domain.Alarms;
using Domain.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.Alarms
{
    public interface IAlarmEventRepository
    {
        Task AddAsync(AlarmEvent alarmEvent);
        void Delete(AlarmEvent alarmEvent);
        Task<AlarmEvent?> GetById(Guid alarmEventId);
        // Ở đây vẫn cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
        void UpdateAsync(AlarmEvent alarmEvent);
    }
}
