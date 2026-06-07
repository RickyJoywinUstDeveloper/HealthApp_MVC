using HealthAppWebApi.App_Data;
using HealthAppWebApi.Models;
using HealthAppWebApi.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HealthAppWebApi.Repositories.Impl
{
    public class AppointmentRepository
     : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public List<Appointment> GetAll()
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToList();
        }

        public Appointment GetById(int id)
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefault(a => a.AppointmentId == id);
        }

        public void Add(
            Appointment appointment)
        {
            _context.Appointments.Add(appointment);

            _context.SaveChanges();
        }

        public void Update(
            Appointment appointment)
        {
            _context.Entry(appointment)
                .State =
                EntityState.Modified;

            _context.SaveChanges();
        }

        public bool IsDoctorSlotBooked(
            int doctorId,
            System.DateTime date,
            string slot)
        {
            return _context.Appointments
                .Any(a =>
                    a.DoctorId == doctorId &&
                    DbFunctions.TruncateTime(
                        a.ScheduledDate)
                    ==
                    DbFunctions.TruncateTime(date) &&
                    a.TimeSlot == slot &&
                    a.Status !=
                    AppointmentStatus.Cancelled);
        }

        public bool HasPatientSlotConflict(
            int patientId,
            System.DateTime date,
            string slot)
        {
            return _context.Appointments
                .Any(a =>
                    a.PatientId == patientId &&
                    DbFunctions.TruncateTime(
                        a.ScheduledDate)
                    ==
                    DbFunctions.TruncateTime(date) &&
                    a.TimeSlot == slot &&
                    a.Status !=
                    AppointmentStatus.Cancelled);
        }

        public bool HasAppointmentWithDoctorOnSameDay(
    int patientId,
    int doctorId,
    DateTime date)
        {
            return _context.Appointments
                .Any(a =>
                    a.PatientId == patientId &&
                    a.DoctorId == doctorId &&
                    DbFunctions.TruncateTime(
                        a.ScheduledDate)
                    ==
                    DbFunctions.TruncateTime(
                        date) &&
                    a.Status !=
                    AppointmentStatus.Cancelled);
        }

        public List<Appointment>
    GetUpcomingConfirmedAppointmentsByDoctor(
        int doctorId)
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a =>
                    a.DoctorId == doctorId &&
                    a.Status ==
                        AppointmentStatus.Confirmed &&
                    a.ScheduledDate >=
                        DateTime.Today)
                .OrderBy(a => a.ScheduledDate)
                .ToList();
        }


        public List<Appointment>
    GetAppointmentsByPatient(
        int patientId)
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a =>
                    a.PatientId == patientId)
                .OrderByDescending(
                    a => a.ScheduledDate)
                .ToList();
        }


    }
}