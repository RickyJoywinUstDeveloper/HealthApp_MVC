using HealthAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebApi.Repositories.Interface
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAll();

        Appointment GetById(int id);

        void Add(Appointment appointment);

        void Update(Appointment appointment);

        bool IsDoctorSlotBooked(
            int doctorId,
            System.DateTime date,
            string slot);

        bool HasPatientSlotConflict(
            int patientId,
            System.DateTime date,
            string slot);

        bool HasAppointmentWithDoctorOnSameDay(
    int patientId,
    int doctorId,
    DateTime date);

        List<Appointment> GetUpcomingConfirmedAppointmentsByDoctor(
    int doctorId);

        List<Appointment>
    GetAppointmentsByPatient(
        int patientId);
    }
}
