using HealthAppWebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebApi.Services.Interface
{
    public interface IHealthRecordService
    {
        List<HealthRecordDto> GetAll();

        HealthRecordDto GetById(int id);

        void Add(CreateHealthRecordDto dto);
    }
}
