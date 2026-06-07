using HealthAppWebApi.DTOs;
using HealthAppWebApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HealthAppWebApi.Controllers
{
    [RoutePrefix("api/doctors")]
    public class DoctorsController : ApiController
    {
        private readonly IDoctorService _service;

        public DoctorsController(
            IDoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(
                _service.GetAllDoctors());
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok(
                _service.GetDoctorById(id));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(
            CreateDoctorDto dto)
        {
            _service.AddDoctor(dto);

            return Ok(
                "Doctor added successfully.");
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Update(
            int id,
            CreateDoctorDto dto)
        {
            _service.UpdateDoctor(
                id,
                dto);

            return Ok(
                "Doctor updated successfully.");
        }

        [HttpPatch]
        [Route("{id}/status")]
        public IHttpActionResult ChangeStatus(
            int id,
            bool isActive)
        {
            _service.ChangeStatus(
                id,
                isActive);

            return Ok(
                "Doctor status updated.");
        }

        [HttpGet]
        [Route("specialisation/{specialisation}")]
        public IHttpActionResult GetBySpecialisation(
    string specialisation)
        {
            return Ok(
                _service.GetDoctorsBySpecialisation(
                    specialisation));
        }
    }
}