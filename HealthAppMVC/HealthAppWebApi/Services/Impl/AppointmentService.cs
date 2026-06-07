using HealthAppWebApi.DTOs;
using HealthAppWebApi.Models;
using HealthAppWebApi.Repositories.Interface;
using HealthAppWebApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HealthAppWebApi.Constants;

namespace HealthAppWebApi.Services.Impl
{
    public class AppointmentService
     : IAppointmentService
    {
        private readonly
            IAppointmentRepository _repo;

        public AppointmentService(
            IAppointmentRepository repo)
        {
            _repo = repo;
        }

        public List<AppointmentDto>
            GetAllAppointments()
        {
            return _repo.GetAll()
                .Select(a =>
                    new AppointmentDto
                    {
                        AppointmentId =
                            a.AppointmentId,

                        PatientName =
                            a.Patient.FullName,

                        DoctorName =
                            a.Doctor.FullName,

                        ScheduledDate =
                            a.ScheduledDate
                            .ToShortDateString(),

                        TimeSlot =
                            a.TimeSlot,

                        Status =
                            a.Status
                            .ToString()
                    })
                .ToList();
        }

        public void BookAppointment(
            CreateAppointmentDto dto)
        {
            if (dto.ScheduledDate.Date <
                DateTime.Today)
            {
                throw new Exception(
                    "Past date not allowed.");
            }

            if (!TimeSlots.Slots.Contains(dto.TimeSlot))
            {
                throw new Exception(
                    "Invalid time slot.");
            }

            if (dto.ScheduledDate.Date ==
    DateTime.Today)
            {
                DateTime slotDateTime =
                    GetSlotDateTime(
                        dto.ScheduledDate,
                        dto.TimeSlot);

                if (slotDateTime <
                    DateTime.Now)
                {
                    throw new Exception(
                        "Cannot book a past time slot.");
                }
            }

            
            if (_repo.IsDoctorSlotBooked(
                dto.DoctorId,
                dto.ScheduledDate,
                dto.TimeSlot))
            {
                throw new Exception(
                    "Doctor already booked.");
            }

            if (_repo.HasPatientSlotConflict(
                dto.PatientId,
                dto.ScheduledDate,
                dto.TimeSlot))
            {
                throw new Exception(
                    "Patient already has appointment in this slot.");
            }

            if (_repo.HasAppointmentWithDoctorOnSameDay(
    dto.PatientId,
    dto.DoctorId,
    dto.ScheduledDate))
            {
                throw new Exception(
                    "Patient already has an appointment with this doctor on this date.");
            }

            Appointment appointment =
                new Appointment
                {
                    PatientId =
                        dto.PatientId,

                    DoctorId =
                        dto.DoctorId,

                    ScheduledDate =
                        dto.ScheduledDate,

                    TimeSlot =
                        dto.TimeSlot,

                    Status =
                        AppointmentStatus.Pending
                };

            _repo.Add(
                appointment);
        }

        public void ConfirmAppointment(
            int id)
        {
            Appointment appointment =
                _repo.GetById(id);

            if (appointment == null)
            {
                throw new Exception(
                    "Appointment not found.");
            }

            appointment.Status =
                AppointmentStatus.Confirmed;

            _repo.Update(
                appointment);
        }

        public void CancelAppointment(
            int id,
            string reason)
        {
            Appointment appointment =
                _repo.GetById(id);

            if (appointment == null)
            {
                throw new Exception(
                    "Appointment not found.");
            }

            if (appointment.Status ==
                AppointmentStatus.Completed)
            {
                throw new Exception(
                    "Completed appointments cannot be cancelled.");
            }

            appointment.Status =
                AppointmentStatus.Cancelled;

            appointment.CancellationReason =
                reason;

            _repo.Update(
                appointment);
        }

        public List<AppointmentDto>
    GetUpcomingAppointmentsForDoctor(
        int doctorId)
        {
            return _repo
                .GetUpcomingConfirmedAppointmentsByDoctor(
                    doctorId)
                .Select(a =>
                    new AppointmentDto
                    {
                        AppointmentId =
                            a.AppointmentId,

                        PatientName =
                            a.Patient.FullName,

                        DoctorName =
                            a.Doctor.FullName,

                        ScheduledDate =
                            a.ScheduledDate
                            .ToShortDateString(),

                        TimeSlot =
                            a.TimeSlot,

                        Status =
                            a.Status.ToString()
                    })
                .ToList();
        }


        public List<AppointmentDto>
    GetAppointmentsForPatient(
        int patientId)
        {
            return _repo
                .GetAppointmentsByPatient(
                    patientId)
                .Select(a =>
                    new AppointmentDto
                    {
                        AppointmentId =
                            a.AppointmentId,

                        PatientName =
                            a.Patient.FullName,

                        DoctorName =
                            a.Doctor.FullName,

                        ScheduledDate =
                            a.ScheduledDate
                            .ToShortDateString(),

                        TimeSlot =
                            a.TimeSlot,

                        Status =
                            a.Status.ToString()
                    })
                .ToList();
        }

        private DateTime GetSlotDateTime(
    DateTime date,
    string slot)
        {
            string timePart =
                DateTime.Parse(slot)
                .ToString("HH:mm");

            return DateTime.Parse(
                date.ToString("yyyy-MM-dd")
                + " "
                + timePart);
        }
    }
}