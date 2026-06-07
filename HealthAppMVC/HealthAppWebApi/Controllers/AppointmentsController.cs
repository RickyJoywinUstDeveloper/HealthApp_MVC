using HealthAppWebApi.DTOs;
using HealthAppWebApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HealthAppWebApi.Controllers
{
    [RoutePrefix("api/appointments")]
    public class AppointmentsController
      : ApiController
    {
        private readonly
            IAppointmentService _service;

        public AppointmentsController(
            IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(
                _service.GetAllAppointments());
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Book(
            CreateAppointmentDto dto)
        {
            try
            {
                _service.BookAppointment(dto);

                return Ok(
                    "Appointment booked.");
            }
            catch (Exception ex)
            {
                return BadRequest(
                    ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}/confirm")]
        public IHttpActionResult Confirm(
            int id)
        {
            _service.ConfirmAppointment(
                id);

            return Ok(
                "Appointment confirmed.");
        }

        [HttpPut]
        [Route("{id}/cancel")]
        public IHttpActionResult Cancel(
            int id,
            CancelAppointmentDto dto)
        {
            _service.CancelAppointment(
                id,
                dto.CancellationReason);

            return Ok(
                "Appointment cancelled.");
        }

        [HttpGet]
        [Route("doctor/{doctorId}/upcoming")]
        public IHttpActionResult
    GetUpcomingForDoctor(
        int doctorId)
        {
            return Ok(
                _service
                .GetUpcomingAppointmentsForDoctor(
                    doctorId));
        }

        [HttpGet]
        [Route("patient/{patientId}")]
        public IHttpActionResult
    GetPatientAppointments(
        int patientId)
        {
            return Ok(
                _service
                .GetAppointmentsForPatient(
                    patientId));
        }
    }
}