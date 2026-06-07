using HealthAppWebApi.DTOs;
using HealthAppWebApi.Repositories.Interface;
using HealthAppWebApi.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace HealthAppWebApi.Controllers
{
    [RoutePrefix("api/healthrecords")]
    public class HealthRecordsController : ApiController
    {
        private readonly IHealthRecordService _service;

        public HealthRecordsController(
            IHealthRecordService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(
            CreateHealthRecordDto dto)
        {
            try
            {
                _service.Add(dto);

                return Ok(
                    "Health Record added successfully. Appointment completed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}