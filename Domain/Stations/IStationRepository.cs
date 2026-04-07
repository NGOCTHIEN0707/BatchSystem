using Domain.Recipes;
using Domain.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchSystem.Domain.Stations
{
    public interface IStationRepository
    {
        Task AddAsync(Station station);
        void Delete(Station station);
        Task<Station?> GetById(string stationId);
        // Ở đây vẫn cần GetById để phục vụ cho các lệnh khác chứ không dùng Get ngay đây để truy vấn dữ liệu
        void UpdateAsync(Station station);
    }
}
