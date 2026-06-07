using HealthAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebApi.Repositories.Interface
{
    public interface IHealthRecordRepository
    {
        List<HealthRecord> GetAll();

        HealthRecord GetById(int id);

        HealthRecord GetByAppointmentId(int appointmentId);

        void Add(HealthRecord record);
    }
}
