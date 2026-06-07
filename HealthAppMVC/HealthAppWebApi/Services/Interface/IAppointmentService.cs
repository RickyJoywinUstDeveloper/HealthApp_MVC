using HealthAppWebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthAppWebApi.Services.Interface
{
    public interface IAppointmentService
    {
        List<AppointmentDto>
            GetAllAppointments();

        void BookAppointment(
            CreateAppointmentDto dto);

        void ConfirmAppointment(
            int id);

        void CancelAppointment(
            int id,
            string reason);

        List<AppointmentDto>
    GetUpcomingAppointmentsForDoctor(
        int doctorId);

        List<AppointmentDto> GetAppointmentsForPatient (int patientId);
    }
}
