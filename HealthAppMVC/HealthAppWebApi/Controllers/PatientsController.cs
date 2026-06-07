using HealthAppWebApi.DTOs;
using HealthAppWebApi.Repositories.Impl;
using HealthAppWebApi.Services.Impl;
using HealthAppWebApi.Services.Interface;
using System;
using System.Web.Http;

namespace HealthAppWebApi.Controllers
{
    [RoutePrefix("api/patients")]
    public class PatientsController : ApiController
    {
        private readonly IPatientService _service;

        public PatientsController(
            IPatientService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAllPatients());
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(_service.GetPatientById(id));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(CreatePatientDto dto)
        {
            try
            {
                _service.RegisterPatient(dto);

                return Ok("Patient added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(
            int id,
            CreatePatientDto dto)
        {
            _service.UpdatePatient(id, dto);

            return Ok("Patient updated successfully");
        }
    }
}