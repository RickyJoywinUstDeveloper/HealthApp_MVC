using HealthAppWebApi.DTOs;
using HealthAppWebApi.Models;
using HealthAppWebApi.Repositories.Interface;
using HealthAppWebApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthAppWebApi.Services.Impl
{
    public class HealthRecordService : IHealthRecordService
    {
        private readonly IHealthRecordRepository _recordRepo;
        private readonly IAppointmentRepository _appointmentRepo;

        public HealthRecordService(
            IHealthRecordRepository recordRepo,
            IAppointmentRepository appointmentRepo)
        {
            _recordRepo = recordRepo;
            _appointmentRepo = appointmentRepo;
        }

        public List<HealthRecordDto> GetAll()
        {
            return _recordRepo.GetAll()
                .Select(h => new HealthRecordDto
                {
                    HealthRecordId = h.HealthRecordId,
                    PatientName = h.Appointment.Patient.FullName,
                    DoctorName = h.Appointment.Doctor.FullName,
                    Diagnosis = h.Diagnosis,
                    Prescription = h.Prescription,
                    VisitDate = h.VisitDate,
                    Notes = h.Notes
                })
                .ToList();
        }

        public HealthRecordDto GetById(int id)
        {
            HealthRecord record = _recordRepo.GetById(id);

            if (record == null)
                throw new Exception("Health Record not found.");

            return new HealthRecordDto
            {
                HealthRecordId = record.HealthRecordId,
                PatientName = record.Appointment.Patient.FullName,
                DoctorName = record.Appointment.Doctor.FullName,
                Diagnosis = record.Diagnosis,
                Prescription = record.Prescription,
                VisitDate= record.VisitDate,
                Notes = record.Notes
            };
        }

        public void Add(CreateHealthRecordDto dto)
        {
            Appointment appointment =
                _appointmentRepo.GetById(dto.AppointmentId);

            if (appointment == null)
                throw new Exception("Appointment not found.");

            if (appointment.Status != AppointmentStatus.Confirmed)
            {
                throw new Exception(
                    "Health record can be added only for confirmed appointments.");
            }

            if (_recordRepo.GetByAppointmentId(dto.AppointmentId) != null)
            {
                throw new Exception(
                    "Health record already exists for this appointment.");
            }

            HealthRecord record = new HealthRecord
            {
                AppointmentId = dto.AppointmentId,
                VisitDate = DateTime.Now,
                Diagnosis = dto.Diagnosis,
                Prescription = dto.Prescription,
                Notes = dto.Notes
            };

            _recordRepo.Add(record);

            appointment.Status = AppointmentStatus.Completed;

            _appointmentRepo.Update(appointment);
        }
    }
}