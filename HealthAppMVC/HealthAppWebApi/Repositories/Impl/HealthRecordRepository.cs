using HealthAppWebApi.App_Data;
using HealthAppWebApi.Models;
using HealthAppWebApi.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HealthAppWebApi.Repositories.Impl
{
    public class HealthRecordRepository : IHealthRecordRepository
    {
        private readonly AppDbContext _context;

        public HealthRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<HealthRecord> GetAll()
        {
            return _context.HealthRecords
                .Include(h => h.Appointment.Patient)
                .Include(h => h.Appointment.Doctor)
                .ToList();
        }

        public HealthRecord GetById(int id)
        {
            return _context.HealthRecords
                .Include(h => h.Appointment.Patient)
                .Include(h => h.Appointment.Doctor)
                .FirstOrDefault(h => h.HealthRecordId == id);
        }

        public HealthRecord GetByAppointmentId(int appointmentId)
        {
            return _context.HealthRecords
                .FirstOrDefault(h => h.AppointmentId == appointmentId);
        }

        public void Add(HealthRecord record)
        {
            _context.HealthRecords.Add(record);
            _context.SaveChanges();
        }
    }
}